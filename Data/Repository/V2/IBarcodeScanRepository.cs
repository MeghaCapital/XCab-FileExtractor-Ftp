using Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.V2
{
    public interface IBarcodeScanRepository
    {
        public Task<ICollection<XCabBarcodeScan>> ExtractXcabScanDetails(string jobNumber, string accountCode, DateTime jobDate, int stateId);
    }
}
