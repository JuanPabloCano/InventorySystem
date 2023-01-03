using AutoMapper;
using InventorySystem.Domain.models.commons.pagination;
using InventorySystem.Domain.models.product;
using InventorySystem.Domain.useCases.product;
using InventorySystem.Infrastructure.DrivenAdapters.sqlServer.context;
using InventorySystem.Infrastructure.DrivenAdapters.sqlServer.product;
using InventorySystem.Infrastructure.DrivenAdapters.sqlServer.product.data;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystem.Infrastructure.controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IMapper _mapper;

    public ProductController(IMapper mapper)
    {
        _mapper = mapper;
    }
    // private readonly ProductUseCase _productUseCase;
    //
    // public ProductController(IMapper mapper, ProductUseCase productUseCase)
    // {
    //     _mapper = mapper;
    //     _productUseCase = productUseCase;
    // }

    private ProductUseCase CreateUseCase()
    {
        var dataContext = new DataContext();
        var productAdapter = new ProductAdapter(dataContext);
        var useCase = new ProductUseCase(productAdapter);
        return useCase;
    }

    [HttpPost]
    public ActionResult Post([FromBody] ProductData productData)
    {
        var useCase = CreateUseCase();
        var product = _mapper.Map<Product>(productData);
        var newProduct = useCase.Create(product);
        return Created("", new { message = "Product created successfully", newProduct });
    }

    [HttpGet]
    public ActionResult<Product> Get([FromQuery] PaginationQuery paginationQuery)
    {
        var useCase = CreateUseCase();
        var products = useCase.GetAll(paginationQuery);
        var metadata = new
        {
            paginationQuery.PageNumber,
            paginationQuery.PageSize,
        };

        return Ok(new
        {
            metadata,
            products
        });
    }

    [HttpGet("{id}")]
    public ActionResult<Product> GetById([FromRoute] Guid id)
    {
        var useCase = CreateUseCase();
        return Ok(useCase.GetById(id));
    }

    [HttpPut("{id}")]
    public ActionResult Put([FromRoute] Guid id, [FromBody] ProductData productData)
    {
        var useCase = CreateUseCase();
        var result = useCase.Update(id, _mapper.Map<Product>(productData));
        var product = _mapper.Map<ProductData>(result);
        return Ok(new { message = "Product updated successfully", product });
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(Guid id)
    {
        var useCase = CreateUseCase();
        useCase.Delete(id);
        return Ok("Product deleted successfully");
    }
}