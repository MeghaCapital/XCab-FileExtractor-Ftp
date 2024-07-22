using Data.Model.Poc.V2;
using Data.Model.PodStreamer.V2;

namespace xcab.como.tracker.Service
{
    public interface IImageExtractor
    {
        IEnumerable<byte[]> AttestationRecord(long decodedJobNumber, ELegType legType, EDocumentType docType);

        Task<IEnumerable<PocImageResponse>> GetPoc(int comoJobId, ELegType legType);

        Task<IEnumerable<PodImageResponse>> GetPod(int comoJobId, ELegType legType);
    }
}
