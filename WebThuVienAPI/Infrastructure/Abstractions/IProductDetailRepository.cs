using Models.Common;
using Models.Entities;
using Models.Filter;

namespace WebThuVienAPI.Infrastructure.Abstractions;

/// <summary>
/// IProductDetailRepository
/// </summary>
public interface IProductDetailRepository : IGenericRepository<ProductDetail>
{
    /// <summary>
    /// FilterDataPaging
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    Task<DataPaging<ProductDetail>?> FilterDataPaging(ProductDetailFilter filter);

    /// <summary>
    /// FindProductDetailValue
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    Task<ProductDetail?> FindProductDetailValue(ProductDetailFilter filter);
}
