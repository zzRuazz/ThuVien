using Models.Common;
using Models.Entities;
using Models.Filter;

namespace WebThuVienAPI.Infrastructure.Abstractions;

/// <summary>
/// IProductRepository
/// </summary>
public interface IProductRepository : IGenericRepository<Product>
{
    /// <summary>
    /// FilterDataPaging
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    Task<DataPaging<Product>?> FilterDataPaging(ProductFilter filter);
}
