using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace xcab.como.common.Struct
{
    public static class JobEndec
    {
        private static readonly int _separationIndex = 2;

        public static long Decode(string number, EEncoding encoding)
        {
            if (encoding == EEncoding.BASE26)
            {
                string num = number.ToLower();
                ICollection<int> array = new List<int>();
                for (int i = 0; i < _separationIndex; i++)
                {
                    if (Regex.IsMatch(num[i].ToString(), "[a-z]", RegexOptions.IgnoreCase))
                    {
                        array.Add(num[i] - 96);
                    }
                }
                if (array.Count == 0)
                {
                    return 0;
                }
                return Convert.ToInt64(Convert.ToString(array.Aggregate((x, y) => 26 * x + y)) + number.Substring(_separationIndex));
            }
            return -1;
        }

        public static string Encode(long input, EEncoding encoding)
        {
            if (encoding == EEncoding.BASE26)
            {
                var array = new LinkedList<long>();
                long number = Convert.ToInt64(Convert.ToString(input).PadLeft(9, '0').Substring(0, _separationIndex + 1));
                while (number > 26)
                {
                    long value = number % 26;
                    if (value == 0)
                    {
                        number = number / 26 - 1;
                        array.AddFirst(26);
                    }
                    else if (value > 0)
                    {
                        number /= 26;
                        array.AddFirst(value);
                    }
                }
                if (number > 0)
                {
                    array.AddFirst(number);
                }
                return (new string(array.Select(s => (char)('A' + s - 1)).ToArray())) + Convert.ToString(input).PadLeft(9, '0').Substring(_separationIndex + 1);
            }
            return null;
        }
    }
}
