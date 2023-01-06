using InventorySystem.Domain.models.commons.exceptions;
using InventorySystem.Domain.models.commons.pagination;
using InventorySystem.Domain.models.product;
using InventorySystem.Domain.models.repositories;
using InventorySystem.Domain.models.sale;
using InventorySystem.Domain.useCases.interfaces;

namespace InventorySystem.Domain.useCases.sale;

public class SaleUseCase : ISaleMovementUseCase<Sale, Guid>
{
    private readonly ISaleMovementRepository<Sale, Guid>? _saleMovementRepository;
    private readonly IBaseRepository<Product, Guid>? _baseRepository;
    private readonly ISaleDetailRepository<SaleDetail, Guid>? _saleDetailRepository;

    public SaleUseCase(ISaleMovementRepository<Sale, Guid>? saleMovementRepository,
        IBaseRepository<Product, Guid>? baseRepository, ISaleDetailRepository<SaleDetail, Guid>? saleDetailRepository)
    {
        _saleMovementRepository = saleMovementRepository;
        _baseRepository = baseRepository;
        _saleDetailRepository = saleDetailRepository;
    }

    public Sale Create(Sale entity)
    {
        if (entity is null) throw new ArgumentNullException(null, Error.EntityIsRequired);

        _saleMovementRepository?.Create(entity);
        entity.SaleDetails?.ForEach(detail =>
        {
            var selectedProduct = _baseRepository?.GetById(detail.ProductId);
            ProductValidations(selectedProduct, detail);
            selectedProduct!.Stock -= detail.Amount;
            _saleDetailRepository?.Create(detail);
            _baseRepository?.Update(detail.ProductId, selectedProduct);
            _baseRepository?.SaveChanges();
        });
        _saleMovementRepository?.SaveChanges();
        return entity;
    }


    public List<Sale>? GetAll(PaginationQuery paginationQuery)
    {
        return (_saleMovementRepository?.GetAll(paginationQuery) ?? throw new InvalidOperationException())
            .OrderBy(sale => sale.Date)
            .Skip((paginationQuery.PageNumber - 1) * paginationQuery.PageSize)
            .Take(paginationQuery.PageSize)
            .ToList();
    }

    public Sale GetById(Guid entityId)
    {
        return _saleMovementRepository?.GetById(entityId) ?? throw new InvalidOperationException();
    }

    private static void ProductValidations(Product? selectedProduct, SaleDetail detail)
    {
        if (selectedProduct is null) throw new NullReferenceException(Error.ProductNotFound);
        if (selectedProduct.Stock < detail.Amount || selectedProduct.Stock == 0)
            throw new ArgumentException("Amount not valid");
    }
}