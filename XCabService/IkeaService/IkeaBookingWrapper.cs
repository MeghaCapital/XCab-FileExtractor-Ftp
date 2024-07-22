using Data.Entities;

namespace XCabService.IkeaService;

public class IkeaBookingWrapper
{
    public XCabBooking Booking { get; set; }
    public IkeaModifyType ModifyType { get; set; } = IkeaModifyType.Create;
    public string Workflow { get; set; }
}