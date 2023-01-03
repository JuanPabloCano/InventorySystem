using InventorySystem.Domain.models.repositories;
using InventorySystem.Domain.models.sale;
using InventorySystem.Infrastructure.DrivenAdapters.sqlServer.context;

namespace InventorySystem.Infrastructure.DrivenAdapters.sqlServer.sale;

public class SaleDetailAdapter : ISaleDetailRepository<SaleDetail, Guid>
{
    private readonly DataContext _dataContext;

    public SaleDetailAdapter(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public SaleDetail Create(SaleDetail entity)
    {
        _dataContext.SaleDetails?.Add(entity);
        return entity;
    }

    public void SaveChanges()
    {
        _dataContext.SaveChanges();
    }
}