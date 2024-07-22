using xcab.como.common.Data.Repository;

namespace XCabService.ComoActiveStatesService
{
	public class ComoActiveStatesProvider : IComoActiveStatesProvider
	{
		private readonly IComoActiveStatesRepository _comoActiveStatesRepository;
		public ComoActiveStatesProvider()
		{
			_comoActiveStatesRepository = new ComoActiveStatesRepository();
		}

		public async Task<bool> IsComoActiveState(int state, DateTime dateTime)
		{
			var isUsingComo = await _comoActiveStatesRepository.IsStateActiveForComo(state, dateTime);
			if (isUsingComo)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public async Task<IDictionary<int, bool>> GetComoActiveStatusForStates()
		{
			var comoActiveStates = await _comoActiveStatesRepository.GetAllComoActiveStates();
			IDictionary<int, bool> statesDictionary = new Dictionary<int, bool>();
			for (int i = 1; i <= 5; i++)
			{
				bool isComoActive = comoActiveStates.Contains(i);
				statesDictionary.Add(i, isComoActive);
			}
			return statesDictionary;
		}
	}
}
