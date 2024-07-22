using Core;

namespace XCabService.IkeaService
{
    public interface IIkeaTrackingService
    {
        List<string> GetFileContent(
        string barcode,
        string status,
        string eventDateTime,
        string podName,
        string suburb,
        string workflow,
        string consignmentNumber,
        string serviceCode,
        bool isFutile,
        bool isFutileAdditionalJob,
        string futileReason,
        int driverId, string accountCode,
        string type = "PACKAGE");

        Task<List<ExtraFields>> GetTypeDetails(int bookingId);
    }
}
