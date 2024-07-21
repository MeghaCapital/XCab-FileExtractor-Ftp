using System.Configuration;

namespace Core.Helpers
{
    public static class ConfigurationHelper
    {
        public static string AppName => ConfigurationManager.AppSettings["AppName"];

    }
}
