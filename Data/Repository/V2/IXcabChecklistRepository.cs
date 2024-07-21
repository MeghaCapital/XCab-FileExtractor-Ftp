using Core;
using Data.Model.Checklist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.V2;

public interface IXcabChecklistRepository
{
    public Task<ICollection<ChecklistImageResponse>> ExtractChecklistImagesAsync(string jobNumber, int legNumber, DateTime jobDate, EStates stateId);
}
