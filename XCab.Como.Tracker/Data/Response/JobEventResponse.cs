using System;
using System.Collections.Generic;
using System.Text;
using xcab.como.common.Data;

namespace xcab.como.tracker.Data.Response
{
    public class JobEventResponse
    {
        public List<JobEvent> clientCodes { get; set; }
    }

    public class JobEvent
    { 
        public JobEventClient usedByClient { get; set; }
    }

    public class JobEventClient
    { 
        public List<JobEventAccount> accounts { get; set; }
    }

    public class JobEventAccount
    {
        public JobEventBusinessUnit businessUnit { get; set; }
        public List<JobEventJob> jobs { get; set; }
    }

    public class JobEventBusinessUnit
    { 
        public string name { get; set; }
    }

    public class JobEventJob
    { 
        public JobEventCompletionState completionState { get; set; }

        public List<JobEventSubJob> subJobs { get; set; }
    }

    public class JobEventCompletionState
    { 
        
    }

    public class JobEventSubJob
    { 
        public JobEventDespatchStatus currentDespatchStatus { get; set; }

        public JobEventAllocationLink allocatedVehicleLink { get; set; }
    }

    public class JobEventDespatchStatus
    { 
        public string name { get; set; }
    }

    public class JobEventAllocationLink
    { 
        public JobEventAllocationNumber allocationNumber { get; set; }
    }

    public class JobEventAllocationNumber
    { 
        public int number { get; set; }
    }
}
