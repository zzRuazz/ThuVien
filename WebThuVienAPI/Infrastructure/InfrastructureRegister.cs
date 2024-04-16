using WebThuVienAPI.Infrastructure.Abstractions;
using WebThuVienAPI.Infrastructure.Implementations;

namespace WebThuVienAPI.Infrastructure;

public static class InfrastructureRegister
{
    public static void RegisterInfrastructures(this IServiceCollection services)
    {
        services.AddTransient<IAccountRepository, AccountRepository>();
        services.AddTransient<ICategoryRepository, CategoryRepository>();
        services.AddTransient<IManufactureRepository, ManufactureRepository>();
        services.AddTransient<IProductDetailRepository, ProductDetailRepository>();
        services.AddTransient<IProductImageRepository, ProductImageRepository>();
        services.AddTransient<IProductPropertyRepository, ProductPropertyRepository>();
        services.AddTransient<IProductRepository, ProductRepository>();
    }
}
