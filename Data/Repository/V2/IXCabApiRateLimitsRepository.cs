using Data.Entities.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.V2
{
    public interface IXCabApiRateLimitsRepository
    {
        Task<XcabApiRateLimits> GetXcabApiRateLimits(string apiKey, int loginId);
        Task<XcabApiRateLimits> GetXcabApiRateLimitsFromTest(string apiKey, int loginId);
    }
}
