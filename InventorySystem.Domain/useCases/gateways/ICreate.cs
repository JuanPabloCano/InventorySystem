namespace InventorySystem.Domain.useCases.gateways;

public interface ICreate<TEntity>
{
    TEntity Create(TEntity entity);
}