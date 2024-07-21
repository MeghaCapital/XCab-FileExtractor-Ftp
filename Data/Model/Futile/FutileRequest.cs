using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model.Futile
{
	public class FutileRequest
	{
		public int JobNumber { get; set; }
		public EStates State { get; set; }
		public string ConsignmentNumber { get; set; }
		public int? ComoJobId { get; set; }
		public string AccountCode { get; set; }
    }
}
