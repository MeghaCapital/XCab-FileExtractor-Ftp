using Data.Model.Tracking.ILogix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.V2
{
	public interface IILogixCustomerRepository
	{
		Task<ILogixCustomer> GetCustomerByName(string customerName);
	}
}
