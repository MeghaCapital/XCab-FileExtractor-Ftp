using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Api.Driver
{
    public class JobReferences
    {
        /// <summary>
        /// Account Code for the Booking
        /// </summary>
        public string AccountCode { get; set; }
        /// <summary>
        /// List of Booking References
        /// </summary>
        public ICollection<string> References { get; set; }

        public override string ToString()
        {
            var returnStr = "AccountCode:" + AccountCode + " References: ";
            return References.Aggregate(returnStr, (current, reference) => current + " " + reference);
        }
    }
}
