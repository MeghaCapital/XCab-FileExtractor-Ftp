using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xcab.como.common.Client;
using xcab.como.common.Struct;

namespace xcab.como.common.Service
{
    public class IdentityCollectionManager : IIdentityCollectionManager
    {
        protected static readonly IdentityClient client;

        static IdentityCollectionManager()
        {
            client = new IdentityClient();
        }

        public IdentityCollectionManager()
        {

        }

        public static void Initialise(string apiToken)
        {
            IdentityCollectionManager.client.Initialise(apiToken);
        }

        public async Task<int?> Search<I>(I entityEnum, string entityName, string property, string searchInput, Dictionary<string, string> filters = null) where I : struct, IConvertible
        {
            if (!typeof(I).IsEnum)
            {
                throw new ArgumentException("Please provide Enum");
            }
            //int nestIndex = DetermineNestIndex(property);
            Dictionary<string, int> nodeCount = GenerateNodeCount(entityName, property);
            if (nodeCount != null)
            {
                bool loaded = await IdentityCollectionManager.client.LoadInstance(entityName, property, searchInput, nodeCount);
                if (loaded)
                {
                    return new IdentityList(IdentityCollectionManager.client).Id(entityEnum, filters);
                }
            }
            else
            {
                //IdentityCollectionManager.client.GetInstance(this.apiToken, entityName, property, searchInput);
            }
            return null;
        }

        /*protected int DetermineNestIndex(string property)
        {
            // Number of open braces must be equal to the number of closed braces for property to be well formed
            if (property.Count(b => char.Equals(b, '{')) == property.Count(b => char.Equals(b, '}')))
            {
                int idIndex = property.IndexOf("id");
                int openBraceEnd = property.LastIndexOf("{");
                int closeBraceStart = property.IndexOf("}");
                if ((openBraceEnd > -1) && (closeBraceStart > -1) && !((idIndex < property.IndexOf("{")) || (idIndex > property.LastIndexOf("}"))))
                {
                    while (!((openBraceEnd < idIndex) && (closeBraceStart > idIndex)))
                    {
                        //Not decrementing
                        openBraceEnd = property.Substring(0,openBraceEnd).LastIndexOf("{", openBraceEnd);
                        var len = property.Length;
                        //Not incrementing
                        closeBraceStart = property.Substring(closeBraceStart+1).IndexOf("}", closeBraceStart);
                    }
                    return property.Substring(0, openBraceEnd+1).Count(b => char.Equals(b, '{'));
                }
                else
                {
                    return idIndex;
                }
            }
            else
            {
                throw new Exception("Property must be well formed.");
            }
        }*/

        protected Dictionary<string, int> GenerateNodeCount(string entityName, string property)
        {
            Dictionary<string, int> nodes = new Dictionary<string, int>();
            List<string> props = property.Split(' ').ToList();
            int count = 0;
            string parentProp = entityName;
            for (int i = 0; i < props.Count; i++)
            {
                if (string.Equals(props[i], "{", StringComparison.InvariantCultureIgnoreCase))
                {
                    parentProp = props[i - 1];
                    count = 0;
                }
                else if (!string.Equals(props[i], "}", StringComparison.InvariantCultureIgnoreCase))
                {
                    count++;
                    nodes[parentProp] = count;
                }
            }
            return nodes.Count > 0 ? nodes : null;
        }
    }
}
