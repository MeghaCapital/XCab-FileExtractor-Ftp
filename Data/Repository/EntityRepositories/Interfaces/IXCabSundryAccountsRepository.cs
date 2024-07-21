namespace Data.Repository.EntityRepositories.Interfaces
{
    public interface IXCabSundryAccountsRepository
    {
        bool? Exists(string accountCode, int stateId, string sundryCode);
    }
}
