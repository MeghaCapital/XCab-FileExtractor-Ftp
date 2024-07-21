
using Core;

namespace Data.Repository.V2
{
    public interface IXCabAsnReleaseScheduleRepository
    {
        Task<List<AsnReleaseSchedules>> GetAsnReleaseSchedules();
    }
}
