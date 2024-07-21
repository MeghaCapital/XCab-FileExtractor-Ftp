using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model.Futile
{
	public class FutileLeg
	{
		public int JobNumber { get; set; }
		public string ConsignmentNumber { get; set; }
		public int DriverNumber { get; set; }
		public bool IsUltimateOfBatch { get; set; }
		public bool IsUltimateOfJob { get; set; }
		public futileType FutileType { get; set; }
	}

	public enum futileType
	{
		ReturnToSender = 1,
		ReturnToDepot = 3,
		RedeliverDirect = 4,
		RedeliverViaDepot = 5
	}
}
