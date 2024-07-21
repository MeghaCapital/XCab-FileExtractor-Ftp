using Core;
using Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.V2
{
	public interface IILogixGpsRepository
	{
		public Task<ICollection<Gps>> GetLastKnownGpsDetails(IEnumerable<int> driverNumbers, EStates state);

		public Task<ICollection<Gps>> GetGpsDetailsBetweenDates(string startDateTime, string endDateTime, ICollection<int> driverNumbers, EStates state);		
	}
}
