namespace Data.Entities.Tplus
{
    public class TplusSoapEntity
    {
        public string Request { get; set; }
        public string Response { get; set; }
        public string JobNumber { get; set; }
        public string ErrorDescription { get; set; }
        public string QuoteRequest { get; set; }
        public string QuoteResponse { get; set; }
        public string JobPriceExGst { get; set; }
        public string JobGst { get; set; }
        public string JobTotalPrice { get; set; }
        // public string Eta { get; set; }
    }
}
