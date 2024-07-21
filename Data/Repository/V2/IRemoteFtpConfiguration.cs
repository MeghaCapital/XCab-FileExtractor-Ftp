using Data.Entities.Ftp;

namespace Data.Repository.V2;

public interface IRemoteFtpConfiguration
{
    Task<List<XcabRemoteConfiguration>> GetConfigurationsAsync();
}
