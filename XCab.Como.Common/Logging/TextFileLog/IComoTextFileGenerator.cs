using System;
using System.Collections.Generic;
using System.Text;

namespace xcab.como.common.Logging.TextFileLog
{
    public interface IComoTextFileGenerator
    {
        void Write(string LogPoint, string Detail, string LogType);
    }
}
