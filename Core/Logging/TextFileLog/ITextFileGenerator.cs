
namespace Core.Logging.TextFileLog
{
    public interface ITextFileGenerator
    {
        void Write(string LogPoint, string Detail, string LogType);
    }
}
