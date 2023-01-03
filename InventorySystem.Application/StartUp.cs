﻿using System.Text.Json.Serialization;
using InventorySystem.Domain.models.product;
using InventorySystem.Domain.models.sale;
using InventorySystem.Infrastructure.DrivenAdapters.sqlServer.product.data;
using InventorySystem.Infrastructure.DrivenAdapters.sqlServer.sale.data;
using Microsoft.OpenApi.Models;

namespace InventorySystem.Application;

public class StartUp
{
    private IConfiguration Configuration { get; }

    public StartUp(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers()
            .AddJsonOptions(opt => opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Inventory System",
                Description = "An ASP.NET Core Web API for managing an inventory system",
            });
        });
        // services.AddTransient<IBaseUseCase<Product, Guid>, ProductUseCase>();
        // services.AddTransient<IBaseRepository<Product, Guid>, ProductAdapter>();
        services.AddMemoryCache();
        services.AddAutoMapper(config =>
        {
            config.CreateMap<Product, ProductData>();
            config.CreateMap<ProductData, Product>();
            config.CreateMap<Sale, SaleData>();
            config.CreateMap<SaleData, Sale>();
            config.CreateMap<SaleDetail, SaleDetailData>();
            config.CreateMap<SaleDetailData, SaleDetail>();
        }, AppDomain.CurrentDomain.GetAssemblies());
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}