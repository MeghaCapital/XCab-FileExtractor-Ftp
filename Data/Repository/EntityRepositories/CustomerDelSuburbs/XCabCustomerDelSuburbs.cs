namespace Data.Repository.EntityRepositories.CustomerDelSuburbs
{
    public class XCabCustomerDelSuburbs
    {
        public int Id { get; set; }
        public int LoginId { get; set; }
        public int StateId { get; set; }
        public string StoreName { get; set; }
        public string FromSuburb { get; set; }
        public int FromPostcode { get; set; }
        public string ToSuburb { get; set; }
        public int ToPostcode { get; set; }
        public int Distance { get; set; }
        public bool Active { get; set; }
    }
}
