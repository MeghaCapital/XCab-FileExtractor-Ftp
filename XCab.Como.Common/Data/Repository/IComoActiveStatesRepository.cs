using System;
using System.Collections.Generic;

namespace xcab.como.common.Data.Repository
{
    public interface IComoActiveStatesRepository
    {
        Task<ICollection<int>> GetAllComoActiveStates();
        Task<bool> IsStateActiveForComo(int statieId, DateTime dateTime);
    }
}
