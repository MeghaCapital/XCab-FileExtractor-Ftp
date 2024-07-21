using Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.AccessControl
{
	public interface IAuthenticationProvider
	{
		Task<XCabAccessControl> VerifyAuthenticationForBooking(string username, string password, string accountCode, int stateId, bool isTestUser);
		Task<XCabAccessControl> VerifyAuthenticationForTracking(string username, string password, string apiKey, string accountCode, int stateId, bool isTestUser);
	}
}
