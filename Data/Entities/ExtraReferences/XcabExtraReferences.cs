namespace Data.Entities.ExtraReferences
{
    public class XCabExtraReferences
    {
        public int Id { get; set; }
        public int PrimaryBookingId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public bool UseInUns { get; set; }
    }
}
