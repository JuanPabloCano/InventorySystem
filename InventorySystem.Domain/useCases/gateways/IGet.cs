using InventorySystem.Domain.models.commons.pagination;

namespace InventorySystem.Domain.useCases.gateways;

public interface IGet<TEntity, in TEntityId>
{
    List<TEntity>? GetAll(PaginationQuery paginationQuery);
    TEntity GetById(TEntityId entityId);
}