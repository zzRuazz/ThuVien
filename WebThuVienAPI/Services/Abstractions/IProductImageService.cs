using Models.Common;
using Models.Entities;
using Models.Filter;
using Models.Request;

namespace WebThuVienAPI.Services.Abstractions;

public interface IProductImageService : IBaseService<ProductImage>
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

    /// <summary>
    /// UpdateProductImageValue
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<bool> UpdateProductImageValue(ProductImage entity);
}
