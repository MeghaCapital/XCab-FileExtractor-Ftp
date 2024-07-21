using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.AccessControl
{
	public enum EAccessControl
	{
		Successful = 1,
		AuthenticationFailed = 2,
		AuthorizationFailed = 3
	}
}
