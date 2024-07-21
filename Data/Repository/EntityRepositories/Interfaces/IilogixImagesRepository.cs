using Data.Entities.Ilogix;

namespace Data.Repository.EntityRepositories.Interfaces
{
    public interface IIlogixImagesRepository
    {
        Task<ICollection<PodImage>> GetPodImage(string jobnumber, string statePrefix, DateTime jobDate);
        ICollection<PodImage> GetPodImage(string jobnumber, string subJobNumber, string statePrefix, DateTime jobDate);
        ICollection<PodImage> GetPodImageV2(string iLogixJobNumber, string subJobNumber, bool useArchiveDatabase = false);
        ICollection<PocImage> GetPocImage(string jobnumber, string statePrefix, DateTime jobDate);
        ICollection<PocImage> GetPocImage(string jobnumber, string subJobNumber, string statePrefix, DateTime jobDate);
    }
}
