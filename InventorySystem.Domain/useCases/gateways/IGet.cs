namespace InventorySystem.Domain.useCases.gateways;

public interface IGet<TEntity, in TEntityId>
{
    List<TEntity> GetAll();
    TEntity GetById(TEntityId entityId);
}