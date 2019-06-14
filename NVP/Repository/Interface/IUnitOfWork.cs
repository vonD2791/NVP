using System;

namespace NVP.Repository.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        INPVHistoryRepository NPVHistory { get; }
        INPVHistoryCashFlowRepository NPVHistoryCashFlow { get; }

        int SaveChanges();
    }
}