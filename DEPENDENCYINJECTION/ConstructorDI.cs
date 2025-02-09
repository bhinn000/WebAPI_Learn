using static WebAPI_Learn.DEPENDENCYINJECTION.ConstructorDI;
//constructor injection
namespace WebAPI_Learn.DEPENDENCYINJECTION
{

    public class ConstructorDI
    {

        public class SavingAccount : IAccount
        {
            public void PrintDetails()
            {
                Console.WriteLine("I am saving account from constructor");
            }

        }

        public class CurrentAccount : IAccount
        {
            public void PrintDetails()
            {
                Console.WriteLine("I am current account from constructor");
            }

        }

        public interface IAccount
        {
            void PrintDetails();
        }

        public class Account
        {
            IAccount _account;
            public Account(IAccount account)
            {
                _account = account;
            }
            public void PrintAccountType()
            {
                _account.PrintDetails();
            }

            //public Account() instead of doing this we can do above method
            //{
            //    _account = new SavingAccount(); 
            //}

        }

        //to execute this , you have comment out Program.cs , similar for other type of dependency injection , now thiss has been executed by registering it to the service
        //public static void Main(string[] args)
        //{
        //    IAccount saRef = new SavingAccount();
        //    Account account1 = new Account(saRef);//if object of Saving sent , call corresponding method
        //    account1.PrintAccountType();

        //    IAccount caRef = new CurrentAccount();
        //    Account account2 = new Account(caRef);
        //    account2.PrintAccountType();
        //}
    }
}

//classes communicate with another classes using interface