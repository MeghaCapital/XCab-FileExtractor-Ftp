using Data.Model.Route;

namespace Data.Repository.V2
{
	public interface IXCabBookingRoutesRepository
	{
		Task<ICollection<RouteBarcodeDetails>> GetBarcodesWithDropSequenceForRoute(RouteBarcodesRequest routeBarcodesRequest);
	}
}