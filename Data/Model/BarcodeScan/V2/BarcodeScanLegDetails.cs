
namespace Data.Model.BarcodeScan.V2
{
    public class BarcodeScanLegDetails
    {
        public DateTime EventDateTime { get; set; }
        public ICollection<string> BarcodeIdentified { get; set; }
        public ICollection<BarcodeExceptionDetails> BarcodeException { get; set; }

    }
}
