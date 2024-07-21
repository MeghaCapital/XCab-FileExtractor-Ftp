using Data.Entities.EmailNotification;

namespace Data.Repository.EntityRepositories.Interfaces
{
    public interface IXCabEmailClientTemplateRepository
    {
        Task<XCabEmailClientTemplate> GetXCabEmailClientTemplate(int emailClientId);

    }
}
