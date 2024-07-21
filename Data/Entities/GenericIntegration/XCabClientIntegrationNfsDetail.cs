namespace Data.Entities.GenericIntegration
{
    /// <summary>
    /// Client Nfs Details
    /// </summary>
    public class XCabClientIntegrationNfsDetail
    {
        public int ClientId { get; set; }
        public string UncPathBookings { get; set; }
        public string UncPathProcessed { get; set; }
        public string UncPathError { get; set; }

    }
}
