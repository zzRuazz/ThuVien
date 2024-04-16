using Models.Common;
using Models.Entities;
using Models.Filter;

namespace WebThuVienAPI.Infrastructure.Abstractions;

/// <summary>
/// IProductImageRepository
/// </summary>
public interface IProductImageRepository : IGenericRepository<ProductImage>
{
    /// <summary>
    /// FilterDataPaging
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    Task<DataPaging<ProductImage>?> FilterDataPaging(ProductImageFilter filter);

    /// <summary>
    /// FindProductImageValue
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    Task<ProductImage?> FindProductImageValue(ProductImageFilter filter);
}
