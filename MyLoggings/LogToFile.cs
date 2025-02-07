namespace WebAPI_Learn.MyLoggings
{
    //implementing service
    public class LogToFile : IMyLoggings
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine("Log to File");
        }
    }
}
