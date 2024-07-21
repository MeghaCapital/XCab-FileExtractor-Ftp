using Data.Entities.ExternalClientIntegration;

namespace Data.Repository.EntityRepositories.ExternalClientIntegrations.Interface
{
    public interface IXCabWebhookTrackingAccountsRepository
    {
        Task<XCabWebhookTrackingAccounts> GetXCabWebhookTrackingAccounts(int externalClientId, string accountCode, int stateId);
    }
}
