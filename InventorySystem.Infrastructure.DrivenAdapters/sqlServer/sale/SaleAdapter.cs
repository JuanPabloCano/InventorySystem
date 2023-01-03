using InventorySystem.Domain.models.commons.pagination;
using InventorySystem.Domain.models.repositories;
using InventorySystem.Domain.models.sale;
using InventorySystem.Infrastructure.DrivenAdapters.sqlServer.context;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.Infrastructure.DrivenAdapters.sqlServer.sale;

public class SaleAdapter : ISaleMovementRepository<Sale, Guid>
{
    private readonly DataContext? _dataContext;

    public SaleAdapter(DataContext? dataContext)
    {
        _dataContext = dataContext;
    }

    public Sale Create(Sale entity)
    {
        entity.SaleId = new Guid();
        entity.Date = DateTime.Now;
        _dataContext?.Sales?.Add(entity);
        return entity;
    }

    public List<Sale>? GetAll(PaginationQuery paginationQuery)
    {
        
        return (_dataContext?.Sales ?? throw new InvalidOperationException())
            .Include(s => s.SaleDetails)
            .ToList();
    }

    public Sale GetById(Guid entityId)
    {
        return _dataContext?.Sales?
                   .Include(s => s.SaleDetails)
                   .FirstOrDefault(sale => sale.SaleId == entityId) ??
               throw new InvalidOperationException();
    }

    public void SaveChanges()
    {
        _dataContext?.SaveChanges();
    }
}