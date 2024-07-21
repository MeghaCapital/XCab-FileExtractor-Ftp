using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace Core.Helpers
{
    public class HideSensitiveDataHelper
    {
        public static string RemoveSensitiveProperties(string jsonString, IEnumerable<string> propertyNamesToHide)
        {
            JToken token = JToken.Parse(jsonString);
            RemoveSensitivePropertiesFromJsonToken(token, propertyNamesToHide);
            return token != null && !string.IsNullOrWhiteSpace(token.ToString()) ?  Regex.Replace(token.ToString(), @"\t|\n|\r|\s", " ").Replace("  ", "") : "";
        }

        private static void RemoveSensitivePropertiesFromJsonToken(JToken token, IEnumerable<string> propertyNamesToHide)
        {
            if (token.Type == JTokenType.Object)
            {
                foreach (JProperty prop in token.Children<JProperty>().ToList())
                {
                    bool removed = false;
                    foreach (var item in propertyNamesToHide)
                    {
                        if (item.ToUpper() == prop.Name.ToUpper())
                        {
                            prop.Remove();
                            removed = true;
                            break;
                        }
                    }
                    if (!removed)
                    {
                        RemoveSensitivePropertiesFromJsonToken(prop.Value, propertyNamesToHide);
                    }
                }
            }
            else if (token.Type == JTokenType.Array)
            {
                foreach (JToken child in token.Children())
                {
                    RemoveSensitivePropertiesFromJsonToken(child, propertyNamesToHide);
                }
            }
        }
    }
}
