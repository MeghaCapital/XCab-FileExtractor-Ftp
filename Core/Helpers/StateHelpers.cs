using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helpers
{
    public abstract class StateHelpers
    {
        public static int GetStateId(string state)
        {
            int stateId = -1;
            switch (state.ToUpper())
            {
                case "VIC":
                    stateId = 1;
                    break;
                case "NSW":
                    stateId = 2;
                    break;
                case "QLD":
                    stateId = 3;
                    break;
                case "SA":
                    stateId = 4;
                    break;
                case "WA":
                    stateId = 5;
                    break;
                case "NT":
                    stateId = 9;
                    break;
            }
            return stateId; 
        }

        public static int GetTplusStateId(string state)
        {
            int stateId = -1;
            switch (state.ToUpper())
            {
                case "VIC":
                    stateId = 1;
                    break;
                case "NSW":
                    stateId = 2;
                    break;
                case "QLD":
                    stateId = 3;
                    break;
                case "SA":
                    stateId = 4;
                    break;
                case "WA":
                    stateId = 5;
                    break;
                case "NT":
                    stateId = 3;
                    break;
            }
            return stateId;
        }
        public static EStates GetStateEnum(int stateId)
        {
            EStates state = EStates.NAT;
            switch (stateId)
            {
                case 1:
                    state = EStates.VIC;
                    break;
                case 2:
                    state = EStates.NSW;
                    break;
                case 3:
                    state = EStates.QLD;
                    break;
                case 4:
                    state = EStates.SA;
                    break;
                case 5:
                    state = EStates.WA;
                    break;
                case 9:
                    state = EStates.NT;
                    break;
            }
            return state;
        }
        public static string GetStatePrefix(EStates state)
        {
            string statePrefix = "";
            switch (state)
            {
                case EStates.VIC:
                    statePrefix = "M";
                    break;
                case EStates.NSW:
                    statePrefix = "S";
                    break;
                case EStates.QLD:
                    statePrefix = "B";
                    break;
                case EStates.SA:
                    statePrefix = "A";
                    break;
                case EStates.WA:
                    statePrefix = "P";
                    break;
            }
            return statePrefix;
        }

        public static string GetStatePrefix(int stateId)
        {
            var statePrefix = "";
            switch (stateId)
            {
                case 1:
                    statePrefix = "M";
                    break;
                case 2:
                    statePrefix = "S";
                    break;
                case 3:
                    statePrefix = "B";
                    break;
                case 4:
                    statePrefix = "A";
                    break;
                case 5:
                    statePrefix = "P";
                    break;
            }
            return statePrefix;
        }

        public static string GetStateAbbrev(string state)
        {
            var stateAbbr = "";
            switch (state)
            {
                case "1":
                    stateAbbr = "VIC";
                    break;
                case "2":
                    stateAbbr = "NSW";
                    break;
                case "3":
                    stateAbbr = "QLD";
                    break;
                case "4":
                    stateAbbr = "SA";
                    break;
                case "5":
                    stateAbbr = "WA";
                    break;
                case "9":
                    stateAbbr = "NT";
                    break;
                default:
                    break;
            }
            return stateAbbr;
        }
    }
}
