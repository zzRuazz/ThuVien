using WebThuVienAPI.Abstractions;
using WebThuVienAPI.Infrastructure.Abstractions;
using WebThuVienAPI.Infrastructure.Implementations;
using WebThuVienAPI.Services.Abstractions;
using WebThuVienAPI.Services.Implementations;

namespace WebThuVienAPI.Services;

public static class ServicesResister
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddTransient<ICategoryService, CategoryService>();
        services.AddTransient<IManufactureService, ManufactureService>();
        services.AddTransient<IProductService, ProductService>();
    }
}
