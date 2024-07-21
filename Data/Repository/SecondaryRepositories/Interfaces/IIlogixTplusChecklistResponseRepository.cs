using System.Collections.Generic;

namespace Data.Repository.SecondaryRepositories.Interfaces
{
    public interface IIlogixTplusChecklistResponseRepository
    {
        IEnumerable<int> Get();
    }
}
