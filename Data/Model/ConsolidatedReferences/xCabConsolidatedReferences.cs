using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model.ConsolidatedReferences
{
    public class xCabConsolidatedReferences
    {
        public string Reference1 { get; set; }
        public string Reference2 { get; set; }
        public int ConsolidateJobId { get; set; }
        public string Barcode { get; set; }
        public int PrimaryJobId { get; set; }
    }
}
