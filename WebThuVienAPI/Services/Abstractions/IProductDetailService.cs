using Models.Common;
using Models.Entities;
using Models.Filter;
using Models.Request;
using WebThuVienAPI.Services.Abstractions;

namespace WebThuVienAPI.Services.Abstractions;

public interface IProductDetailService : IBaseService<ProductDetail>
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

    /// <summary>
    /// UpdateProductDetailValue
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<bool> UpdateProductDetailValue(ProductDetailUpdateRequest request);
}
