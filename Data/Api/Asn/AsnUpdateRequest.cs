namespace Data.Api.Asn
{
    public class AsnUpdateRequest
    {
        public string ConsignmentNumber { get; set; }

        public string AccountCode { get; set; }

        public int StateId { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public string? ToSuburb { get; set; }

        public string? ToDetail1 { get; set; }

        public string? ToDetail2 { get; set; }

        public string? ToDetail3 { get; set; }
    }
}
