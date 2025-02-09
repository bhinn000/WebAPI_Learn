namespace WebAPI_Learn.DEPENDENCYINJECTION
{
    public class PropertyInjection
    {
        public class SavingAccount : IAccount
        {
            public void PrintDetails()
            {
                Console.WriteLine("I am saving account from property");
            }

        }

        public class CurrentAccount : IAccount
        {
            public void PrintDetails()
            {
                Console.WriteLine("I am current account from property");
            }

        }

        public interface IAccount
        {
            void PrintDetails();
        }

        public class Account
        {

            public IAccount accountType { get; set; } //to hold reference for specific class
            
            public void PrintAccountType()
            {
                accountType.PrintDetails();
            }

        }

        //to execute this , you have comment out Program.cs , similar for other type of dependency injection , now thiss has been executed by registering it to the service
        public static void Main(string[] args)
        {

            Account account1 = new Account();
            account1.accountType=new SavingAccount();
            account1.PrintAccountType();

            Account account2=new Account();
            account2.accountType = new CurrentAccount();
            account2.PrintAccountType();
        }
    }
}

