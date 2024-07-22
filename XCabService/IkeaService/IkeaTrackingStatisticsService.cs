using Core.Logging.SeriLog;
using Data.Repository.EntityRepositories.Ikea;
using Quartz;

namespace XCabService.IkeaService
{
    public class IkeaTrackingStatisticsService : IIkeaTrackingStatisticsService
    {
        private IIkeaTrackingStatisticsRepository _ikeaTrackingStatisticsRepository;
        public IkeaTrackingStatisticsService()
        {
            _ikeaTrackingStatisticsRepository = new IkeaTrackingStatisticsRepository();
        }
        public async Task Execute(IJobExecutionContext context)
        {
            RollingLogger.WriteToIkeaTrackingFileCreatorLogs("IkeaTrackingStatisticsService scheduler started.", ELogTypes.Information);
            await IkeaTrackingStatisticsHandler();
        }

        public async Task IkeaTrackingStatisticsHandler()
        {
            var expectedNumberOfIkeaTrackingEvents = await _ikeaTrackingStatisticsRepository.GetStatisticsForIkeaTrackingEvents();
            await _ikeaTrackingStatisticsRepository.InsertTrackingStatistics(expectedNumberOfIkeaTrackingEvents);
        }

        public string Name()
        {
            return nameof(IkeaTrackingStatisticsService);
        }

        public void Start()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
