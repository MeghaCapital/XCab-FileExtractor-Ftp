using Data.Model.Futile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCabService.FutileService
{
	public interface IFutileServiceManager
	{
		Task<FutileJobResponse> GetFutileDetails(string consignmentNumber);
	}
}
