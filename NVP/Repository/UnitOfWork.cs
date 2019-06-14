using NVP.Models;
using NVP.Repository.Interface;

namespace NVP.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private TakeHomeExamEntities _ctx = null;
        private TakeHomeExamEntities _context
        {
            get
            {
                if (_ctx == null)
                {
                    _ctx = new TakeHomeExamEntities();
                }
                return _ctx;
            }
        }

        public UnitOfWork()
        {

        }

        private INPVHistoryRepository _NPVHistory;
        public INPVHistoryRepository NPVHistory
        {
            get
            {
                if (this._NPVHistory == null)
                    this._NPVHistory = new NPVHistoryRepository(this._context);
                return this._NPVHistory;
            }
        }

        private INPVHistoryCashFlowRepository _NPVHistoryCashFlow;
        public INPVHistoryCashFlowRepository NPVHistoryCashFlow
        {
            get
            {
                if (this._NPVHistoryCashFlow == null)
                    this._NPVHistoryCashFlow = new NPVHistoryCashFlowRepository(this._context);
                return this._NPVHistoryCashFlow;
            }
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            if (_ctx != null)
                _ctx.Dispose();
        }
    }
}