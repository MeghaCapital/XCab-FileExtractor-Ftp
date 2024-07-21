using System;

namespace Core.Models.Bbq
{
    [Serializable]
    public class BbqBookingModel : Booking
    {
        /// <summary>
        /// Assembly Instructions are used for the generation of Scram file when extracting
        /// information from BBQs Galore file
        /// </summary>
        public string BbqAssemblyInstructions { get; set; }

        public bool AssemblyRequired { get; set; } = false;
    }
}
