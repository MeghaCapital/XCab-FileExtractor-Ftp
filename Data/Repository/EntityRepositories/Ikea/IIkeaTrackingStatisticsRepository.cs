using Data.Model.Tracking.IKEA;

namespace Data.Repository.EntityRepositories.Ikea
{
    public interface IIkeaTrackingStatisticsRepository
    {
        Task InsertTrackingStatistics(IkeaTrackingStatisticModel ikeaTrackingStatisticModel);

        Task<IkeaTrackingStatisticModel> GetStatisticsForIkeaTrackingEvents();
    }
}
