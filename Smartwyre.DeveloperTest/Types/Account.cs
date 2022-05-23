namespace Smartwyre.DeveloperTest.Types
{
    public class Account
    {
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public AccountStatus Status { get; set; }
        public AllowedPaymentSchemes AllowedPaymentSchemes { get; set; }

        /// <summary>
        /// Returns true if this account has no valid data, false otherwise.
        /// </summary>
        public bool IsEmpty => string.IsNullOrEmpty(AccountNumber) && Status == AccountStatus.Inactive && Balance == 0;

        public Account() { }

        public Account(string accountNumber)
        {
            AccountNumber = accountNumber;
        }

        /// <summary>
        /// Returns true if the account is live and supports automated payments, false otherwise.
        /// </summary>
        public bool IsAutomatedPayment => AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.AutomatedPaymentSystem) && Status == AccountStatus.Live && !string.IsNullOrWhiteSpace(AccountNumber);

        /// <summary>
        /// True if this account supports Bank-to-Bank transfers, false otherwise.
        /// </summary>
        public bool AllowsB2B => AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.BankToBankTransfer) && !string.IsNullOrWhiteSpace(AccountNumber);

        /// <summary>
        /// Returns true if the account supported expedited payments, false otherwise.
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public bool IsExpeditedPayment(decimal amount) => AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.ExpeditedPayments) && Balance < amount && !string.IsNullOrWhiteSpace(AccountNumber);
    }
}
