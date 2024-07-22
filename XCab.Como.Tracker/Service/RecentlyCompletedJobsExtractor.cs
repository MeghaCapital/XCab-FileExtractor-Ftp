using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using xcab.como.common.Client;
using xcab.como.common.Data.Response;
using xcab.como.common.Logging;
using xcab.como.common.Logging.TextFileLog;
using xcab.como.common.Service;
using xcab.como.tracker.Client;
using xcab.como.tracker.Data.Response;

namespace xcab.como.tracker.Service
{
    public class RecentlyCompletedJobsExtractor : IRecentlyCompletedJobsExtractor
    {
        private static readonly IApiTokenClient apiTokenClient;
        private static readonly IInternalUserLoginClient internalUserLoginClient;
        private static readonly IRecentlyCompletedJobsClient recentlyCompletedJobsClient;
        private static readonly IComoTextFileGenerator textFileLog;
        private readonly string apiToken;

        static RecentlyCompletedJobsExtractor()
        {
            RecentlyCompletedJobsExtractor.apiTokenClient = new ApiTokenClient();
            RecentlyCompletedJobsExtractor.internalUserLoginClient = new InternalUserLoginClient();
            RecentlyCompletedJobsExtractor.recentlyCompletedJobsClient = new RecentlyCompletedJobsClient();
            RecentlyCompletedJobsExtractor.textFileLog = new ComoTextFileGenerator("C:\\Logs\\SVC\\", "XCAB_COMO-" + DateTime.Now.ToString("yyyyMMdd"));
        }

        public RecentlyCompletedJobsExtractor()
        {
            AccessTokenResponse accessTokenResponse = null;
            try
            {
                accessTokenResponse = Task.Run(async () => await RecentlyCompletedJobsExtractor.internalUserLoginClient.GetAccessTokenAsync()).Result;
            }
            catch (Exception ex)
            {
                RecentlyCompletedJobsExtractor.textFileLog.Write(GetType().Name + " - svc/xcab-como - ", "Access token: " + ex.Message, Constants.ErrorList.Error);
            }
            string accessToken = accessTokenResponse.AccessToken;
            if (!string.IsNullOrEmpty(accessToken))
            {
                ApiTokenResponse apiTokenresponse = null;
                try
                {
                    apiTokenresponse = Task.Run(async () => await RecentlyCompletedJobsExtractor.apiTokenClient.CreateApiTokenAsync(accessToken)).Result;
                }
                catch (Exception ex)
                {
                    RecentlyCompletedJobsExtractor.textFileLog.Write(GetType().Name + " - svc/xcab-como - ", "Api token: " + ex.Message, Constants.ErrorList.Error);
                }
                string apiToken = apiTokenresponse.Payload.ApiToken;
                if (!string.IsNullOrEmpty(apiToken))
                {
                    this.apiToken = apiToken;
                }
            }
        }

        public RecentlyCompletedJobs RecentlyCompletedJobs(string accountCode, string state)
        {
            var apiToken = this.apiToken;
            var recentlyCompletedJobs = new List<RecentlyCompletedJobsResponse>();
            string clientCode = accountCode.ToUpper();
            DateTime date = DateTime.Now.AddDays(-3);

            string query = @"query RecentlyCompletedJobs {
clientCodes(position: 0, limit: 0, filters: { generalSearch: " + "\"" + clientCode + "\"" + @"}) {
usedByClient {
                clientCode {
                    id
                    displayName
                }
                accounts {
                    id
                    businessUnit {
                        name
                    }
                    jobs {
                        completionState(filters: { id:[20103]}){
                            id
}
                        jobNumber {
                            displayName
                        }
                        subJobs {
                            address {
                                name
                                line1
                            line2
                            suburb {
                                    displayName
                                    name
                            latitude
                            longitude
                            }
                            }
                            externalBookingReference
                            allocatedVehicleLink {
                                allocationNumber {
                                    number
                                }
                            }
                            extraInformation
                            totalWeight
                        totalPieces
                        subJobLegs {
                                address {
                                    name
                                    line1
                                line2
                                suburb {
                                        displayName
                                        name
                                latitude
                                longitude
                                }
                                }
                                trackingEvents(filters: { eventDateTimeUTC: { gt: " + "\"" + date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'") + "\"" + @" } }, orderBy: { desc: eventDateTimeUTC}) {
                                    eventDateTimeUTC
                                    trackingEventType {
                                        displayName
                                    }
                                }
                                extraInformation
                        }
                        }
                    }
                }
            }
        }
    }";

            RecentlyCompletedJobs recentltCompletedJobs = Task.Run(async () => await RecentlyCompletedJobsExtractor.recentlyCompletedJobsClient.GetRecentlyCompletedJobsHttpAsync(apiToken, query)).Result;

            return recentltCompletedJobs;

        }
    }
}
