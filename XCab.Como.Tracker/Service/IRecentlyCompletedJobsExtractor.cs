using System;
using System.Collections.Generic;
using System.Text;
using xcab.como.tracker.Data.Response;

namespace xcab.como.tracker.Service
{
    public interface IRecentlyCompletedJobsExtractor
    {
        RecentlyCompletedJobs RecentlyCompletedJobs(string accountCode, string state);
    }
}
