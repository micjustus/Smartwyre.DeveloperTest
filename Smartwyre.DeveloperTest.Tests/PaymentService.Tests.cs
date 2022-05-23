using System;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests
{
    public class PaymentServiceTests
    {
        private class ServiceEventArgs: EventArgs
        {
            public Exception Ex { get; set; }
        }

        [Fact]
        public void Test_Blank_Account_Checks_Fail_If_Blank()
        {
            var test = new Account();
            Assert.False(test.IsExpeditedPayment(-1234.234m));
            Assert.False(test.IsAutomatedPayment);
            Assert.False(test.AllowsB2B);
        }

        [Fact]
        public void Test_Account_Checks_Pass_Expedited()
        {
            var test = new Account("test");
            test.AllowedPaymentSchemes = AllowedPaymentSchemes.ExpeditedPayments;
            
            Assert.True(test.IsExpeditedPayment(1234.234m));
            Assert.False(test.IsAutomatedPayment);
            Assert.False(test.AllowsB2B);
        }

        [Fact]
        public void Test_Account_Checks_Pass_Automated_Not_Live()
        {
            var test = new Account("test");
            test.AllowedPaymentSchemes = AllowedPaymentSchemes.AutomatedPaymentSystem;

            Assert.False(test.IsExpeditedPayment(1234.234m));
            Assert.False(test.IsAutomatedPayment);
            Assert.False(test.AllowsB2B);
        }

        [Fact]
        public void Test_Account_Checks_Pass_Automated_Live()
        {
            var test = new Account("test");
            test.AllowedPaymentSchemes = AllowedPaymentSchemes.AutomatedPaymentSystem;
            test.Status = AccountStatus.Live;

            Assert.False(test.IsExpeditedPayment(1234.234m));
            Assert.True(test.IsAutomatedPayment);
            Assert.False(test.AllowsB2B);
        }

        [Fact]
        public void Test_Account_Checks_Pass_B2B()
        {
            var test = new Account("test");
            test.AllowedPaymentSchemes = AllowedPaymentSchemes.BankToBankTransfer;

            Assert.False(test.IsExpeditedPayment(1234.234m));
            Assert.False(test.IsAutomatedPayment);
            Assert.True(test.AllowsB2B);
        }

        [Fact]
        public void Test_Service_Null_Request_Fails()
        {
            //Assert.Raises<ServiceEventArgs>(attach => { }, detach => { },
            //    () =>
            //{
            //    var test = new PaymentService();
            //    test.MakePayment(null);
            //});

            var test = new PaymentService();
            var result = test.MakePayment(null);

            Assert.NotNull(result);
            Assert.False(result.Success);
        }

        [Fact]
        public void Test_Service_Request_No_Accounts()
        {
            var test = new PaymentService();
            var result = test.MakePayment(new MakePaymentRequest { });

            Assert.NotNull(result);
            Assert.False(result.Success);
        }

        [Fact]
        public void Test_Service_Request_Creditor_No_Debtor()
        {
            var test = new PaymentService();
            var result = test.MakePayment(new MakePaymentRequest { CreditorAccountNumber = "A"});

            Assert.NotNull(result);
            Assert.False(result.Success);
        }

        [Fact]
        public void Test_Service_Request_Debtor_No_Creditor()
        {
            var test = new PaymentService();
            var result = test.MakePayment(new MakePaymentRequest { DebtorAccountNumber = "B" });

            Assert.NotNull(result);
            Assert.False(result.Success);
        }

        [Fact]
        public void Test_Service_Request_Accounts()
        {
            var test = new PaymentService();
            var result = test.MakePayment(new MakePaymentRequest { DebtorAccountNumber = "A", CreditorAccountNumber = "B" });

            Assert.NotNull(result);
            Assert.True(result.Success);
        }
    }
}
