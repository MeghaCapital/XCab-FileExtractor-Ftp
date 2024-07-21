using Data.Api.TrackingEvents.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Utils.Helpers
{
    public abstract class UniqueReferenceTypeHelper
    {
        public static string GetUniqueReferenceTypeForDatabaseMapping(EUniqueReferenceType eUniqueReferenceType)
        {
            var referenceType = "";
            switch (eUniqueReferenceType)
            {
                case EUniqueReferenceType.Reference1:
                    referenceType = "Ref1";
                    break;
                case EUniqueReferenceType.Reference2:
                    referenceType = "Ref2";
                    break;
                case EUniqueReferenceType.ConsignmentNumber:
                    referenceType = "ConsignmentNumber";
                    break;
                case EUniqueReferenceType.JobNumber:
                    referenceType = "TPLUS_JobNumber";
                    break;
                default:
                    referenceType = "Any";
                    break;
            }
            return referenceType;
        }
    }
}
