using AutoMapper;
using InventorySystem.Domain.models.commons.pagination;
using InventorySystem.Domain.models.product;
using InventorySystem.Domain.useCases.interfaces;
using InventorySystem.Infrastructure.DrivenAdapters.sqlServer.product.data;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystem.Infrastructure.controllers.product;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IBaseUseCase<Product, Guid> _baseUseCase;
    private readonly IMapper _mapper;

    public ProductController(IBaseUseCase<Product, Guid> baseUseCase, IMapper mapper)
    {
        _baseUseCase = baseUseCase;
        _mapper = mapper;
    }

    [HttpPost]
    public ActionResult Post([FromBody] ProductData productData)
    {
        var product = _mapper.Map<Product>(productData);
        var newProduct = _baseUseCase.Create(product);
        return Created("", new
        {
            message = "Product created successfully",
            newProduct
        });
    }

    [HttpGet]
    public ActionResult<Product> Get([FromQuery] PaginationQuery paginationQuery)
    {
        var products = _baseUseCase.GetAll(paginationQuery);
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
        return Ok(_baseUseCase.GetById(id));
    }

    [HttpPut("{id}")]
    public ActionResult Put([FromRoute] Guid id, [FromBody] ProductData productData)
    {
        var result = _baseUseCase.Update(id, _mapper.Map<Product>(productData));
        var product = _mapper.Map<ProductData>(result);
        return Ok(new { message = "Product updated successfully", product });
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(Guid id)
    {
        _baseUseCase.Delete(id);
        return Ok("Product deleted successfully");
    }
}