using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model.PodStreamer.V2
{
	public class PodImageResponse
	{
		public byte[] Image { get; set; }
		public string PodName { get; set; }
	}
}
