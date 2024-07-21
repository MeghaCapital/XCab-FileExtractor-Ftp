namespace Data.Repository.EntityRepositories
{
    public interface IXCabDriverRepository
    {
        Task<ICollection<int>> GetVehicleIdWhereCrane(int driverId);

        Task<string> GetDriverClass(int driverId);
    }
}