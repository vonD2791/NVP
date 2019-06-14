using NVP.Models;
using NVP.Repository.Interface;

namespace NVP.Repository
{
    public class NPVHistoryCashFlowRepository : Repository<NPV_HISTORY_CASHFLOWS>, INPVHistoryCashFlowRepository
    {
        public NPVHistoryCashFlowRepository(TakeHomeExamEntities context)
            : base(context)
        {

        }
    }
}
