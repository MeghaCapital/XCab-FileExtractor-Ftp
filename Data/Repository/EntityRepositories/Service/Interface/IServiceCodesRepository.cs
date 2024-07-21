using Data.Model.ServiceCodes;
using System.Collections.Generic;

namespace Data.Repository.EntityRepositories.Service.Interface
{
    public interface IServiceCodesRepository
    {
        ICollection<ServiceCodeFilter> GetServiceCodesPallets(ShipmentModel.ServiceQuoteRequest serviceCodeRequestModel);
        ICollection<ServiceCodeFilter> GetServiceCodesNonPallets(ShipmentModel.ServiceQuoteRequest serviceCodeRequestModel);
        string GetUrgencyTypeOfServiceCode(string serviceCode);
    }
}
