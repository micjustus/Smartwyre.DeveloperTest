using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Data
{
    public class AccountDataStore
    {
        public Account GetAccount(string accountNumber)
        {
            // Usually, throw an exception, but we don't really need to since a blank account instance
            // reflectes the blank account number. Bad input data = bad output data, like for like.

            if (string.IsNullOrWhiteSpace(accountNumber))
                return new Account();

            // We do some hard-coding here to simulate populating valid account if 
            // account numbers are provided
            if (string.Equals(accountNumber, "A") || 
                string.Equals(accountNumber, "B"))
                return new Account(accountNumber) { AllowedPaymentSchemes = AllowedPaymentSchemes.AutomatedPaymentSystem, Status = AccountStatus.Live};

            // Access database to retrieve account, code removed for brevity 
            return new Account(accountNumber);
        }

        public bool UpdateAccount(Account debitAccount, Account creditAccount)
        {
            // Update account in database, code removed for brevity
            if (debitAccount == null || creditAccount == null)
                return false;

            // assume a DB transaction context
            return true;
        }
    }
}
