using System;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services
{
    public class PaymentService : IPaymentService
    {
        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            // we could do this from a purely technical approach, however, to be as graceful as 
            // possible, we simply instantiate a new request object will blank values. 
            // The important value is "DebtorAccountNumber" which will be blanlk
            //
            //if (request == null)
            //    throw new ArgumentNullException("Empty request object.");

            if (request == null)
                request = new MakePaymentRequest();

            var accountDataStoreGetData = new AccountDataStore();
            var debtorAccount = accountDataStoreGetData.GetAccount(request.DebtorAccountNumber) ?? new Account();
            var creditorAccount = accountDataStoreGetData.GetAccount(request.CreditorAccountNumber) ?? new Account();

            if (debtorAccount.IsEmpty
                || creditorAccount.IsEmpty)
                return new MakePaymentResult { Success = false, ErrorMessage = "Either creditor or debtor account numbers are invalid. " };

            var result = new MakePaymentResult();

            var accountDataStoreUpdateData = new AccountDataStore();
            try
            {
                if (debtorAccount.AllowsB2B ||
                    debtorAccount.IsExpeditedPayment(request.Amount) ||
                    debtorAccount.IsAutomatedPayment)
                {
                    // Makes no sense to just take money out of an account, it also has to be added somewhere... creditorAccount

                    debtorAccount.Balance -= request.Amount;
                    creditorAccount.Balance += request.Amount;

                    // the payment must occur inside a single transaction, so let's assume we have 
                    // a transaction context
                    if (accountDataStoreUpdateData.UpdateAccount(debtorAccount, creditorAccount))
                    {
                        result.Success = true;
                    }
                    else
                    {
                        debtorAccount.Balance += request.Amount;
                        creditorAccount.Balance -= request.Amount;
                    }
                }
            }
            catch (Exception ex)
            {
                // Generally we don't expose internal exception messages, however, we do need a way
                // to alert the user that something occurred and we need a way to describe a bit about
                // what occurred. This is the same as Windows command "net helpmsg <errorcode>"

                result.ErrorMessage = ex.Message;
            }

            return result;
        }
    }
}
