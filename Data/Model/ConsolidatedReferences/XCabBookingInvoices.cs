// ----------------------------------------------------------
// Written by Graeme Anderson
// 24th February 2017
// ---------------------------------------------------------- 

using Data.Entities;

namespace Data.Model.ConsolidatedReferences
{
    /// <summary>
    /// extension of the XCabBooking Class to cater for lookup of jobs in the 
    /// ancient iLogix Website. The link will contain a string like :
    /// </summary>
    public class XCabBookingInvoices : XCabBooking
    {
        // ReSharper disable once InconsistentNaming
        public string iLogixWWWLink { get; set; }

        // used to return ref1 ids
        public string Ref1Ids { get; set; }
        // used to return ref2 ids
        public string Ref2Ids { get; set; }

    }

    public class ClientReferenceIdList
    {
        // used to return ref1 ids
        public string Ref1Ids { get; set; }
        // used to return ref2 ids
        public string Ref2Ids { get; set; }

    }
}
