namespace Data.Model.Driver
{
    public class XCabDriverRoute
    {
        public int Id { get; set; }
        public int LoginId { get; set; }
        public int StateId { get; set; }
        public string RouteName { get; set; }

        public string DriverNumber { get; set; }
        public bool IsConsolidationAllowed { get; set; }
        public bool IsQueueAllocateAllowed { get; set; }

        public string AccountCode { get; set; }
    }
}
