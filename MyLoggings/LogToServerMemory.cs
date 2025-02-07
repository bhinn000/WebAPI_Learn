namespace WebAPI_Learn.MyLoggings
{
    //implementing service
    public class LogToServerMemory : IMyLoggings
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine("Log to Server Memory");
        }
    }
}
