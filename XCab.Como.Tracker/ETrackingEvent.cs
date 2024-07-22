using System;
using System.Collections.Generic;
using System.Text;

namespace xcab.como.tracker
{
    public enum ETrackingEvent
    {
        JOBBOOKED = 1,
        JOBALLOCATED = 2,
        PICKUPARRIVE = 3,
        PICKUPCOMPLETE = 4,
        DELIVERYARRIVE = 5,
        DELIVERYCOMPLETE = 6,
        JOBMODIFIED = 7,
        CANCELLED = 8
    }
}
