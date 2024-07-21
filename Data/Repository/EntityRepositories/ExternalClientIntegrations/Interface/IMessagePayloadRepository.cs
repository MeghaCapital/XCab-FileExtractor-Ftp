using Data.Entities.ExternalClientIntegration;

namespace Data.Repository.EntityRepositories.Interface.ExternalClientIntegration
{
    public interface IMessagePayloadRepository
    {
        Task<int> Insert(MessagePayload messagePayloads);
    }
}
