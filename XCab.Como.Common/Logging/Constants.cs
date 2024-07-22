using System;
using System.Collections.Generic;
using System.Text;

namespace xcab.como.common.Logging
{
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

        public struct ProjectType
        {
            public const string Service = "SVC";
            public const string ApplicationInterface = "API";
            public const string Common = "CMN";
            public const string Client = "CLNT";
        }
    }
}
