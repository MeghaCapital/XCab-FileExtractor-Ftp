using System;
using System.Collections.Generic;

namespace Data.Repository.EntityRepositories.FileInfo
{
    interface IXCabBookingFileInformationRepository
    {
        Task<ICollection<XCabBookingFileInformation>> GetXCabBookingFileInformationForStore(int loginId, int stateId, string storeName, DateTime fileDatetime, string jobType, string previousWorkingday);
        ICollection<XCabBookingFileInformation> GetXCabBookingFileInformationForState(int loginId, int stateId, DateTime fileDatetime);
        void Insert(XCabBookingFileInformation XCabBookingFileInformation);
    }
}
