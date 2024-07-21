using Data.Entities.Futile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.V2
{
	public interface IILogixFutileJobRepository
	{
		public Task<ICollection<JobFutile>> GetFutileJobDetails(string consignmentNumber);
	}
}
