using InventorySystem.Domain.useCases.gateways;

namespace InventorySystem.Domain.useCases.interfaces;

public interface IBaseUseCase<TEntity, in TEntityId> : ICreate<TEntity>, IUpdate<TEntity>, IDelete<TEntityId>,
    IGet<TEntity, TEntityId>
{
}