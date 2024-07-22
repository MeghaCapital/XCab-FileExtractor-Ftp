using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using xcab.como.common.Logging;
using xcab.como.common.Logging.TextFileLog;

namespace xcab.como.common.Struct
{
    public class NestedKeyValueList<T> : List<List<KeyValuePair<string, object>>> where T : ISerialisable
    {
        private T entity;
        private static readonly IComoTextFileGenerator textFileLog;

        static NestedKeyValueList()
        {
            NestedKeyValueList<T>.textFileLog = new ComoTextFileGenerator("C:\\Logs\\Struct\\", "XCAB_COMO-" + DateTime.Now.ToString("yyyyMMdd"));
        }

        public NestedKeyValueList(T entity)
        {
            this.entity = entity;
            try
            {
                List<List<KeyValuePair<string, object>>> llkvp = JsonConvert.DeserializeObject<List<List<KeyValuePair<string, object>>>>(this.entity.Json);
                base.AddRange(llkvp);
            }
            catch (Exception ex)
            {
                NestedKeyValueList<T>.textFileLog.Write(GetType().Name + " - svc/xcab-como - ", "Could not deserialise to nested key value list.", Constants.ErrorList.Error);
            }
        }
    }
}
