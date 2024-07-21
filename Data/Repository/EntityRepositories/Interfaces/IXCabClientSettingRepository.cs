using Data.Model;
using System.Collections.Generic;

namespace Data.Repository.EntityRepositories.Interfaces
{
    public interface IXCabClientSettingRepository
    {
        ICollection<XCabClientSetting> GetXCabClientSetting();

        XCabClientSetting GetXCabClientSetting(int ftpLoginId, int state, string accountCode, string serviceCode = null);

        bool IsBarcodeAllowed(int loginId, string accountCode, int stateId);
    }
}
