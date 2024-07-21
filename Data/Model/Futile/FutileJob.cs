using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model.Futile
{
	public class FutileJob
	{
		public string AccountCode { get; set; }
		public int StateId { get; set; }
		public string OriginalConNumber { get; set; }
		public int OriginalJobNumber { get; set; }
		public int SubJobNumber { get; set; }
		public string Ref1 { get; set; }
		public string Ref2 { get; set; }
		public List<FutileLeg> PreLegs { get; set; }
		public FutileLeg CurrentLeg { get; set; }
		public List<FutileLeg> PostLegs { get; set; }
	}
}
