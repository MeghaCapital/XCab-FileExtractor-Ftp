using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model.PodStreamer.V2
{
	public class PodImageRequest
	{
		public string JobNumber { get; set; }
		public EStates StateId { get; set; }
		public DateTime JobDate { get; set; }
		public int? ComoJobId { get; set; }
		public int LegNumber { get; set; }
	}
}
