using AutoMapper;
using InventorySystem.Domain.models.commons.pagination;
using InventorySystem.Domain.models.sale;
using InventorySystem.Domain.useCases.interfaces;
using InventorySystem.Infrastructure.DrivenAdapters.sqlServer.sale.data;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystem.Infrastructure.controllers.sale;

[EnableCors]
[ApiController]
[Route("api/buy")]
public class SaleController : ControllerBase
{
    private readonly ISaleMovementUseCase<Sale, Guid> _saleMovementUseCase;
    private readonly IMapper _mapper;
    private const string SaleCreatedSuccessfully = "Sale created successfully";

    public SaleController(IMapper mapper, ISaleMovementUseCase<Sale, Guid> saleMovementUseCase)
    {
        _mapper = mapper;
        _saleMovementUseCase = saleMovementUseCase;
    }

    [HttpGet]
    public IActionResult Get([FromQuery] PaginationQuery paginationQuery)
    {
        var sales = _saleMovementUseCase.GetAll(paginationQuery);
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
    public IActionResult GetById([FromRoute] Guid id)
    {
        return Ok(_saleMovementUseCase.GetById(id));
    }

    [HttpPost]
    public IActionResult Post([FromBody] SaleData saleData)
    {
        try
        {
            var sale = _mapper.Map<Sale>(saleData);
            var newSale = _saleMovementUseCase.Create(sale);
            
            return Created("", new
            {
                message = SaleCreatedSuccessfully,
                newSale
            });
        }
        catch (Exception e)
        {
            return BadRequest(
                new
                {
                    Error = "Error",
                    e.Message
                });
        }
    }
}