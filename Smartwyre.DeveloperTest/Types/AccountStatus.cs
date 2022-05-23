namespace Smartwyre.DeveloperTest.Types
{
    public enum AccountStatus
    {
        /// <summary>
        /// Account has not yet been activated. Default status.
        /// </summary>
        Inactive, 

        /// <summary>
        /// Account is live for trading.
        /// </summary>
        Live,

        /// <summary>
        /// Account is no longer usable.
        /// </summary>
        Disabled,


        InboundPaymentsOnly
    }
}
