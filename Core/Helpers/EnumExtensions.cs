using System;

namespace Core.Helpers
{
    public static class EnumExtensions
    {
        public static string GetValueAttr(this Enum value)
        {
            if (value == null || value.ToString() == "0")
                return string.Empty;
            var output = string.Empty;
            var type = value.GetType();
            var fi = type.GetField(value.ToString());
            if (fi == null)
                return string.Empty;
            var attrs =
               fi.GetCustomAttributes(typeof(StringValueAttribute),
                                       false) as StringValueAttribute[];
            if (attrs.Length > 0)
                output = attrs[0].Value;
            return output;
        }

        public static string GetDescriptionAttr(this Enum value)
        {
            if (value == null || value.ToString() == "0")
                return string.Empty;
            var output = string.Empty;
            var type = value.GetType();
            var fi = type.GetField(value.ToString());
            if (fi == null)
                return string.Empty;
            var attrs =
               fi.GetCustomAttributes(typeof(StringValueAttribute),
                                       false) as StringValueAttribute[];
            if (attrs.Length > 0)
                output = attrs[0].Description;
            return output;
        }
    }
}
