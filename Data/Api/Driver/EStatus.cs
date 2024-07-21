using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Api.Driver
{
    public enum EStatus
    {

        /// <summary>
        /// The current Booking is Active and currently in transit
        /// </summary>
        Active = 1,
        /// <summary>
        /// Booking has been completed
        /// </summary>
        ///
        Completed = 2,
        /// <summary>
        /// The supplied Reference does not exist
        /// </summary>
        ReferenceNotFound = 3,
        /// <summary>
        /// Unspecified Error
        /// </summary>
        UnspecifiedError = 4


    }
}
