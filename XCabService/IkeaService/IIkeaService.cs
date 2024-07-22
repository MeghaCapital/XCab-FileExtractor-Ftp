using System.Xml;
using Data.Entities;

namespace XCabService.IkeaService;

public interface IIkeaService
{
    public Task<List<IkeaBookingWrapper>> ParseExportXml(string fileName, int ftpLoginId);
    public Task<List<IkeaPinrouteModifier>> ParsePinrouteXls(string fileName);    
    public Task<bool> ProcessBookings(List<IkeaBookingWrapper> bookings);
	public Task ProcessPinroutes(List<IkeaPinrouteModifier> pinroutes, string fileName);
	public Task<string> GenerateAccountCode(string senderBu);
    public Task<string> GenerateServiceCode(string storeRef, double weight, bool isExchange, bool hasDelivery);
    public Task<int> GenerateAccountStateId(string accountCode);
    public Task<TimeOnly> ParseTime(string time);
    public Task<string> CreateUpdateXml(string shipmentNo, string status, DateTime eventTime, string podName, string suburb, string shipmentType);
}
