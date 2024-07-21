using Data.Model.FileStatistics;

namespace Data.Repository.V2
{
    public interface IXCabFtpFileStatisticsRepository
    {
        Task Insert(ICollection<XCabFtpFileStatistics> fileStatistics);
        Task UpdatePreviousStatisics();
    }
}
