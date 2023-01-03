using InventorySystem.Domain.useCases.gateways;

namespace InventorySystem.Domain.useCases.interfaces;

public interface ISaleMovementUseCase<TEntity, TEntityId> : ICreate<TEntity>, IGet<TEntity, TEntityId>
{
}