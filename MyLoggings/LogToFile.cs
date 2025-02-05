namespace WebAPI_Learn.MyLoggings
{
    public class LogToFile : IMyLoggings
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine("Log to File");
        }
    }
}
