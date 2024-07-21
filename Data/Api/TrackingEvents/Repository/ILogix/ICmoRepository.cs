using Data.Api.TrackingEvents.Model.ILogix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Api.TrackingEvents.Repository.ILogix
{
    public interface ICmoRepository
    {
        CmoDriverRow GetCmoDriverRow(int driverNumber);
    }
}
