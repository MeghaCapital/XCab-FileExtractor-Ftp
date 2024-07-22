using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Edifact_Library;

namespace XCabBookingFileExtractor.Utils.Consolidation
{
    public static class JobConsolidation
    {

        public static ConsolidatedJob ByToDetail2(ICollection<Booking> lstBooking)
        {
        
            var consolidatedJob = new ConsolidatedJob();
            
            var consolidatedBookings =
                
                lstBooking.GroupBy(x => new {x.ToDetail1, x.ToDetail2}).Select(
                x => new Booking
                {
                    AccountCode = x.ToList()[0].AccountCode,
                    Ref1 = x.Count()==1? x.ToList()[0].Ref1: x.Count().ToString() + " Invoices",
                    Ref2 = x.Count()==1? x.ToList()[0].Ref2: x.ToList()[0].ToDetail1.Replace('/', ' '), //store customer name as reference
                    Caller = x.ToList()[0].Caller,
                    FromDetail1 = x.ToList()[0].FromDetail1,
                    FromDetail2 = x.ToList()[0].FromDetail2,
                    FromDetail3 = x.ToList()[0].FromDetail3 == null ? "" : x.ToList()[0].FromDetail3,
                    FromDetail4 = x.ToList()[0].FromDetail4 == null ? "" : x.ToList()[0].FromDetail4,
                    FromDetail5 = x.ToList()[0].FromDetail5 == null ? "" : x.ToList()[0].FromDetail5,
                    FromSuburb = x.ToList()[0].FromSuburb,
                    FromPostcode = x.ToList()[0].FromPostcode,
                    ToDetail1 = x.ToList()[0].ToDetail1,
                    ToDetail2 = x.ToList()[0].ToDetail2,
                    ToDetail3 = x.ToList()[0].ToDetail3 == null ? "" : x.ToList()[0].ToDetail3,
                    ToDetail4 = x.ToList()[0].ToDetail4 == null ? "" : x.ToList()[0].ToDetail4,
                    ToDetail5 = x.ToList()[0].ToDetail5 == null ? "" : x.ToList()[0].ToDetail5,
                    ToSuburb = x.ToList()[0].ToSuburb,
                    ToPostcode = x.ToList()[0].ToPostcode,
                    StateId = x.ToList()[0].StateId,
                    ServiceCode = x.ToList()[0].ServiceCode,
                    PreAllocatedDriverNumber = x.ToList()[0].PreAllocatedDriverNumber,
                    DriverNumber = x.ToList()[0].DriverNumber,
                    DespatchDateTime = x.ToList()[0].DespatchDateTime,
                    AdvanceDateTime = x.ToList()[0].AdvanceDateTime,
                    ExtraPuInformation = x.ToList()[0].ExtraPuInformation == null ? "" : x.ToList()[0].ExtraPuInformation,
                    ExtraDelInformation= x.ToList()[0].ExtraDelInformation == null ? "" : x.ToList()[0].ExtraDelInformation,
                    OkToUpload = x.ToList()[0].OkToUpload,
                    LoginDetails = new LoginDetails
                    {
                        Id = x.ToList()[0].LoginDetails.Id
                    },
                    UploadDateTime = x.ToList()[0].UploadDateTime,
                    Notification = x.ToList()[0].Notification,
                    
                    TotalItems =  x.Sum(c => c.lstItems.Count).ToString(),
                    TotalWeight = x.Sum(c => Convert.ToDouble(c.TotalWeight)).ToString(),
                    TotalVolume = x.Sum(c => Convert.ToDouble(c.TotalVolume)).ToString(),
                    
                    lstItems = x.SelectMany(y => y.lstItems).ToList(),
                });

            var _groupedBookings = lstBooking.GroupBy(x => x.ToDetail2);


            var consolidatedJobsList = consolidatedBookings.ToList();

            lstBooking = consolidatedJobsList;
            consolidatedJob.ConsolidatedBookings = (List<Booking>) lstBooking;
            consolidatedJob.GroupedJobs = _groupedBookings;

            return consolidatedJob;
        }

        public static ConsolidatedJob ByToDetail2WithContactDetails(ICollection<Booking> lstBooking)
        {

            var consolidatedJob = new ConsolidatedJob();
             
            var consolidatedBookings =

                lstBooking.GroupBy(x => new { x.ToDetail1, x.ToDetail2 }).Select(
                x => new Booking
                {
                    AccountCode = x.ToList()[0].AccountCode,
                    Ref1 = x.Count() == 1 ? x.ToList()[0].Ref1 : x.Count().ToString() + " Invoices",
                    Ref2 = x.Count() == 1 ? x.ToList()[0].Ref2 : x.ToList()[0].ToDetail1.Replace('/', ' '), //store customer name as reference
                    Caller = x.ToList()[0].Caller,
                    FromDetail1 = x.ToList()[0].FromDetail1,
                    FromDetail2 = x.ToList()[0].FromDetail2,
                    FromDetail3 = x.ToList()[0].FromDetail3 == null ? "" : x.ToList()[0].FromDetail3,
                    FromDetail4 = x.ToList()[0].FromDetail4 == null ? "" : x.ToList()[0].FromDetail4,
                    FromDetail5 = x.ToList()[0].FromDetail5 == null ? "" : x.ToList()[0].FromDetail5,
                    FromSuburb = x.ToList()[0].FromSuburb,
                    FromPostcode = x.ToList()[0].FromPostcode,
                    ToDetail1 = x.ToList()[0].ToDetail1,
                    ToDetail2 = x.ToList()[0].ToDetail2,
                    ToDetail3 = x.ToList()[0].ToDetail3 == null ? "" : x.ToList()[0].ToDetail3,
                    ToDetail4 = x.ToList()[0].ToDetail4 == null ? "" : x.ToList()[0].ToDetail4,
                    ToDetail5 = x.ToList()[0].ToDetail5 == null ? "" : x.ToList()[0].ToDetail5,
                    ToSuburb = x.ToList()[0].ToSuburb,
                    ToPostcode = x.ToList()[0].ToPostcode,
                    StateId = x.ToList()[0].StateId,
                    ServiceCode = x.ToList()[0].ServiceCode,
                    PreAllocatedDriverNumber = x.ToList()[0].PreAllocatedDriverNumber,
                    DriverNumber = x.ToList()[0].DriverNumber,
                    DespatchDateTime = x.ToList()[0].DespatchDateTime,
                    AdvanceDateTime = x.ToList()[0].AdvanceDateTime,
                    ExtraPuInformation = x.ToList()[0].ExtraPuInformation == null ? "" : x.ToList()[0].ExtraPuInformation,
                    ExtraDelInformation = x.ToList()[0].ExtraDelInformation == null ? "" : x.ToList()[0].ExtraDelInformation,
                    LoginDetails = new LoginDetails
                    {
                        Id = x.ToList()[0].LoginDetails.Id
                    },
                    UploadDateTime = x.ToList()[0].UploadDateTime,

                    TotalItems = x.Sum(c => c.lstItems.Count).ToString(),
                    TotalWeight = x.Sum(c => Convert.ToDouble(c.TotalWeight)).ToString(),
                    TotalVolume = x.Sum(c => Convert.ToDouble(c.TotalVolume)).ToString(),

                    lstItems = x.SelectMany(y => y.lstItems).ToList(),
                    lstContactDetail = x.SelectMany(p=> p.lstContactDetail).ToList()
                });

            var _groupedBookings = lstBooking.GroupBy(x => x.ToDetail2);


            var consolidatedJobsList = consolidatedBookings.ToList();

            lstBooking = consolidatedJobsList;
            consolidatedJob.ConsolidatedBookings = (List<Booking>)lstBooking;
            consolidatedJob.GroupedJobs = _groupedBookings;

            return consolidatedJob;
        }

        public static ConsolidatedJob ByToDetail2NoItemsPresent(ICollection<Booking> lstBooking)
        {

            var consolidatedJob = new ConsolidatedJob();

            var consolidatedBookings =

                lstBooking.GroupBy(x => new { x.ToDetail1, x.ToDetail2 }).Select(
                x => new Booking
                {
                    AccountCode = x.ToList()[0].AccountCode,
                    Ref1 = x.Count() == 1 ? x.ToList()[0].Ref1 : x.Count().ToString() + " Invoices",
                    Ref2 = x.Count() == 1 ? x.ToList()[0].Ref2 : x.ToList()[0].ToDetail1.Replace('/', ' '), //store customer name as reference
                    Caller = x.ToList()[0].Caller,
                    FromDetail1 = x.ToList()[0].FromDetail1,
                    FromDetail2 = x.ToList()[0].FromDetail2,
                    FromDetail3 = x.ToList()[0].FromDetail3 == null ? "" : x.ToList()[0].FromDetail3,
                    FromDetail4 = x.ToList()[0].FromDetail4 == null ? "" : x.ToList()[0].FromDetail4,
                    FromDetail5 = x.ToList()[0].FromDetail5 == null ? "" : x.ToList()[0].FromDetail5,
                    FromSuburb = x.ToList()[0].FromSuburb,
                    FromPostcode = x.ToList()[0].FromPostcode,
                    ToDetail1 = x.ToList()[0].ToDetail1,
                    ToDetail2 = x.ToList()[0].ToDetail2,
                    ToDetail3 = x.ToList()[0].ToDetail3 == null ? "" : x.ToList()[0].ToDetail3,
                    ToDetail4 = x.ToList()[0].ToDetail4 == null ? "" : x.ToList()[0].ToDetail4,
                    ToDetail5 = x.ToList()[0].ToDetail5 == null ? "" : x.ToList()[0].ToDetail5,
                    ToSuburb = x.ToList()[0].ToSuburb,
                    ToPostcode = x.ToList()[0].ToPostcode,
                    StateId = x.ToList()[0].StateId,
                    ServiceCode = x.ToList()[0].ServiceCode,
                    PreAllocatedDriverNumber = x.ToList()[0].PreAllocatedDriverNumber,
                    DriverNumber = x.ToList()[0].DriverNumber,
                    DespatchDateTime = x.ToList()[0].DespatchDateTime,
                    AdvanceDateTime = x.ToList()[0].AdvanceDateTime,
                    LoginDetails = new LoginDetails
                    {
                        Id = x.ToList()[0].LoginDetails.Id
                    },
                    UploadDateTime = x.ToList()[0].UploadDateTime,
                   
                });

            var _groupedBookings = lstBooking.GroupBy(x => x.ToDetail2);


            var consolidatedJobsList = consolidatedBookings.ToList();

            lstBooking = consolidatedJobsList;
            consolidatedJob.ConsolidatedBookings = (List<Booking>)lstBooking;
            consolidatedJob.GroupedJobs = _groupedBookings;

            return consolidatedJob;
        }

        public static ConsolidatedJob ByToDetail2WithRefValues(ICollection<Booking> lstBooking)
        {

            var consolidatedJob = new ConsolidatedJob();

            var consolidatedBookings =

                lstBooking.GroupBy(x => new { x.ToDetail1, x.ToDetail2 }).Select(
                x => new Booking
                {
                    AccountCode = x.ToList()[0].AccountCode,
                    Ref1 = x.ToList()[0].Ref1,
                    Ref2 = x.ToList()[0].Ref2, //store customer name as reference
                    Caller = x.ToList()[0].Caller,
                    FromDetail1 = x.ToList()[0].FromDetail1,
                    FromDetail2 = x.ToList()[0].FromDetail2,
                    FromDetail3 = x.ToList()[0].FromDetail3 == null ? "" : x.ToList()[0].FromDetail3,
                    FromDetail4 = x.ToList()[0].FromDetail4 == null ? "" : x.ToList()[0].FromDetail4,
                    FromDetail5 = x.ToList()[0].FromDetail5 == null ? "" : x.ToList()[0].FromDetail5,
                    FromSuburb = x.ToList()[0].FromSuburb,
                    FromPostcode = x.ToList()[0].FromPostcode,
                    ToDetail1 = x.ToList()[0].ToDetail1,
                    ToDetail2 = x.ToList()[0].ToDetail2,
                    ToDetail3 = x.ToList()[0].ToDetail3 == null ? "" : x.ToList()[0].ToDetail3,
                    ToDetail4 = x.ToList()[0].ToDetail4 == null ? "" : x.ToList()[0].ToDetail4,
                    ToDetail5 = x.ToList()[0].ToDetail5 == null ? "" : x.ToList()[0].ToDetail5,
                    ToSuburb = x.ToList()[0].ToSuburb,
                    ToPostcode = x.ToList()[0].ToPostcode,
                    StateId = x.ToList()[0].StateId,
                    ServiceCode = x.ToList()[0].ServiceCode,
                    PreAllocatedDriverNumber = x.ToList()[0].PreAllocatedDriverNumber,
                    DriverNumber = x.ToList()[0].DriverNumber,
                    DespatchDateTime = x.ToList()[0].DespatchDateTime,
                    AdvanceDateTime = x.ToList()[0].AdvanceDateTime,
                    ExtraPuInformation = x.ToList()[0].ExtraPuInformation == null ? "" : x.ToList()[0].ExtraPuInformation,
                    ExtraDelInformation = x.ToList()[0].ExtraDelInformation == null ? "" : x.ToList()[0].ExtraDelInformation,
                    LoginDetails = new LoginDetails
                    {
                        Id = x.ToList()[0].LoginDetails.Id
                    },
                    UploadDateTime = x.ToList()[0].UploadDateTime,
                    
                    TotalItems = x.Sum(c => Convert.ToDouble(c.TotalItems)).ToString(),
                    TotalWeight = x.Sum(c => Convert.ToDouble(c.TotalWeight)).ToString(),
                    TotalVolume = x.Sum(c => Convert.ToDouble(c.TotalVolume)).ToString(),

                    lstItems = x.SelectMany(y => y.lstItems).ToList(),
                    DeliveryTimeSlot = x.ToList()[0].DeliveryTimeSlot
                });

            var _groupedBookings = lstBooking.GroupBy(x => x.ToDetail2);


            var consolidatedJobsList = consolidatedBookings.ToList();

            lstBooking = consolidatedJobsList;
            consolidatedJob.ConsolidatedBookings = (List<Booking>)lstBooking;
            consolidatedJob.GroupedJobs = _groupedBookings;

            return consolidatedJob;
        }

        public static ConsolidatedJob ByFromDetail2(ICollection<Booking> lstBooking)
        {

            var consolidatedJob = new ConsolidatedJob();

            var consolidatedBookings =

                lstBooking.GroupBy(x => new { x.FromDetail1, x.FromDetail2 }).Select(
                x => new Booking
                {
                    AccountCode = x.ToList()[0].AccountCode,
                    Ref1 = x.Count() == 1 ? x.ToList()[0].Ref1 : x.Count().ToString() + " Invoices",
                    Ref2 = x.Count() == 1 ? x.ToList()[0].Ref2 : x.ToList()[0].FromDetail1.Replace('/', ' '), //store customer name as reference
                    Caller = x.ToList()[0].Caller,
                    FromDetail1 = x.ToList()[0].FromDetail1,
                    FromDetail2 = x.ToList()[0].FromDetail2,
                    FromDetail3 = x.ToList()[0].FromDetail3 == null ? "" : x.ToList()[0].FromDetail3,
                    FromDetail4 = x.ToList()[0].FromDetail4 == null ? "" : x.ToList()[0].FromDetail4,
                    FromDetail5 = x.ToList()[0].FromDetail5 == null ? "" : x.ToList()[0].FromDetail5,
                    FromSuburb = x.ToList()[0].FromSuburb,
                    FromPostcode = x.ToList()[0].FromPostcode,
                    ToDetail1 = x.ToList()[0].ToDetail1,
                    ToDetail2 = x.ToList()[0].ToDetail2,
                    ToDetail3 = x.ToList()[0].ToDetail3 == null ? "" : x.ToList()[0].ToDetail3,
                    ToDetail4 = x.ToList()[0].ToDetail4 == null ? "" : x.ToList()[0].ToDetail4,
                    ToDetail5 = x.ToList()[0].ToDetail5 == null ? "" : x.ToList()[0].ToDetail5,
                    ToSuburb = x.ToList()[0].ToSuburb,
                    ToPostcode = x.ToList()[0].ToPostcode,
                    StateId = x.ToList()[0].StateId,
                    ServiceCode = x.ToList()[0].ServiceCode,
                    PreAllocatedDriverNumber = x.ToList()[0].PreAllocatedDriverNumber,
                    DriverNumber = x.ToList()[0].DriverNumber,
                    DespatchDateTime = x.ToList()[0].DespatchDateTime,
                    AdvanceDateTime = x.ToList()[0].AdvanceDateTime,
                    LoginDetails = new LoginDetails
                    {
                        Id = x.ToList()[0].LoginDetails.Id
                    },
                    UploadDateTime = x.ToList()[0].UploadDateTime,

                    TotalItems = x.Sum(c => c.lstItems.Count).ToString(),
                    TotalWeight = x.Sum(c => Convert.ToDouble(c.TotalWeight)).ToString(),
                    TotalVolume = x.Sum(c => Convert.ToDouble(c.TotalVolume)).ToString(),

                    lstItems = x.SelectMany(y => y.lstItems).ToList()

                });

            var _groupedBookings = lstBooking.GroupBy(x => x.FromDetail2);


            var consolidatedJobsList = consolidatedBookings.ToList();

            lstBooking = consolidatedJobsList;
            consolidatedJob.ConsolidatedBookings = (List<Booking>)lstBooking;
            consolidatedJob.GroupedJobs = _groupedBookings;

            return consolidatedJob;
        }
        public static ConsolidatedJob ByToDetail1NoItemsPresent(ICollection<Booking> lstBooking)
        {

            var consolidatedJob = new ConsolidatedJob();

            var consolidatedBookings =

                lstBooking.GroupBy(x => new { x.ToDetail1, x.ToDetail2 }).Select(
                x => new Booking
                {
                    AccountCode = x.ToList()[0].AccountCode,
                    Ref1 = x.Count() == 1 ? x.ToList()[0].Ref1 : x.Count().ToString() + " Invoices",
                    Ref2 = x.Count() == 1 ? x.ToList()[0].Ref2 : x.ToList()[0].ToDetail1.Replace('/', ' '), //store customer name as reference
                    Caller = x.ToList()[0].Caller,
                    FromDetail1 = x.ToList()[0].FromDetail1,
                    FromDetail2 = x.ToList()[0].FromDetail2,
                    FromDetail3 = x.ToList()[0].FromDetail3 == null ? "" : x.ToList()[0].FromDetail3,
                    FromDetail4 = x.ToList()[0].FromDetail4 == null ? "" : x.ToList()[0].FromDetail4,
                    FromDetail5 = x.ToList()[0].FromDetail5 == null ? "" : x.ToList()[0].FromDetail5,
                    FromSuburb = x.ToList()[0].FromSuburb,
                    FromPostcode = x.ToList()[0].FromPostcode,
                    ToDetail1 = x.ToList()[0].ToDetail1,
                    ToDetail2 = x.ToList()[0].ToDetail2,
                    ToDetail3 = x.ToList()[0].ToDetail3 == null ? "" : x.ToList()[0].ToDetail3,
                    ToDetail4 = x.ToList()[0].ToDetail4 == null ? "" : x.ToList()[0].ToDetail4,
                    ToDetail5 = x.ToList()[0].ToDetail5 == null ? "" : x.ToList()[0].ToDetail5,
                    ToSuburb = x.ToList()[0].ToSuburb,
                    ToPostcode = x.ToList()[0].ToPostcode,
                    StateId = x.ToList()[0].StateId,
                    ServiceCode = x.ToList()[0].ServiceCode,
                    PreAllocatedDriverNumber = x.ToList()[0].PreAllocatedDriverNumber,
                    DriverNumber = x.ToList()[0].DriverNumber,
                    DespatchDateTime = x.ToList()[0].DespatchDateTime,
                    AdvanceDateTime = x.ToList()[0].AdvanceDateTime,
                    LoginDetails = new LoginDetails
                    {
                        Id = x.ToList()[0].LoginDetails.Id
                    },
                    UploadDateTime = x.ToList()[0].UploadDateTime,

                });

            var _groupedBookings = lstBooking.GroupBy(x => x.ToDetail1);


            var consolidatedJobsList = consolidatedBookings.ToList();

            lstBooking = consolidatedJobsList;
            consolidatedJob.ConsolidatedBookings = (List<Booking>)lstBooking;
            consolidatedJob.GroupedJobs = _groupedBookings;

            return consolidatedJob;
        }

        public static ConsolidatedJob ForOfficeWorksByRef1Values(ICollection<Booking> lstBooking)
        {

            var consolidatedJob = new ConsolidatedJob();

            var consolidatedBookings =

                lstBooking.GroupBy(x => new { x.Ref1 }).Select(
                x => new Booking
                {
                    AccountCode = x.ToList()[0].AccountCode,
                    Ref1 = x.ToList()[0].Ref1,
                    Ref2 = x.ToList()[0].Ref2, 
                    Caller = x.ToList()[0].Caller,
                    FromDetail1 = x.ToList()[0].FromDetail1,
                    FromDetail2 = x.ToList()[0].FromDetail2,
                    FromDetail3 = x.ToList()[0].FromDetail3 == null ? "" : x.ToList()[0].FromDetail3,
                    FromDetail4 = x.ToList()[0].FromDetail4 == null ? "" : x.ToList()[0].FromDetail4,
                    FromDetail5 = x.ToList()[0].FromDetail5 == null ? "" : x.ToList()[0].FromDetail5,
                    FromSuburb = x.ToList()[0].FromSuburb,
                    FromPostcode = x.ToList()[0].FromPostcode,
                    ToDetail1 = x.ToList()[0].ToDetail1,
                    ToDetail2 = x.ToList()[0].ToDetail2,
                    ToDetail3 = x.ToList()[0].ToDetail3 == null ? "" : x.ToList()[0].ToDetail3,
                    ToDetail4 = x.ToList()[0].ToDetail4 == null ? "" : x.ToList()[0].ToDetail4,
                    ToDetail5 = x.ToList()[0].ToDetail5 == null ? "" : x.ToList()[0].ToDetail5,
                    ToSuburb = x.ToList()[0].ToSuburb,
                    ToPostcode = x.ToList()[0].ToPostcode,
                    StateId = x.ToList()[0].StateId,
                    ServiceCode = x.ToList()[0].ServiceCode,
                    PreAllocatedDriverNumber = x.ToList()[0].PreAllocatedDriverNumber,
                    DriverNumber = x.ToList()[0].DriverNumber,
                    DespatchDateTime = x.ToList()[0].DespatchDateTime,
                    AdvanceDateTime = x.ToList()[0].AdvanceDateTime,
                    OkToUpload = x.ToList()[0].OkToUpload,
                    IsQueued = x.ToList()[0].IsQueued,
                    ExtraPuInformation = x.ToList()[0].ExtraPuInformation == null ? "" : x.ToList()[0].ExtraPuInformation,
                    ExtraDelInformation = x.ToList()[0].ExtraDelInformation == null ? "" : x.ToList()[0].ExtraDelInformation,
                    LoginDetails = new LoginDetails
                    {
                        Id = x.ToList()[0].LoginDetails.Id
                    },
                    UploadDateTime = x.ToList()[0].UploadDateTime,

                    TotalItems = x.Sum(c => Convert.ToDouble(c.TotalItems)).ToString(),
                    TotalWeight = x.Sum(c => Convert.ToDouble(c.TotalWeight)).ToString(),
                    TotalVolume = x.Sum(c => Convert.ToDouble(c.TotalVolume)).ToString(),

                    lstItems = x.SelectMany(y => y.lstItems).ToList(),
                    Notification = x.ToList()[0].Notification
                });

            var _groupedBookings = lstBooking.GroupBy(x => x.ToDetail2);


            var consolidatedJobsList = consolidatedBookings.ToList();

            lstBooking = consolidatedJobsList;
            consolidatedJob.ConsolidatedBookings = (List<Booking>)lstBooking;
            consolidatedJob.GroupedJobs = _groupedBookings;

            return consolidatedJob;
        }

    }
}
