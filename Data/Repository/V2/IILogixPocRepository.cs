using Core;
using Data.Model.Poc.V2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.V2
{
	public interface IILogixPocRepository
	{
		public Task<ICollection<PocImageResponse>> ExtractPocImages(string jobNumber, int legNumber, DateTime jobDate, EStates stateId);

	}
}
