using Core.Helpers;
using Dapper;
using Data.Entities.Tplus;
using Data.Repository.EntityRepositories.Interfaces;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repository.EntityRepositories
{
    public class TplusJobRepository : ITplusJobRepository
    {
        public List<TplusJobEntity> GetJobsFromState(TplusConnectionString tpcs, List<string> clientCodes)

        {
            throw new NotImplementedException("Not Implemented");
        }

        public List<TplusMultiLegModel> GetJobsFromState(TplusConnectionString tpcs, List<string> clientCodes,
            DateTime bookingDate, int LoginId, bool ignorePermanentBookings)

        {
            var output = new List<TplusMultiLegModel>();
            var stateId = -1;
            switch (tpcs)
            {
                case TplusConnectionString.Adelaide:
                    stateId = 4;
                    break;
                case TplusConnectionString.Melbourne:
                    stateId = 1;
                    break;
                case TplusConnectionString.Sydney:
                    stateId = 2;
                    break;
                case TplusConnectionString.Brisbane:
                    stateId = 3;
                    break;
                case TplusConnectionString.Perth:
                    stateId = 5;
                    break;
                case TplusConnectionString.NotSet:
                    stateId = -1;
                    break;
            }
            if (stateId == -1)
                return output;
            using (var conn = new OracleConnection(tpcs.GetConnectionString()))
            {
                conn.Open();
                var p = new OracleDynamicParameters();
                // p.Add("dateMinusFortyFiveDays", DateTime.Today.AddDays(-45));

                var query =
                    $@"
                SELECT  j.JOB_CLIENT_CODE AS ""ClientCode"",
                        j.JOB_BOOKING_DAY AS ""JobDate"",
                        j.JOB_NUMBER AS ""JobNumber"",  
                        {stateId} As ""StateId"",
                        {LoginId} As ""LoginId""
                FROM job j
                INNER JOIN audit_events ae
                    ON  j.job_number      = ae.ae_event_jobnum --Link Job Number between tables
                    AND j.job_booking_day = ae.ae_event_jobbday --Link Job Date between tables
                WHERE   ae.AE_EVENT_TYPE = 'BK'
                    {DapperHelper.GenerateWhereListForOracle(clientCodes, "j.JOB_CLIENT_CODE", false)}
                    AND j.JOB_BOOKING_FLAG IS NOT NULL
                    AND j.JOB_BOOKING_FLAG <> ' '
                    AND j.JOB_WAS_CANCELLED = 'N'
                    AND j.JOB_BOOKING_DAY >=  '{bookingDate:dd-MMM-yy}'
                   
                ";
                if (ignorePermanentBookings)
                    query += " AND j.JOB_BOOKING_USER_NAME <> 'PermBook'";
                output = conn.Query<TplusMultiLegModel>(query, p).ToList();
            }

            return output;
        }

        public List<TplusMultiLegModel> GetJobsFromStateForXcab(TplusConnectionString tpcs, List<TplusMultiLegModel> Jobs,
            DateTime bookingDate)
        {
            var output = new List<TplusMultiLegModel>();

            using (var conn = new OracleConnection(tpcs.GetConnectionString()))
            {
                conn.Open();
                var p = new OracleDynamicParameters();
                foreach (var job in Jobs)
                {
                    var query =
    $@"
                SELECT  j.JOB_CLIENT_CODE AS ""ClientCode"",
                        j.JOB_BOOKING_DAY AS ""JobDate"",
                        j.JOB_NUMBER AS ""JobNumber"",
                        j.Job_client_Ref As Ref1,
                        j.Job_client_Ref_2 As Ref2,
                        j.job_pickup_from1 As FromDetail1,
                        j.job_pickup_from2 As FromDetail2,
                        j.job_pickup_from3 As FromDetail3,
                        j.job_pickup_from4 As FromDetail4,
                        j.job_pickup_from5 As FromDetail5,
                        j.job_deliver_To1 As ToDetail1,
                        j.job_deliver_To2 As ToDetail2,
                        j.job_deliver_To3 As ToDetail3,
                        j.job_deliver_To4 As ToDetail4,
                        j.job_deliver_To3 As ToDetail5,
                        j.job_eta_override As DeliveryEta,
                        j.JOB_BASEJOB_NUM As BaseJobNumber, 
                         j.JOB_TOTAL_LEGS As TotalLegs,
                         j.JOB_LEG_NUMBER As LegNumber, 
                        j.job_driver As Driver,
                        s.sub_name As ToSuburb     ,
                        s.sub_postcode As ToPostcode,
                        s2.sub_name AS FromSuburb,
                        s2.sub_postcode As FromPostcode,
                        ch.ch_abbrev As ServiceCode,
                        {job.LoginId} As LoginId,
                        J.JOB_ADVANCE_TIME As AdvanceDateTime,
                        J.JOB_ADVANCE_TIME As DespatchDateTime
                FROM job j
                INNER JOIN audit_events ae
                    ON  j.job_number      = ae.ae_event_jobnum --Link Job Number between tables
                    AND j.job_booking_day = ae.ae_event_jobbday --Link Job Date between tables
                LEFT JOIN suburb s
                    ON j.job_deliver_suburb = s.sub_abbrev_code
                LEFT JOIN suburb s2
                    ON j.job_pickup_suburb = s2.sub_abbrev_code
                LEFT JOIN service_events se
                       ON j.job_number = se.se_jobnum
                          AND  j.job_booking_day = se.se_jobbday
                          AND se.se_type = 0
               LEFT join charges ch
                       ON ch.ch_abbrev_num = se.se_service_code
                          AND ch.ch_charge_object = se.se_chgobj_num
                          AND ch.ch_charge_type_ind = 'C'
                WHERE   ae.AE_EVENT_TYPE = 'BK'
                   AND j.JOB_NUMBER ='{job.JobNumber}'
                    AND j.JOB_BOOKING_FLAG IS NOT NULL
                    AND j.JOB_BOOKING_FLAG <> ' '
                    AND j.JOB_WAS_CANCELLED = 'N'
                    AND j.JOB_BOOKING_DAY >=  '{bookingDate:dd-MMM-yy}'     
                                       
                ";

                    var result = conn.Query<TplusMultiLegModel>(query);
                    if (result.Any())
                        output.AddRange(result);
                }


                //output = conn.Query<TplusJobEntity>(query, p).ToList();
            }

            return output;
        }

        public List<TplusMultiLegModel> GetMultiLegJobsFromStateForXcab(TplusConnectionString tpcs, List<TplusMultiLegModel> Jobs,
           DateTime bookingDate)
        {
            var output = new List<TplusMultiLegModel>();

            using (var conn = new OracleConnection(tpcs.GetConnectionString()))
            {
                conn.Open();
                var p = new OracleDynamicParameters();
                foreach (var job in Jobs)
                {
                    var query =
    $@"
                SELECT  j.JOB_CLIENT_CODE AS ""ClientCode"",
                        j.JOB_BOOKING_DAY AS ""JobDate"",
                        j.JOB_NUMBER AS ""JobNumber"",
                        j.Job_client_Ref As Ref1,
                        j.Job_client_Ref_2 As Ref2,
                        j.job_pickup_from1 As FromDetail1,
                        j.job_pickup_from2 As FromDetail2,
                        j.job_pickup_from3 As FromDetail3,
                        j.job_pickup_from4 As FromDetail4,
                        j.job_pickup_from5 As FromDetail5,
                        j.job_deliver_To1 As ToDetail1,
                        j.job_deliver_To2 As ToDetail2,
                        j.job_deliver_To3 As ToDetail3,
                        j.job_deliver_To4 As ToDetail4,
                        j.job_deliver_To3 As ToDetail5,
                        j.job_eta_override As DeliveryEta,
                        s.sub_postcode As ToPostcode,
                        s2.sub_postcode As FromPostcode,
                        ch.ch_abbrev As ServiceCode,
                        {job.LoginId} As LoginId,

                         j.JOB_BASEJOB_NUM As BaseJobNumber, 
                         j.JOB_TOTAL_LEGS As TotalLegs,
                         j.JOB_LEG_NUMBER As LegNumber, 
                         
                         j.job_driver As Driver,
                         s.sub_name As SuburbName
                FROM job j
                INNER JOIN suburb s
                    ON j.job_deliver_suburb = s.sub_abbrev_code
                FROM job j
                INNER JOIN audit_events ae
                    ON  j.job_number      = ae.ae_event_jobnum --Link Job Number between tables
                    AND j.job_booking_day = ae.ae_event_jobbday --Link Job Date between tables
                LEFT JOIN suburb s
                    ON j.job_deliver_suburb = s.sub_abbrev_code               
                LEFT JOIN service_events se
                       ON j.job_number = se.se_jobnum
                          AND  j.job_booking_day = se.se_jobbday
                          AND se.se_type = 0
               LEFT join charges ch
                       ON ch.ch_abbrev_num = se.se_service_code
                          AND ch.ch_charge_object = se.se_chgobj_num
                          AND ch.ch_charge_type_ind = 'C'
                WHERE   ae.AE_EVENT_TYPE = 'BK'
                   AND j.JOB_NUMBER ='{job.JobNumber}'
                    AND j.JOB_BOOKING_FLAG IS NOT NULL
                    AND j.JOB_BOOKING_FLAG <> ' '
                    AND j.JOB_WAS_CANCELLED = 'N'
                    AND j.JOB_BOOKING_DAY >=  '{bookingDate:dd-MMM-yy}'  
                    and j.job_number <> j.JOB_BASEJOB_NUM 
                                       
                ";

                    var result = conn.Query<TplusMultiLegModel>(query);
                    if (result.Any())
                        output.AddRange(result);
                }


                //output = conn.Query<TplusJobEntity>(query, p).ToList();
            }

            return output;
        }

        public TplusMultiLegModel GetDeliveryEta(TplusConnectionString tpcs, string jobNumber, DateTime bookingDate)
        {
            var tplusMultiLegModel = new TplusMultiLegModel();
            using (var conn = new OracleConnection(tpcs.GetConnectionString()))
            {
                conn.Open();
                var p = new OracleDynamicParameters();

                var query =
                    $@"
                SELECT  
                        j.job_eta_override As DeliveryEta
                       
                FROM job j
               
                INNER JOIN audit_events ae
                    ON  j.job_number      = ae.ae_event_jobnum --Link Job Number between tables
                    AND j.job_booking_day = ae.ae_event_jobbday --Link Job Date between tables
                
                LEFT JOIN service_events se
                       ON j.job_number = se.se_jobnum
                          AND  j.job_booking_day = se.se_jobbday
                          AND se.se_type = 0
               LEFT join charges ch
                       ON ch.ch_abbrev_num = se.se_service_code
                          AND ch.ch_charge_object = se.se_chgobj_num
                          AND ch.ch_charge_type_ind = 'C'
                WHERE   ae.AE_EVENT_TYPE = 'BK'
                   AND j.JOB_NUMBER ='{jobNumber}'
                    AND j.JOB_BOOKING_FLAG IS NOT NULL
                    AND j.JOB_BOOKING_FLAG <> ' '
                    AND j.JOB_WAS_CANCELLED = 'N'
                    AND j.JOB_BOOKING_DAY >=  '{bookingDate:dd-MMM-yy}'  
                    and j.job_number <> j.JOB_BASEJOB_NUM 
                                       
                ";

                var result = conn.Query<TplusMultiLegModel>(query);
                if (result.Any())
                    tplusMultiLegModel = result.FirstOrDefault();
            }
            return tplusMultiLegModel;

        }

        public List<TplusMultiLegModel> GetMultiLegJobsFromState(TplusConnectionString tpcs, List<string> clientCodes, DateTime bookingDate, int LoginId,
            bool ignorePermanentBookings)
        {
            //bookingDate = bookingDate.AddDays(-1);
            var output = new List<TplusMultiLegModel>();
            var stateId = -1;
            switch (tpcs)
            {
                case TplusConnectionString.Adelaide:
                    stateId = 4;
                    break;
                case TplusConnectionString.Melbourne:
                    stateId = 1;
                    break;
                case TplusConnectionString.Sydney:
                    stateId = 2;
                    break;
                case TplusConnectionString.Brisbane:
                    stateId = 3;
                    break;
                case TplusConnectionString.Perth:
                    stateId = 5;
                    break;
                case TplusConnectionString.NotSet:
                    stateId = -1;
                    break;
            }
            if (stateId == -1)
                return output;
            using (var conn = new OracleConnection(tpcs.GetConnectionString()))
            {
                conn.Open();
                var p = new OracleDynamicParameters();
                // p.Add("dateMinusFortyFiveDays", DateTime.Today.AddDays(-45));
                //only retrive jobs that have not been created through XCAB i.e. the caller field != XCAB in TPLUS
                //In XCAB DB clients using the API can populate this field (Caller), however if this not provided Job Creator will create jobs in TPLUS with Caller = XCAB
                var query =
                    $@"
                SELECT  j.JOB_CLIENT_CODE AS ""ClientCode"",
                        j.JOB_BOOKING_DAY AS ""JobDate"",
                        j.JOB_NUMBER AS ""JobNumber"",  
                        {stateId} As ""StateId"",
                        {LoginId} As ""LoginId"",
                         j.JOB_BASEJOB_NUM As BaseJobNumber, 
                         j.JOB_TOTAL_LEGS As TotalLegs,
                         j.JOB_LEG_NUMBER As LegNumber, 
                         j.JOB_NEXT_LEG,
                         j.JOB_PREV_LEG,
                         j.job_client_ref As Ref1,
                         j.job_client_ref_2 As Ref2,
                         j.job_driver As Driver,
                         s.sub_name As SuburbName,
                         j.JOB_BOOKING_USER_NAME AS ""UserName""
                FROM job j
                INNER JOIN suburb s
                    ON j.job_deliver_suburb = s.sub_abbrev_code
                INNER JOIN audit_events ae
                    ON  j.job_number      = ae.ae_event_jobnum --Link Job Number between tables
                    AND j.job_booking_day = ae.ae_event_jobbday --Link Job Date between tables               
                WHERE   ae.AE_EVENT_TYPE = 'BK'
                    {DapperHelper.GenerateWhereListForOracle(clientCodes, "j.JOB_CLIENT_CODE", false)}
                    AND j.JOB_BOOKING_FLAG IS NOT NULL
                    AND j.JOB_BOOKING_FLAG <> ' '
                    AND j.JOB_WAS_CANCELLED = 'N'
                    AND j.JOB_BOOKING_DAY >=  '{bookingDate:dd-MMM-yy}'
                    AND UPPER(J.JOB_CALLERS_NAME) <> 'XCAB' 
--                  and j.job_number <> j.JOB_BASEJOB_NUM
                ";
                if (ignorePermanentBookings)
                    query += " AND j.JOB_BOOKING_USER_NAME <> 'PermBook'";
                output = conn.Query<TplusMultiLegModel>(query, p).ToList();
            }

            return output;
        }
    }
}
