using InventorySystem.Domain.useCases.gateways;

namespace InventorySystem.Domain.models.repositories;

public interface ISaleMovementRepository<TEntity, in TEntityId> : ICreate<TEntity>, IGet<TEntity, TEntityId>, ITransaction
{
}