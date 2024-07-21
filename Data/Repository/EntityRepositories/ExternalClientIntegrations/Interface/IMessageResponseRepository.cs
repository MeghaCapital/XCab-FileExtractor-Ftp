using Data.Entities.ExternalClientIntegration;

namespace Data.Repository.EntityRepositories.Interface.ExternalClientIntegration
{
    public interface IMessageResponseRepository
    {
        void Insert(MessageResponse messageResponses);
    }
}
