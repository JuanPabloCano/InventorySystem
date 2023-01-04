using InventorySystem.Domain.models.commons.pagination;

namespace InventorySystem.Tests.InventorySystem.Infrastructure.EntryPoints.Tests.controllers.commons.paginationQuery;

public abstract class PaginationQueryUtil
{
    public static PaginationQuery PaginationQuery ()
    {
        var paginationQuery = new PaginationQuery()
        {
            PageNumber = 1,
            PageSize = 2
        };
        return paginationQuery;
    }
}