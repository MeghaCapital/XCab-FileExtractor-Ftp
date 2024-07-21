namespace Data.Entities.EmailAlerts
{
    public class XCabEmailAlerts
    {
        public int Id { get; set; }
        public int StateId { get; set; }
        public string ClientName { get; set; }
        public string EmailAddress { get; set; }
        public int LoginId { get; set; }
        public bool Active { get; set; }
    }
}
