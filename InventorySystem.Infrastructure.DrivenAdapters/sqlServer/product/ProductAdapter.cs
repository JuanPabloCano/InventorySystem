using InventorySystem.Domain.models.commons.pagination;
using InventorySystem.Domain.models.product;
using InventorySystem.Domain.models.repositories;
using InventorySystem.Infrastructure.DrivenAdapters.sqlServer.context;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.Infrastructure.DrivenAdapters.sqlServer.product;

public class ProductAdapter : IBaseRepository<Product, Guid>
{
    private readonly DataContext _dataContext;

    public ProductAdapter(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public Product Create(Product entity)
    {
        entity.Id = Guid.NewGuid();
        _dataContext.Products?.Add(entity);
        return entity;
    }

    public Product Update(Guid entityId, Product entity)
    {
        var selectedProduct = GetById(entityId);
        selectedProduct.Name = entity.Name;
        selectedProduct.Stock = entity.Stock;
        selectedProduct.Enabled = entity.Enabled;
        selectedProduct.Max = entity.Max;
        selectedProduct.Min = entity.Min;

        _dataContext.Entry(selectedProduct).State = EntityState.Modified;
        return selectedProduct ?? throw new InvalidOperationException();
    }

    public void Delete(Guid entityId)
    {
        var selectedProduct = _dataContext.Products?.FirstOrDefault(product => product.Id == entityId);
        if (selectedProduct is not null)
        {
            _dataContext.Products?.Remove(selectedProduct);
        }
    }

    public List<Product> GetAll(PaginationQuery paginationQuery)
    {
        return (_dataContext.Products ?? throw new InvalidOperationException()).ToList();
    }

    public Product GetById(Guid entityId)
    {
        var selectedProduct = _dataContext.Products?.FirstOrDefault(product => product.Id == entityId);
        return selectedProduct ?? throw new InvalidOperationException();
    }

    public void SaveChanges()
    {
        _dataContext.SaveChanges();
    }
}