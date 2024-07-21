using Data.Model;

namespace Data.Repository.EntityRepositories.Brand
{
    public interface IBrandRepository
    {
        Task<BusinessBrand> GetBrandDetails(string accountCode, int stateId);
    }
}
