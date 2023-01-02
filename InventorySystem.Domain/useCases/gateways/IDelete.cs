namespace InventorySystem.Domain.useCases.gateways;

public interface IDelete<in TEntityId>
{
    void Delete(TEntityId entityId);
}