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
    private const string ProductSavedSuccessfully = "Product saved successfully";

    private const string ProductDeletedSuccessfully = "Product deleted successfully";

    public ProductController(IBaseUseCase<Product, Guid> baseUseCase, IMapper mapper)
    {
        _baseUseCase = baseUseCase;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult Post([FromBody] ProductData productData)
    {
        try
        {
            var product = _mapper.Map<Product>(productData);
            var newProduct = _baseUseCase.Create(product);
            return Created("", new
            {
                message = ProductSavedSuccessfully,
                newProduct
            });
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpGet]
    public IActionResult Get([FromQuery] PaginationQuery paginationQuery)
    {
        var products = _baseUseCase.GetAll(paginationQuery);
        var metadata = new
        {
            paginationQuery.PageNumber,
            paginationQuery.PageSize,
        };

        return Ok(products);
    }

    [HttpGet("{id}")]
    public IActionResult GetById([FromRoute] Guid id)
    {
        return Ok(_baseUseCase.GetById(id));
    }

    [HttpPut("{id}")]
    public IActionResult Put([FromRoute] Guid id, [FromBody] ProductData productData)
    {
        var result = _baseUseCase.Update(id, _mapper.Map<Product>(productData));
        var product = _mapper.Map<ProductData>(result);
        return Ok(new { message = ProductSavedSuccessfully, product });
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        _baseUseCase.Delete(id);
        return Ok(ProductDeletedSuccessfully);
    }
}