using System.Text.Json.Serialization;
using InventorySystem.Domain.models.product;
using InventorySystem.Domain.models.repositories;
using InventorySystem.Domain.useCases.interfaces;
using InventorySystem.Domain.useCases.product;
using InventorySystem.Infrastructure.DrivenAdapters.sqlServer.product;
using InventorySystem.Infrastructure.DrivenAdapters.sqlServer.product.data;
using Microsoft.OpenApi.Models;

namespace InventorySystem.Infrastructure;

public class StartUp
{
    private static readonly string SofkaInventorySystem = "Sofka Inventory System";
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
            var groupName = "V1";
            options.SwaggerDoc(groupName, new OpenApiInfo
            {
                Title = $"Inventory System {groupName}",
                Version = groupName,
                Description = "Sofka BOT Challenge"
            });
        });
        services.AddTransient<IBaseUseCase<Product, Guid>, ProductUseCase>();
        services.AddTransient<IBaseRepository<Product, Guid>, ProductAdapter>();
        services.AddMemoryCache();
        services.AddAutoMapper(config =>
        {
            config.CreateMap<Product, ProductData>();
            config.CreateMap<ProductData, Product>();
        }, AppDomain.CurrentDomain.GetAssemblies());
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseSwagger();
        app.UseSwaggerUI(c => { c.SwaggerEndpoint("/", SofkaInventorySystem); });

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}