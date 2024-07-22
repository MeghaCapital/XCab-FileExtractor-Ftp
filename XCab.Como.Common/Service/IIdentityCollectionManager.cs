using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace xcab.como.common.Service
{
    public interface IIdentityCollectionManager
    {
        Task<int?> Search<I>(I entityEnum, string entityName, string property, string searchInput, Dictionary<string, string> filters = null) where I : struct, IConvertible;
    }
}
