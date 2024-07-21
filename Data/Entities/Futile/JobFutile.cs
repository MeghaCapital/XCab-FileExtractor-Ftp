using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities.Futile
{
	public class JobFutile
	{
        public int Id { get; set; }
        public string AccountCode { get; set; }
        public int JobNumber { get; set; }
        public int SubJobNumber { get; set; }
        public int StateId { get; set; }
        public int FutileType { get; set; }
        public int DriverNumber { get; set; }
        public int GeneratedFutileJobNumber { get; set; }
        public string Ref1 { get; set; }
        public string Ref2 { get; set; }
        public string ConsignmentNumber { get; set; }
        public bool IsUltimateOfBatch { get; set; }
    }
}
