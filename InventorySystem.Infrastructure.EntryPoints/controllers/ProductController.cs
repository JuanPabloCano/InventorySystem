using AutoMapper;
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
    private readonly ProductUseCase _productUseCase;

    public ProductController(IMapper mapper, ProductUseCase productUseCase)
    {
        _mapper = mapper;
        _productUseCase = productUseCase;
    }

    // private ProductUseCase CreateUseCase()
    // {
    //     var dataContext = new DataContext();
    //     var productAdapter = new ProductAdapter(dataContext);
    //     var useCase = new ProductUseCase(productAdapter);
    //     return useCase;
    // }

    [HttpPost]
    public ActionResult Post([FromBody] ProductData productData)
    {
        // var useCase = CreateUseCase();
        var product = _mapper.Map<Product>(productData);
        var newProduct = _productUseCase.Create(product);
        return Created("", new { message = "Product created successfully", newProduct });
    }

    [HttpGet]
    public ActionResult<List<Product>> Get()
    {
        //var useCase = CreateUseCase();
        return Ok(_productUseCase.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult<Product> GetById([FromRoute] Guid id)
    {
        //var useCase = CreateUseCase();
        return Ok(_productUseCase.GetById(id));
    }
}