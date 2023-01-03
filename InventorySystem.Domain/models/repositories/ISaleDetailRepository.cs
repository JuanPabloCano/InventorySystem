using InventorySystem.Domain.useCases.gateways;

namespace InventorySystem.Domain.models.repositories;

public interface ISaleDetailRepository<TEntity, TEntityId> : ICreate<TEntity>, ITransaction
{
}