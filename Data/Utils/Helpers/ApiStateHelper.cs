using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Utils.Helpers
{
    public abstract class ApiStateHelper
    {
        public static Api.Bookings.BookingModel.State GetApiStateEnum(EStates eState)
        {
            //making default as VIC as all validations should have happened in the other layers
            Api.Bookings.BookingModel.State state = Api.Bookings.BookingModel.State.Vic;
            switch (eState)
            {
                case EStates.VIC:
                    state = Api.Bookings.BookingModel.State.Vic;
                    break;
                case EStates.NSW:
                    state = Api.Bookings.BookingModel.State.Nsw;
                    break;
                case EStates.QLD:
                    state = Api.Bookings.BookingModel.State.Qld;
                    break;
                case EStates.SA:
                    state = Api.Bookings.BookingModel.State.Sa;
                    break;
                case EStates.WA:
                    state = Api.Bookings.BookingModel.State.Wa;
                    break;
            }
            return state;
        }
    }
}
