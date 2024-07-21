namespace Data.Repository.SecondaryRepositories.CustomerDelSuburbs
{
    public interface IXCabCustomerDelSuburbsRepository
    {
        bool IsDelSuburbValid(int LoginId, string fromSuburb, string toSuburb, string fromPostcode, string toPostcode, int distance, string storeName);
    }
}
