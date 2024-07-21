using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helpers
{
    public abstract class ILogixHelpers
    {
        public static string GetILogixJobNumber(string jobNumber, EStates state, DateTime allocationDateTime)
        {
            string iLogixJobNumber = "";
            if (allocationDateTime!=DateTime.MinValue && !string.IsNullOrEmpty(jobNumber))
            {
                //sample iLogixJobNumber: 050922B00292070
                iLogixJobNumber = allocationDateTime.ToString("ddMMyy");               
                switch (state)
                {
                    case EStates.VIC:
                        iLogixJobNumber += "M";
                        break;
                    case EStates.NSW:
                        iLogixJobNumber += "S";
                        break;
                    case EStates.QLD:
                        iLogixJobNumber += "B";
                        break;
                    case EStates.SA:
                        iLogixJobNumber += "A";
                        break;
                    case EStates.WA:
                        iLogixJobNumber += "P";
                        break;
                }
                iLogixJobNumber += jobNumber.PadLeft(8, '0');                
            }
            return iLogixJobNumber;
        }
    }
}
