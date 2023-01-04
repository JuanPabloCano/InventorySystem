using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using InventorySystem.Domain.models.sale;
using InventorySystem.Domain.useCases.interfaces;
using InventorySystem.Infrastructure.controllers.sale;
using InventorySystem.Infrastructure.DrivenAdapters.sqlServer.sale.data;
using InventorySystem.Tests.InventorySystem.Infrastructure.EntryPoints.Tests.controllers.commons.paginationQuery;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystem.Tests.InventorySystem.Infrastructure.EntryPoints.Tests.controllers.sale;

public class SaleControllerTests
{
    private readonly ISaleMovementUseCase<Sale, Guid> _saleMovementUseCase;
    private readonly IMapper _mapper;
    private readonly SaleController _saleController;

    public SaleControllerTests()
    {
        _saleMovementUseCase = A.Fake<ISaleMovementUseCase<Sale, Guid>>();
        _mapper = A.Fake<IMapper>();
        _saleController = new SaleController(_mapper, _saleMovementUseCase);
    }

    [Fact]
    public void SaleController_Get_ReturnsSuccess()
    {
        // Arrange
        var sales = A.Fake<ICollection<SaleData>>();
        var saleList = A.Fake<List<SaleData>>();
        A.CallTo(() => _mapper.Map<List<SaleData>>(sales))
            .Returns(saleList);
        
        // Act
        var result = _saleController.Get(PaginationQueryUtil.PaginationQuery());

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));
    }
    
    [Fact]
    public void SaleController_GetById_ReturnsSuccess()
    {
        // Arrange
        var saleId = new Guid();
        var sale = A.Fake<Sale>();
        A.CallTo(() => _saleMovementUseCase.GetById(saleId))
            .Returns(sale);
        
        // Act
        var result = _saleController.GetById(saleId);
        
        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));
    }

    [Fact]
    public void SaleController_Post_ReturnsCreated()
    {
        // Arrange
        var saleData = A.Fake<SaleData>();
        var sale = A.Fake<Sale>();
        A.CallTo(() => _mapper.Map<SaleData>(saleData))
            .Invokes(() => _saleMovementUseCase.Create(sale));
        
        // Act
        var result = _saleController.Post(saleData);
        
        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(CreatedResult));
    }
}