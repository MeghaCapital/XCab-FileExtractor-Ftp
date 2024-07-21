namespace Data.Entities.Items
{
    public class XCabItems
    {
        public int ItemId { get; set; }
        public int BookingId { get; set; }
        public string Description { get; set; }
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public decimal Cubic { get; set; }
        public string Barcode { get; set; }
        public int Qantity { get; set; }
        public string Status { get; set; }
    }
}
