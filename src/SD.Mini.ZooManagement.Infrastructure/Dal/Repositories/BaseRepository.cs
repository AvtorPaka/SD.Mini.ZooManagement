using System.Transactions;
using SD.Mini.ZooManagement.Application.Contracts.Dal.Interfaces;
using SD.Mini.ZooManagement.Infrastructure.Dal.Infrastructure;

namespace SD.Mini.ZooManagement.Infrastructure.Dal.Repositories;

public abstract class BaseRepository: IDbRepository
{
    protected readonly InMemoryStorage MemoryStorage;

    protected BaseRepository(InMemoryStorage memoryStorage)
    {
        MemoryStorage = memoryStorage;
    }
    
    
    public TransactionScope CreateTransactionScope(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
    {
        return new TransactionScope(
            scopeOption: TransactionScopeOption.Required,
            transactionOptions: new TransactionOptions
            {
                IsolationLevel = isolationLevel,
                Timeout = TimeSpan.FromSeconds(5)
            },
            asyncFlowOption: TransactionScopeAsyncFlowOption.Enabled
        );
    }
}