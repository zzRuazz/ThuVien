using Models.Common;
using Models.Entities;
using Models.Filter;
using WebThuVienAPI.Services.Abstractions;

namespace WebThuVienAPI.Services.Abstractions;

/// <summary>
/// IProductService
/// </summary>
public interface IProductService : IBaseService<Product>
{
    /// <summary>
    /// FilterDataPaging
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    Task<DataPaging<Product>?> FilterDataPaging(ProductFilter filter);
}
