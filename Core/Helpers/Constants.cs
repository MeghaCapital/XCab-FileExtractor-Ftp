using System;

namespace Core.Helpers
{
    [Serializable]
    public static class Constants
    {
        public struct ErrorList
        {
            public const string Error = "Error";
            public const string Information = "Information";
            public const string Debug = "Debug";
            public const string Warning = "Warning";
            public const string Verbose = "Verbose";
            public const string Fatal = "Fatal";
        }
    }
}
