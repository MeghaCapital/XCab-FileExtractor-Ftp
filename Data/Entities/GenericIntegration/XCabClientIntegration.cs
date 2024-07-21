namespace Data.Entities.GenericIntegration
{
    /// <summary>
    /// Client Integration Details
    /// </summary>
    public class XCabClientIntegration
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ClientCode { get; set; }
        public string StateId { get; set; }
        public string DefaultPickupAddressLine1 { get; set; }
        public string DefaultPickupAddressLine2 { get; set; }
        public string DefaultPickupAddressLine3 { get; set; }
        public string DefaultPickupAddressLine4 { get; set; }
        public string DefaultPickupAddressLine5 { get; set; }
        public string DefaultPickupSuburb { get; set; }
        public string DefaultPickupPostcode { get; set; }
        public string EmailList { get; set; }
        public bool AllowConsolidation { get; set; }
        public int IntegrationType { get; set; }
        public int FtpLoginId { get; set; }
        public bool SubAccountActive { get; set; }
        public string ServiceCode { get; set; }
        public bool IsUniqueJobsPerDay { get; set; }
        public int NumOfDaysJobsUnique { get; set; }
    }

}
