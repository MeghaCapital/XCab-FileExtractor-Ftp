namespace Core.Helpers
{
    public abstract class FileNameHelper
    {
        public static string GetDateTimeForFile()
        {
            var dateTime = DateTime.Now.Day + "_" + DateTime.Now.Month + "_" +
                             DateTime.Now.Year + "_" + DateTime.Now.Hour + "_" +
                             DateTime.Now.Minute + "_" + DateTime.Now.Millisecond;
            return dateTime;
        }
    }
}