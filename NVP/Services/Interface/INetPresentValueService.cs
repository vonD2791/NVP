using NVP.DTO;
using NVP.Requests;
using System.Collections.Generic;

namespace NVP.Services.Interface
{
    public interface INetPresentValueService
    {
        bool AddToHistory(NetPresentValueRequest netPresentValueRequest);
        decimal ComputeNetPresentValue(List<decimal> cashFlows, decimal initialValue, decimal discountRate);

        IEnumerable<NPVDTO> GetNPVDTOHistory();
        IEnumerable<NPVPerDiscountRateDTO> ComputeNetPresentValues(NetPresentValueRequest netPresentValueRequest);

    }
}
