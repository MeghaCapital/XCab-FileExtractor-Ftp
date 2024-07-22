using System;
using System.Collections.Generic;
using System.Text;
using xcab.como.common.Data;

namespace xcab.como.booker.Data.Variable
{
    public class Job
    {
        public Job(int accountId, int bookingPhaseId, int subJobCreationId, string caller, IEnumerable<SubJob> subJobs)
        {
            this.subJobCreationMethod = new SubJobCreation();
            this.jobForAccount = new AccountWithOnlyId();
            this.currentJobBookingPhase = new BookingPhase();
            this.subJobs = new List<SubJob>();
            if (subJobs != null)
            {
                this.subJobs.AddRange(subJobs);
            }
            this.jobForAccount.id = accountId;
            this.currentJobBookingPhase.id = bookingPhaseId;
            this.subJobCreationMethod.id = subJobCreationId;
            this.caller = caller;
        }

        public Job(int accountId, int bookingPhaseId, int subJobCreationId, string caller, int jobNumberId, IEnumerable<SubJob> subJobs)
        : this(accountId, bookingPhaseId, subJobCreationId, caller, subJobs)
        {
            this.jobNumber = new JobNumber();
            this.jobNumber.id = jobNumberId;
        }

        public Job()
        {

        }

        public AccountWithOnlyId jobForAccount { get; private set; }

        public BookingPhase currentJobBookingPhase { get; private set; }

        public string caller { get; private set; }

        public List<SubJob> subJobs { get; private set; }

        public SubJobCreation subJobCreationMethod { get; private set; }

        public virtual JobNumber jobNumber { get; private set; }

        public bool ShouldSerializejobNumber()
        {
            return (this.jobNumber != null);
        }
    }

    public class AccountWithOnlyId : IdentityDefinition
    {

    }

    public class BookingPhase : IdentityDefinition
    {

    }

    public class SubJobCreation : IdentityDefinition
    {

    }

    public class JobNumber : IdentityDefinition
    {
        public string DisplayName { get; set; }
    }
}
