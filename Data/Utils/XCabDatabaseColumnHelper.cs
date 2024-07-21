using Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Utils
{
    public abstract class XCabDatabaseColumnHelper

    {        
        public static string GetReferenceColumnName(EReferenceType reftype) => reftype switch
        {
            EReferenceType.Reference1 => "Ref1",
            EReferenceType.Reference2 => "Ref2",
            EReferenceType.ConsignmentNumber => "ConsignmentNumber",
            _ => "ConsignmentNumber"
        };
    }
}
