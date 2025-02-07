namespace WebAPI_Learn.MyLoggings
{
    //implementing service
    public class LogToDB: IMyLoggings
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine("Log to DB");
        }
    }
}
