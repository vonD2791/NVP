using NVP.Models;
using NVP.Repository.Interface;

namespace NVP.Repository
{
    public class NPVHistoryRepository : Repository<NPV_HISTORY>, INPVHistoryRepository
    {
        public NPVHistoryRepository(TakeHomeExamEntities context)
            : base(context)
        {

        }
    }
}
