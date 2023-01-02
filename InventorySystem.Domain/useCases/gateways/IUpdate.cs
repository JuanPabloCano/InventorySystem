namespace InventorySystem.Domain.useCases.gateways;

public interface IUpdate<in TEntity>
{
    void Update(TEntity entity);
}