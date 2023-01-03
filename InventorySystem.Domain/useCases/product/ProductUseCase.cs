using InventorySystem.Domain.models.commons.pagination;
using InventorySystem.Domain.models.product;
using InventorySystem.Domain.models.repositories;
using InventorySystem.Domain.useCases.interfaces;

namespace InventorySystem.Domain.useCases.product;

public class ProductUseCase : IBaseUseCase<Product, Guid>
{
    private const string ErrorMessage = "The Product is required";
    private readonly IBaseRepository<Product, Guid> _baseRepository;

    public ProductUseCase(IBaseRepository<Product, Guid> baseRepository)
    {
        _baseRepository = baseRepository;
    }

    public Product Create(Product entity)
    {
        if (entity is null) throw new ArgumentNullException(ErrorMessage);
        var productResult = _baseRepository.Create(entity);
        _baseRepository.SaveChanges();
        return entity;
    }

    public Product Update(Guid entityId, Product entity)
    {
        var selectedProduct = this.GetById(entityId);
        if (selectedProduct is null) throw new ArgumentNullException(ErrorMessage);

        _baseRepository.Update(entityId, entity);
        _baseRepository.SaveChanges();
        return selectedProduct;
    }

    public void Delete(Guid entityId)
    {
        _baseRepository.Delete(entityId);
        _baseRepository.SaveChanges();
    }

    public List<Product> GetAll(PaginationQuery paginationQuery)
    {
        return _baseRepository.GetAll(paginationQuery)
            .OrderBy(product => product.Name)
            .Skip((paginationQuery.PageNumber - 1) * paginationQuery.PageSize)
            .Take(paginationQuery.PageSize)
            .ToList();
    }

    public Product GetById(Guid entityId)
    {
        return _baseRepository.GetById(entityId);
    }
}