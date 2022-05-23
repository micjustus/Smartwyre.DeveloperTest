using System;
using Smartwyre.DeveloperTest.Services;

namespace Smartwyre.DeveloperTest.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new PaymentService();

            Console.Write("Calling service with null request...");
            var result = service.MakePayment(null);
            Console.WriteLine($"{result.Success}");

            Console.Write("Calling service with non-null request, no account number...");
            result = service.MakePayment(new Types.MakePaymentRequest());
            Console.WriteLine($"{result.Success}");

            Console.Write("Calling service with non-null request, creditor account number...");
            result = service.MakePayment(new Types.MakePaymentRequest() {  CreditorAccountNumber = "A"});
            Console.WriteLine($"{result.Success}");


            Console.Write("Calling service with non-null request, debtor account number...");
            result = service.MakePayment(new Types.MakePaymentRequest() { DebtorAccountNumber = "A" });
            Console.WriteLine($"{result.Success}");


            Console.Write("Calling service with non-null request, creditor and debtor account number...");
            result = service.MakePayment(new Types.MakePaymentRequest() { CreditorAccountNumber = "A", DebtorAccountNumber = "B" });
            Console.WriteLine($"{result.Success}");

            Console.ReadKey();
        }
    }
}
