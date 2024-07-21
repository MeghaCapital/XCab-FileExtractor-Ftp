using Data.Model;
using System.Collections.Generic;

namespace Data.Repository.SecondaryRepositories.Interfaces
{
    public interface IChecklistResponseRepository
    {
        void UpdateSchema(object schema);

        IEnumerable<TplusWebApi> Get(string token);

        IEnumerable<int> Get();
    }
}
