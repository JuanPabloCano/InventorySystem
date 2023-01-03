using AutoMapper;
using InventorySystem.Domain.models.commons.pagination;
using InventorySystem.Domain.models.sale;
using InventorySystem.Domain.useCases.sale;
using InventorySystem.Infrastructure.DrivenAdapters.sqlServer.context;
using InventorySystem.Infrastructure.DrivenAdapters.sqlServer.product;
using InventorySystem.Infrastructure.DrivenAdapters.sqlServer.sale;
using InventorySystem.Infrastructure.DrivenAdapters.sqlServer.sale.data;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystem.Infrastructure.controllers.sale;

[ApiController]
[Route("api/buy")]
public class SaleController : ControllerBase
{
    private readonly IMapper _mapper;

    public SaleController(IMapper mapper)
    {
        _mapper = mapper;
    }

    private static SaleUseCase CreateUseCase()
    {
        var dataContext = new DataContext();
        var saleAdapter = new SaleAdapter(dataContext);
        var productAdapter = new ProductAdapter(dataContext);
        var saleDetailAdapter = new SaleDetailAdapter(dataContext);
        var saleUseCase = new SaleUseCase(saleAdapter, productAdapter, saleDetailAdapter);
        return saleUseCase;
    }

    [HttpGet]
    public ActionResult<Sale> Get([FromQuery] PaginationQuery paginationQuery)
    {
        var useCase = CreateUseCase();
        var sales = useCase.GetAll(paginationQuery);
        var metadata = new
        {
            paginationQuery.PageNumber,
            paginationQuery.PageSize
        };

        return Ok(new
        {
            metadata,
            sales
        });
    }

    [HttpGet("{id}")]
    public ActionResult<Sale> GetById([FromRoute] Guid id)
    {
        var useCase = CreateUseCase();
        return Ok(useCase.GetById(id));
    }

    [HttpPost]
    public ActionResult Post([FromBody] SaleData saleData)
    {
        var useCase = CreateUseCase();
        var sale = _mapper.Map<Sale>(saleData);
        var newSale = useCase.Create(sale);
        return Created("", new
        {
            mmessage = "Sale created successfully",
            newSale
        });
    }
}