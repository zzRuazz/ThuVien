using Models.Common;
using Models.Entities;
using Models.Filter;

namespace WebThuVienAPI.Services.Abstractions;

/// <summary>
/// IProductService
/// </summary>
public interface IProductService
{
    /// <summary>
    /// CreateAsync
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<int> CreateAsync(Product entity);

    /// <summary>
    /// GetAsync
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<Product?> GetAsync(string id);

    /// <summary>
    /// GetAllAsync
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<Product>?> GetAllAsync();

    /// <summary>
    /// UpdateAsync
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<bool> UpdateAsync(Product entity);

    /// <summary>
    /// DeleteAsync
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<bool> DeleteAsync(string id);

    /// <summary>
    /// FilterDataPaging
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    Task<DataPaging<Product>?> FilterDataPaging(ProductFilter filter);
}