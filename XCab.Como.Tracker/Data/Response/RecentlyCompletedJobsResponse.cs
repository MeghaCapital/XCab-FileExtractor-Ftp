using System;
using System.Collections.Generic;
using System.Text;
using xcab.como.common.Data;

namespace xcab.como.tracker.Data.Response
{
    public class RecentlyCompletedJobsResponse
    {
        public List<RecentlyCompletedJobs> clientCodes { get; set; }
    }

	public class RecentlyCompletedJobs
	{

		public UsedByClient usedByClient { get; set; }

	}

	public class UsedByClient
	{
		public RecentlyCompletedJobsClientCode clientCode { get; set; }
		public List<Accounts> accounts { get; set; }
	}

	public class RecentlyCompletedJobsClientCode
	{
		public string accountCode { get; set; }
	}

	public class Accounts
	{
		public BusinessUnit state { get; set; }

		public List<Jobs> jobs { get; set; }

	}

	public class BusinessUnit
	{
		public string name { get; set; }
	}

	public class RecentlyCompletedJobCompletionState : IdentityDefinition
	{

	}

	public class Jobs
	{

		public RecentlyCompletedJobCompletionState completionState { get; set; }

		public RecentlyCompletedJobNumber jobNumber { get; set; }

		public List<SubJobResponse> subJobs { get; set; }

	}

	public class RecentlyCompletedJobNumber
	{
		public string number { get; set; }
	}

	public class SubJobResponse
	{
		public RecentlyCompletedJobAddress address { get; set; }

		public string eventTime { get; set; }

		//allocatedVehicleLink

		//extraInformation

		public decimal totalWeight { get; set; }

		public int totalPieces { get; set; }

		public List<SubJobLegResponse> subJobLegs { get; set; }
	}

	public class SubJobLegResponse
	{
		public RecentlyCompletedJobAddress address { get; set; }

		public List<TrackingEvents> trackingEvents { get; set; }
	}

	public class TrackingEvents
	{
		public string eventDateTimeUTC { get; set; }

		public RecentlyCompletedJobTrackingEventType trackingEventType { get; set; }
	}

	public class RecentlyCompletedJobTrackingEventType
	{
		public string trackingEventName { get; set; }
	}

	//public class RecentlyCompletedJobClient
	//{

	//}

	public class RecentlyCompletedJobAddress
	{
		public RecentlyCompletedJobAddress()
		{
			this.suburb = new RecentlyCompletedJobSuburb();
		}

		public string line1 { get; set; }

		public string line2 { get; set; }

		public RecentlyCompletedJobSuburb suburb { get; private set; }

		public string addressifyString { get; set; }

		public string name { get; set; }
	}

	public class RecentlyCompletedJobSuburb
	{
		public string displayName { get; set; }

		public decimal latitude { get; set; }

		public decimal longitude { get; set; }

	}
}
