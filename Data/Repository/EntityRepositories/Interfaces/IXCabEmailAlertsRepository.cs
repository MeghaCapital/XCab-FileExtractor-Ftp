using Data.Entities.EmailAlerts;

namespace Data.Repository.EntityRepositories.Interfaces
{
    public interface IXCabEmailAlertsRepository
    {
        XCabEmailAlerts GetXCabEmailAlert(int LoginId, int StateId);
    }
}
