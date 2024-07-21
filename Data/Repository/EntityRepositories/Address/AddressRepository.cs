
using Core;
using Data.Model.Address;
using Data.Repository.EntityRepositories;
using Data.Repository.EntityRepositories.Address.Interface;
using System;

namespace Data.Repository.EntityRepositories.Address

{
    /// <summary>
    /// Repository for addresses
    /// </summary>
    public class AddressRepository : IAddressRepository
    {
        /// <summary>
        /// Defined set of rules for Westpoint Auto clean fields
        /// </summary>
        /// <param name="Booking"></param>
        /// <param name="State"></param>
        /// <param name="SplitChar"></param>
        /// <returns></returns>
        public Booking CleanAddressFields(Booking Booking, string State, char SplitChar)
        {
            try
            {
                //Add a space infront to differentiate letter combination in a Suburb Name and state short code EX: "SAMDRUM" and  " SA 5205"
                //it will split in wrong places if use only the State short code to split the strings
                string SplitFlag = " " + State + " ";

                if (string.IsNullOrEmpty(Booking.ToDetail2) || Booking.ToDetail2.Split(SplitChar).GetLength(0) == 1)
                    return Booking;

                //Remove Additional . included by abbreviation
                if (Booking.ToDetail2.Split(SplitChar).GetLength(0) > 4)
                    if (Booking.ToDetail2.ToUpper().Contains("PTY."))
                        Booking.ToDetail2 = Booking.ToDetail2.Replace("Pty.", "Pty").Replace("PTY.", "PTY");

                //Pass the validation for accurate data where suburb and postcodes are not in the Address line 2 and passed in ToSuburb and ToPostcode
                if (Booking.ToDetail1.ToUpper() != Booking.ToDetail2.ToUpper() && Booking.ToDetail2.Split(SplitChar).GetLength(0) == 0)
                    return Booking;

                //Split the Address Line 1 into 4 address lines
                string[] AddressSplit = Booking.ToDetail2.Split(SplitChar);

                if (AddressSplit.GetLength(0) < 4)
                    Array.Resize(ref AddressSplit, 4);

                //Check if Address line 1 Contains ** marks of "PickUp" or "Pick up" which is a field with invalid data if so remove that address line
                if (!string.IsNullOrEmpty(AddressSplit[0]) && (AddressSplit[0].Contains("**")
                    || AddressSplit[0].ToUpper().Contains("PICKUP")
                    || AddressSplit[0].ToUpper().Contains("PICK UP")))
                {
                    for (int iFlag = 0; iFlag <= AddressSplit.GetLength(0) - 2; iFlag++)
                        AddressSplit[iFlag] = AddressSplit[iFlag + 1].Trim();
                }

                //Check if Suburb Contains ** marks which is a field with invalid data if so remove value at Suburb and if Postcode 
                //contains a valus assign that value to Suburb
                if (!string.IsNullOrEmpty(AddressSplit[2]) && AddressSplit[2].Contains("**"))
                {
                    AddressSplit[2] = AddressSplit[3].Trim();
                    AddressSplit[3] = null;
                }

                //Check if Suburb Contains State Short Code if so slipt by the short code and assign first part to suburb 
                //and second part to Postcode while removing State Short Code 
                //Add a space infront to differentiate letter combination in a Suburb Name and state short code EX: "SAMDRUM" and  " SA 5205"
                AddressSplit[2] = string.IsNullOrEmpty(AddressSplit[2]) ? AddressSplit[2] : " " + AddressSplit[2];
                if (!string.IsNullOrEmpty(AddressSplit[2]) && AddressSplit[2].Contains(SplitFlag))
                {
                    AddressSplit[3] = AddressSplit[2].ToString().Replace(SplitFlag, "#").Split('#')[1].Trim();
                    AddressSplit[2] = AddressSplit[2].ToString().Replace(SplitFlag, "#").Split('#')[0].Trim();
                }

                //Check if Postcode Contains State Short Code along with aa  if so slipt by the short code and assign first part to suburb 
                //and second part to Postcode while removing State Short Code 
                //Add a space infront to differentiate letter combination in a Suburb Name and state short code EX: "SAMDRUM" and  " SA 5205"
                AddressSplit[3] = string.IsNullOrEmpty(AddressSplit[3]) ? AddressSplit[3] : " " + AddressSplit[3];
                if (!string.IsNullOrEmpty(AddressSplit[3]) && AddressSplit[3].Contains(SplitFlag))
                {
                    AddressSplit[3] = AddressSplit[3].ToString().Replace(SplitFlag, "#").Split('#')[1].Trim();
                }

                //Check if Postcode is a numerical value
                if (!string.IsNullOrEmpty(AddressSplit[3]) && (!int.TryParse(AddressSplit[3].ToString(), out int Postcode)))
                    AddressSplit[3] = null;

                //Check if Postcode is 4 digits
                if (!string.IsNullOrEmpty(AddressSplit[3]) && AddressSplit[3].ToString().Trim().Length != 4)
                    AddressSplit[3] = null;

                //Re-assing address lines 
                Booking.ToDetail2 = AddressSplit[0] != null ? AddressSplit[0].Trim() : null;
                Booking.ToDetail3 = AddressSplit[1] != null ? AddressSplit[1].Trim() : null;
                Booking.ToSuburb = AddressSplit[2] != null ? (AddressSplit[2].Trim() != "" ? AddressSplit[2].Trim() : null) : null;
                Booking.ToPostcode = AddressSplit[3] != null ? AddressSplit[3].Trim() : null;

                //Remove Client name duplication in ToDetail1 and ToDetail2 if Client name is repeated in ToDetail2 initially
                if (Booking.ToDetail1.ToUpper() == Booking.ToDetail2.ToUpper())
                {
                    Booking.ToDetail2 = Booking.ToDetail3;
                    Booking.ToDetail3 = null;
                }
            }
            catch (Exception)
            { }

            return Booking;
        }

      

        /// <summary>
        /// validate Subub and postcode against a Tpplus alllowed subrbs
        /// </summary>
        /// <param name="booking"></param>
        /// <param name="state"></param>
        /// <param name="failureNote"></param>
        /// <returns></returns>
        public bool ValidateSuburb(Booking booking, string state, out string failureNote)
        {
            failureNote = "";
            try
            {
                if (string.IsNullOrEmpty(booking.ToSuburb) && string.IsNullOrEmpty(booking.ToPostcode))
                {
                    failureNote = "Empty Suburb and Postcodes";
                    return false;
                }

                var XCabClientIntegrationRepository = new XCabClientIntegrationRepository();
                var Suburb = new Suburb();

                if (!string.IsNullOrEmpty(booking.ToSuburb))
                {
                    Suburb = XCabClientIntegrationRepository.ValidateSuburb(booking.ToSuburb, state);
                    if (Suburb != null)
                    {
                        if (Suburb.PostCode == booking.ToPostcode)
                        {
                            return true;
                        }
                        else
                        {
                            booking.ToPostcode = Suburb.PostCode;
                            return true;
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(booking.ToPostcode))
                        {
                            Suburb = XCabClientIntegrationRepository.ValidatePostcode(booking.ToPostcode, state);
                            if (Suburb != null)
                            {
                                booking.ToSuburb = Suburb.Name;
                                return true;
                            }
                            else
                            {
                                failureNote = "Invalid Suburb and Postcode";
                                return false;
                            }
                        }
                        else
                        {
                            failureNote = "Invalid Suburb with an Empty Postcode";
                            return false;
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(booking.ToPostcode))
                    {
                        Suburb = XCabClientIntegrationRepository.ValidatePostcode(booking.ToPostcode, state);
                        if (Suburb == null)
                        {
                            failureNote = "Empty Suburb with an Invalid Postcode";
                            return false;
                        }
                        else
                        {
                            booking.ToSuburb = Suburb.Name;
                            return true;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

       
    }
}

