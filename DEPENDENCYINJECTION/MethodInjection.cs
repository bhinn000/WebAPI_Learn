namespace WebAPI_Learn.DEPENDENCYINJECTION
{
    public class MethodInjection
    {
        public class SavingAccount : IAccount
        {
            public void PrintDetails()
            {
                Console.WriteLine("I am saving account method");
            }

        }

        public class CurrentAccount : IAccount
        {
            public void PrintDetails()
            {
                Console.WriteLine("I am current account method");
            }

        }

        public interface IAccount
        {
            void PrintDetails();
        }

        public class Account
        {
            public void PrintAccountType(IAccount account)
            {
                account.PrintDetails();
            }
        }

        //to execute this , you have comment out Program.cs , similar for other type of dependency injection , now thiss has been executed by registering it to the service
        public static void Main(string[] args)
        {

            Account account1 = new Account();//if object of Saving sent , call corresponding method
            account1.PrintAccountType(new SavingAccount());

            Account account2 = new Account();
            account2.PrintAccountType(new CurrentAccount());
        }
    }
}

