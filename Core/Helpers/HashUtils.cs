using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helpers
{
    public abstract class HashUtils
    {
        public static bool HashAndCheckIfSame(string value1, string value2)
        {
            bool same = false;
            if (value1.GetHashCode() == value2.GetHashCode())
            {
                same = true;
            }
            return same;
        }
    }
}
