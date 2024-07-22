using Core;

namespace XCabService.IkeaService
{
    public interface IIkeaTrackingStatisticsService : IccrProcess
    {
        Task IkeaTrackingStatisticsHandler();
    }
}
