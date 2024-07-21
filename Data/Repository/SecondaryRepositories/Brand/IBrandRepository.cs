using Data.Model;

namespace Data.Repository.SecondaryRepositories.Brand
{
    public interface IBrandRepository
    {
        Task<BusinessBrand> GetBrandDetails(string accountCode, int stateId);
    }
}
