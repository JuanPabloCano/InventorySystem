namespace InventorySystem.Domain.useCases.gateways;

public interface IUpdate<in TEntityId, TEntity>
{
    TEntity Update(TEntityId entityId, TEntity entity);
}