using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.V2
{
	public interface IXCabClientSettingRepository
	{
		public Task<bool> IsStagedBooking(int ftpLoginId, int state, string accountCode, string serviceCode = null);
	}
}
