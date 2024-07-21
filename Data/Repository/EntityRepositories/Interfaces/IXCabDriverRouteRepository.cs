
using Data.Model.Driver;

namespace Data.Repository.EntityRepositories.Interfaces
{
    public interface IXCabDriverRouteRepository
    {
        XCabDriverRoute GetXCabDriverRoutes(int driverNumber, string logininId);
        XCabDriverRoute GetXCabDriverRoutesForRouteName(string routeName, string logininId);

        XCabDriverRoute GetXCabDriverRoutesForRouteName(string routeName, string logininId, int state, string accountCode);
    }
}
