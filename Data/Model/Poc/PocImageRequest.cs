using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model.Poc
{
	public class PocImageRequest
	{
		public string JobNumber { get; set; }
		public string SubJobNumber { get; set; }
		public string JobDateTime { get; set; }
		public string StatePrefix { get; set; }
		public string ClientId { get; set; }
		public string Key { get; set; }
	}
}
