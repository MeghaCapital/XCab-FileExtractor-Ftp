using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.EntityRepositories.Interfaces
{
    interface IXCabRemarksRepository
    {
        List<string> GetXCabRemarks(int bookingId);
        void Insert(List<string> remarks, int bookingId);

    }
}
