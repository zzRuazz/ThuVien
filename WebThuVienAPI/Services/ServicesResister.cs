using WebThuVienAPI.Services.Abstractions;
using WebThuVienAPI.Services.Implementations;

namespace WebThuVienAPI.Services;

public static class ServicesRegister
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddTransient<ICategoryService, CategoryService>();
        services.AddTransient<IManufactureService, ManufactureService>();
        services.AddTransient<IProductDetailService, ProductDetailService>();
        services.AddTransient<IProductImageService, ProductImageService>();
        services.AddTransient<IProductPropertyService, ProductPropertyService>();
        services.AddTransient<IProductService, ProductService>();
    }
}
