using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using xcab.como.tracker.Data.Response;

namespace xcab.como.tracker.Client
{
    public interface IRecentlyCompletedJobsClient
    {
        //RecentlyCompletedJobsResponse GetRecentlyCompletedJobsRest(IRestClient restClient, IRestRequest restRequest, string apiToken, string query, List<int> ids);

        //Task<RecentlyCompletedJobsResponse> GetRecentlyCompletedJobsAsync(IRestClient restClient, IRestRequest restRequest, string apiToken, string query, List<int> ids);

        //RecentlyCompletedJobsResponse GetRecentlyCompletedJobsHttp(string apiToken, string query, List<int> ids);

        Task<RecentlyCompletedJobs> GetRecentlyCompletedJobsHttpAsync(string apiToken, string query);

        //  ICollection<Job> GetRecentCompletedJobs(string accountCode, int state);
    }
}
