using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using xcab.como.common.Logging;
using xcab.como.common.Logging.TextFileLog;

namespace xcab.como.common.Struct
{
    public class KeyValueList<T> : List<KeyValuePair<string, object>> where T : ISerialisable
    {
        private T entity;
        private static readonly IComoTextFileGenerator textFileLog;

        static KeyValueList()
        {
            KeyValueList<T>.textFileLog = new ComoTextFileGenerator("C:\\Logs\\Struct\\", "XCAB_COMO-" + DateTime.Now.ToString("yyyyMMdd"));
        }

        public KeyValueList(T entity) : base()
        {
            this.entity = entity;
            try
            {
                List<KeyValuePair<string, object>> lkvp = JsonConvert.DeserializeObject<List<KeyValuePair<string, object>>>(this.entity.Json);
                base.AddRange(lkvp);
            }
            catch (Exception ex)
            {
                KeyValueList<T>.textFileLog.Write(GetType().Name + " - svc/xcab-como - ", "Could not desrialise to key value list.", Constants.ErrorList.Error);
            }
        }
    }
}
