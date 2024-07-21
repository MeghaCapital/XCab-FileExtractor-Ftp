using Dapper;
using Data.Model.Tracking;
using Data.Model.Tracking.ILogix;
using MySql.Data.MySqlClient;
using System.Data;
using System.Globalization;

namespace Data.Api.TrackingEvents.Repository.ILogix
{
    public class ILogixTrackingStatusRepository : IILogixTrackingStatusRepository
    {
        public async Task<ICollection<TmsTrackingEvents>> GetTmsTrackingEvents(ICollection<ILogixTrackingRequest> iLogixtrackingRequests)
        {
            var tmsTrackingEvents = new List<TmsTrackingEvents>();
            using (var mySqlConnection = new MySqlConnection(DbSettings.Default.ILogixMysqlConnection))
            {
                try
                {
                    await mySqlConnection.OpenAsync();
                    var stateAbbrev = string.Empty;
                    foreach (var iLogixTrackingRequest in iLogixtrackingRequests)
                    {
                        switch (iLogixTrackingRequest.StateId)
                        {
                            case 1:
                                stateAbbrev = "M";
                                break;
                            case 2:
                                stateAbbrev = "S";
                                break;
                            case 3:
                                stateAbbrev = "B";
                                break;
                            case 4:
                                stateAbbrev = "A";
                                break;
                            case 5:
                                stateAbbrev = "P";
                                break;
                            case 7:
                                stateAbbrev = "S";
                                break;
                            default:
                                break;
                        }
                        if (stateAbbrev == string.Empty)
                            continue; //skip current iteration - should never occur
                        var subJobNumberToBeMonitored = string.Empty;
                        try
                        {
                            var dt = Convert.ToDateTime(iLogixTrackingRequest.JobAllocationDateTime, CultureInfo.GetCultureInfo("en-AU"));
                            //create iLogix date time which is based on the despatch date
                            var iLogixJobNumber = dt.ToString("ddMMyy") + stateAbbrev +
                                                  iLogixTrackingRequest.JobNumber.PadLeft(8, '0');
                            //iLogix Status Mappings
                            // Status = 7 -> Job pickup Complete                            
                            // Status = 8 -> Job Complete
                            // Status = 11 -> Job Delivered
                            //work out what ilogix table needs to be queried - archived or live table
                            var jobsTableName = "jobs";
                            var logsTableName = "log";
                            if (DateTime.Now.Subtract(dt).Days > 6)
                            {
                                jobsTableName = "jobs_" + dt.Year + "_" + dt.Month.ToString().PadLeft(2, '0');
                                logsTableName = "log_" + dt.Year + "_" + dt.Month.ToString().PadLeft(2, '0');
                                //check if we can find this job in the table - do that when not checking live table                        
                                var sqlCheckIfJobExists =
                                    "SELECT JobNumber from " + jobsTableName + " WHERE JobNumber = '" +
                                    iLogixJobNumber + "'";
                                try
                                {
                                    var dataSet = new DataSet();
                                    //setup a data adapter and read the retrieved rows
                                    using (var dataAdapter = new MySqlDataAdapter(sqlCheckIfJobExists, mySqlConnection))
                                    {
                                        dataAdapter.Fill(dataSet, "MySQLData");
                                        if (dataSet.Tables[0].Rows.Count == 0)
                                        {
                                            //this would indicate the job information could be contained in the next months table
                                            var nextMonthDt = dt.AddMonths(1);
                                            //jobs & logs table will now be for the next month
                                            jobsTableName = "jobs_" + nextMonthDt.Year + "_" +
                                                            nextMonthDt.Month.ToString().PadLeft(2, '0');
                                            logsTableName = "log_" + nextMonthDt.Year + "_" +
                                                            nextMonthDt.Month.ToString().PadLeft(2, '0');
                                            //need to validate that these tables exist
                                            var cmdStr =
                                                "SELECT COUNT(*) FROM information_schema.tables WHERE table_schema = 'ilogixdb' AND table_name = '" +
                                                jobsTableName + "'";
                                            var tableExist = true;
                                            try
                                            {
                                                using var command = new MySqlCommand(cmdStr, mySqlConnection);
                                                var mysqlreader = command.ExecuteReader();
                                                while (mysqlreader.Read())
                                                {
                                                    var count = mysqlreader.GetInt32(0);

                                                    if (count == 0)
                                                        tableExist = false;
                                                }
                                            }
                                            catch (Exception e)
                                            {                                               
                                            }
                                            if (!tableExist)
                                            {
                                                //revert to live tables
                                                jobsTableName = "jobs";
                                                logsTableName = "log";
                                            }
                                        }
                                    }
                                }
                                catch (Exception)
                                {                                    
                                }
                            }                          
                            var sql = @"
                                SELECT J.DateTimePickup, J.DateTimeDelivered, J.comments, J.SubJobNumber,L.Latitude,L.longitude, J.SubJobNumber,
                                L.StatusId
                                FROM  " + jobsTableName + @" j
                                INNER JOIN  " + logsTableName + @" L ON J.JobNumber = L.JobNumber  and L.SubJobNumber = J.SubJobNumber 
                                WHERE 
                                -- J.comments LIKE 'Arrived%' AND 
                                J.JobNumber = '" + iLogixJobNumber + @"' 
                                -- AND J.SubJobNumber ='
                                 AND l.StatusId IN( 15,7,11)
                                 order by l.logrecordid asc limit 4
                            ";
                            XcabTmsTrackingEvent xcabTmsTrackingEvent = new()
                            {
                                BookingId = iLogixTrackingRequest.XCabBookingId,    
                                StateId = iLogixTrackingRequest.StateId,
                                LoginDetails = iLogixTrackingRequest.LoginDetails,
                                AccountCode = iLogixTrackingRequest.AccountCode,
                                Ref1 = iLogixTrackingRequest.Ref1,
                                Ref2 = iLogixTrackingRequest.Ref2,
                                ConsignmentNumber = iLogixTrackingRequest.ConsignmentNumber,
                                Jobnumber = !string.IsNullOrWhiteSpace(iLogixTrackingRequest.JobNumber) ? Convert.ToInt32(iLogixTrackingRequest.JobNumber) : 0,
                                UploadDateTime = iLogixTrackingRequest.JobBookedDate,
                                AllocationDateTime = iLogixTrackingRequest.JobAllocationDateTime,
                                FromDetail1 = iLogixTrackingRequest.FromDetail1,
                                FromDetail2 = iLogixTrackingRequest.FromDetail2,
                                FromDetail3  = iLogixTrackingRequest.FromDetail3,
                                FromDetail4 = iLogixTrackingRequest.FromDetail4,
                                FromDetail5 = iLogixTrackingRequest.FromDetail5,
                                FromSuburb = iLogixTrackingRequest.FromSuburb,
                                FromPostcode = !string.IsNullOrWhiteSpace(iLogixTrackingRequest.FromPostcode) ? Convert.ToInt32(iLogixTrackingRequest.FromPostcode) : 0,
                                ToDetail1 = iLogixTrackingRequest.ToDetail1,
                                ToDetail2 = iLogixTrackingRequest.ToDetail2,
                                ToDetail3 = iLogixTrackingRequest.ToDetail3,
                                ToDetail4 = iLogixTrackingRequest.ToDetail4,
                                ToDetail5 = iLogixTrackingRequest.ToDetail5,
                                ToSuburb = iLogixTrackingRequest.ToSuburb,
                                ToPostcode = !string.IsNullOrWhiteSpace(iLogixTrackingRequest.ToPostcode) ? Convert.ToInt32(iLogixTrackingRequest.ToPostcode) : 0
                            };
                            ICollection<ILogixTrackingEvents> ilogixTrackingEvents;
                            using (mySqlConnection)
                            {
                                ilogixTrackingEvents = (ICollection<ILogixTrackingEvents>)mySqlConnection.QueryAsync<ILogixTrackingEvents>(sql).Result;
                            }
                            foreach (var ilogixTrackingEvent in ilogixTrackingEvents)
                            {
                                if (ilogixTrackingEvent.statusId == 7)
                                {
                                    //pickup arrive event
                                    if (ilogixTrackingEvent.Comments.Length > 0)
                                    {
                                        string tmpArriveDateTime = "";
                                        string dateTimeForEvent = "";
                                        if (ilogixTrackingEvent.Comments.StartsWith("Arrived"))
                                            if (ilogixTrackingEvent.Comments.Split('\r').Length > 1)
                                            {
                                                var tmp = ilogixTrackingEvent.Comments.Split('\r')[0];
                                                var getCommentParts = tmp.Split((char)32);
                                                tmpArriveDateTime = getCommentParts[2] + " " +
                                                                    getCommentParts[3] + " " +
                                                                    getCommentParts[4] + " " + getCommentParts[5];
                                            }
                                            else
                                            {
                                                // looks like >>> Arrived: 9200 30 Oct 2013 11:57 <<<
                                                var getCommentParts = ilogixTrackingEvent.Comments.Split((char)32);
                                                tmpArriveDateTime = getCommentParts[2] + " " +
                                                                    getCommentParts[3] + " " +
                                                                    getCommentParts[4] + " " + getCommentParts[5];
                                            }
                                        if (tmpArriveDateTime.Length > 0)
                                        {
                                            DateTime parsedLegArriveDateTime;
                                            // var locationText = "";
                                            DateTime.TryParse(tmpArriveDateTime, out parsedLegArriveDateTime);
                                            dateTimeForEvent = parsedLegArriveDateTime.ToString("dd MMM yyyy HH:mm");
                                            if (dateTimeForEvent.Length > 0)
                                            {
                                                if (ilogixTrackingEvent.SubJobNumber.Length > 0)
                                                {
                                                    if (ilogixTrackingEvent.SubJobNumber.Equals("01"))
                                                    {
                                                        xcabTmsTrackingEvent.PickupArriveDateTime = parsedLegArriveDateTime;
                                                        xcabTmsTrackingEvent.PickupArriveLatitude = ilogixTrackingEvent.Latitude.ToString();
                                                        xcabTmsTrackingEvent.PickupArriveLongitude = ilogixTrackingEvent.Longitude.ToString();
                                                    }
                                                    else if (ilogixTrackingEvent.SubJobNumber.Equals("02"))
                                                    {
                                                        xcabTmsTrackingEvent.DeliveryArriveDateTime = parsedLegArriveDateTime;
                                                    }
                                                    else
                                                    {
                                                        //#TODO: multileg check what we need to do here?
                                                    }
                                                }
                                            }

                                        }
                                    }
                                }
                                else if (ilogixTrackingEvent.statusId == 15)
                                {
                                    //status id of 15 can be for pickup and delivery both (arrive evemts)
                                    if (ilogixTrackingEvent.SubJobNumber == "01")
                                    {
                                        //pickup complete event
                                        if (ilogixTrackingEvent.DateTimePickup > 0)
                                        {
                                            xcabTmsTrackingEvent.PickupCompleteDateTime = DateTime.FromOADate(ilogixTrackingEvent.DateTimePickup);
                                            xcabTmsTrackingEvent.PickupCompleteLatitude = ilogixTrackingEvent.Latitude.ToString();
                                            xcabTmsTrackingEvent.PickupCompleteLongitude = ilogixTrackingEvent.Longitude.ToString();
                                        }
                                    }
                                    else if (ilogixTrackingEvent.SubJobNumber == "02")
                                    {
                                        //Delivery Arrive event
                                        if (ilogixTrackingEvent.Comments.Length > 0)
                                        {
                                            string tmpArriveDateTime = "";
                                            string dateTimeForEvent = "";
                                            if (ilogixTrackingEvent.Comments.StartsWith("Arrived"))
                                                if (ilogixTrackingEvent.Comments.Split('\r').Length > 1)
                                                {
                                                    var tmp = ilogixTrackingEvent.Comments.Split('\r')[0];
                                                    var getCommentParts = tmp.Split((char)32);
                                                    tmpArriveDateTime = getCommentParts[2] + " " +
                                                                        getCommentParts[3] + " " +
                                                                        getCommentParts[4] + " " + getCommentParts[5];
                                                }
                                                else
                                                {
                                                    // looks like >>> Arrived: 9200 30 Oct 2013 11:57 <<<
                                                    var getCommentParts = ilogixTrackingEvent.Comments.Split((char)32);
                                                    tmpArriveDateTime = getCommentParts[2] + " " +
                                                                        getCommentParts[3] + " " +
                                                                        getCommentParts[4] + " " + getCommentParts[5];
                                                }
                                            if (tmpArriveDateTime.Length > 0)
                                            {
                                                DateTime parsedLegArriveDateTime;
                                                DateTime.TryParse(tmpArriveDateTime, out parsedLegArriveDateTime);
                                                dateTimeForEvent = parsedLegArriveDateTime.ToString("dd MMM yyyy HH:mm");
                                                if (dateTimeForEvent.Length > 0)
                                                {
                                                    if (ilogixTrackingEvent.SubJobNumber.Length > 0)
                                                    {
                                                        xcabTmsTrackingEvent.DeliveryArriveDateTime = parsedLegArriveDateTime;
                                                        xcabTmsTrackingEvent.DeliveryArriveLatitude = ilogixTrackingEvent.Latitude.ToString();
                                                        xcabTmsTrackingEvent.DeliveryArriveLongitude = ilogixTrackingEvent.Longitude.ToString();
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else if (ilogixTrackingEvent.statusId == 11)
                                {
                                    if (ilogixTrackingEvent.DateTimeDelivered > 0)
                                    {
                                        //delivery complete event
                                        xcabTmsTrackingEvent.DeliveryCompleteDateTime = DateTime.FromOADate(ilogixTrackingEvent.DateTimeDelivered);
                                        xcabTmsTrackingEvent.DeliveryCompleteLatitude = ilogixTrackingEvent.Latitude.ToString();
                                        xcabTmsTrackingEvent.DeliveryCompleteLongitude = ilogixTrackingEvent.Longitude.ToString();
                                    }
                                }
                            }                           
                            if (xcabTmsTrackingEvent != null)
                            {
                                
                                tmsTrackingEvents.Add(xcabTmsTrackingEvent);
                            }
                        }
                        catch (Exception)
                        {                         
                        }
                    }

                }
                catch (Exception)
                {
                }
                finally
                {
                    if (mySqlConnection.State == ConnectionState.Open)
                    {
                        mySqlConnection.Close();
                    }
                }
            }
            return tmsTrackingEvents;
        }       
    }
}
