using InventorySystem.Domain.useCases.gateways;

namespace InventorySystem.Domain.models.repositories;

public interface IBaseRepository<TEntity, in TEntityId> : ICreate<TEntity>, IUpdate<TEntityId, TEntity>, IDelete<TEntityId>,
    IGet<TEntity, TEntityId>, ITransaction
{
}