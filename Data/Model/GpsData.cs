using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
	public class Gps
	{
		public int Id { get; set; }

		public string DriverNumber { get; set; }

		public DateTime GpsDateTime { get; set; }	

		public decimal Latitude { get; set; }

		public decimal Longitude { get; set; }

		public decimal Speed { get; set; }

		public DateTime? DriveDate { get; set; } 

		public decimal? Distance { get; set; }
	}
}
