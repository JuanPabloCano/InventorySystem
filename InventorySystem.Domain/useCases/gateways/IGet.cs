using InventorySystem.Domain.models.commons.pagination;
using InventorySystem.Domain.models.product;

namespace InventorySystem.Domain.useCases.gateways;

public interface IGet<TEntity, in TEntityId>
{
    List<Product> GetAll(PaginationQuery paginationQuery);
    TEntity GetById(TEntityId entityId);
}