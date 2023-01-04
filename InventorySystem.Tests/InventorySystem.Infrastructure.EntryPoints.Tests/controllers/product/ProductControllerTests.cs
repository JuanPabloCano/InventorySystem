using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using InventorySystem.Domain.models.product;
using InventorySystem.Domain.useCases.interfaces;
using InventorySystem.Infrastructure.controllers.product;
using InventorySystem.Infrastructure.DrivenAdapters.sqlServer.product.data;
using InventorySystem.Tests.InventorySystem.Infrastructure.EntryPoints.Tests.controllers.commons.paginationQuery;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystem.Tests.InventorySystem.Infrastructure.EntryPoints.Tests.controllers.product;

public class ProductControllerTests
{
    private readonly IBaseUseCase<Product, Guid> _baseUseCase;
    private readonly IMapper _mapper;
    private readonly ProductController _productController;
    
    public ProductControllerTests()
    {
        _baseUseCase = A.Fake<IBaseUseCase<Product, Guid>>();
        _mapper = A.Fake<IMapper>();
        _productController = new ProductController(_baseUseCase, _mapper);
    }

    [Fact]
    public void ProductController_Get_ReturnsSuccess()
    {
        // Arrange
        var products = A.Fake<ICollection<ProductData>>();
        var productList = A.Fake<List<ProductData>>();
        A.CallTo(() => _mapper.Map<List<ProductData>>(products))
            .Returns(productList);

        // Act
        var result = _productController.Get(PaginationQueryUtil.PaginationQuery());

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));
    }

    [Fact]
    public void ProductController_GetById_ReturnsSuccess()
    {
        // Arrange
        var productId = new Guid();
        var product = A.Fake<Product>();
        A.CallTo(() => _baseUseCase.GetById(productId))
            .Returns(product);
        
        // Act
        var result = _productController.GetById(productId);
        
        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));
    }

    [Fact]
    public void ProductController_Post_ReturnsCreated()
    {
        // Arrange
        var productData = A.Fake<ProductData>();
        var product = A.Fake<Product>();
        A.CallTo(() => _mapper.Map<ProductData>(productData))
            .Invokes(() => _baseUseCase.Create(product));
        
        // Act
        var result = _productController.Post(productData);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(CreatedResult));
    }
}