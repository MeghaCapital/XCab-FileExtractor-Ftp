namespace Data.Repository.EntityRepositories.CustomerDelSuburbs
{
    public interface IXCabCustomerDelSuburbsRepository
    {
        Task<bool> IsDelSuburbValid(int LoginId, string fromSuburb, string toSuburb, string fromPostcode, string toPostcode, int distance, string storeName);
    }
}
