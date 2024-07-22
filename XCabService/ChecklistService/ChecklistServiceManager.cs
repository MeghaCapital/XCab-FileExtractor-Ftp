using Core;
using Data.Model;
using Data.Model.Checklist;
using Google.Protobuf.Reflection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xcab.como.booker.Data.Variable;
using XCabService.PocService;

namespace XCabService.ChecklistService;

public class ChecklistServiceManager : IChecklistServiceManager
{
    private IChecklistService _checklistService;

    public ChecklistServiceManager(IChecklistService checklistService)
    {
        _checklistService = checklistService;
    }

    public ChecklistServiceManager() {
        _checklistService = new Xcab.ChecklistService();
    }

    public async Task<ICollection<ChecklistImageResponse>> GetChecklistImagesAsync(ChecklistImageRequest checklistImageRequest)
    {
        var checklistImages = new List<ChecklistImageResponse>();

        if (checklistImageReqeustIsValid(checklistImageRequest))
            return checklistImages;

        try
        {
            checklistImages = (await _checklistService.GetChecklistImagesAsync(checklistImageRequest)).ToList();
        } catch (Exception ex)
        {
            _ = Logger.Log(
                     "Exception Occurred in GetPocImage : Failed extracting Checklist image for request - " + JsonConvert.SerializeObject(checklistImageRequest) + ". Message: " +
                     ex.Message, nameof(ChecklistServiceManager));
        }

        return checklistImages;
    }


    private bool checklistImageReqeustIsValid(ChecklistImageRequest checklistImageRequest)
    {
        var res = false;

        if (checklistImageRequest?.ComoJobId is not null)
        {
            res = true;
        } else if (string.IsNullOrEmpty(checklistImageRequest?.JobNumber) && checklistImageRequest?.JobDate is not null)
        {
            res = true;
        }

        return res;
    }



}
