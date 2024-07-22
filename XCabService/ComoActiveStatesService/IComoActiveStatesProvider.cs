namespace XCabService.ComoActiveStatesService
{
	public interface IComoActiveStatesProvider
	{
		Task<bool> IsComoActiveState(int state, DateTime dateTime);
		Task<IDictionary<int, bool>> GetComoActiveStatusForStates();
	}
}