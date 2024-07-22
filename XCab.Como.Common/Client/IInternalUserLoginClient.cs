using System.Threading.Tasks;
using xcab.como.common.Data.Response;

namespace xcab.como.common.Client
{
    public interface IInternalUserLoginClient
    {
        Task<AccessTokenResponse> GetAccessTokenAsync();
    }
}
