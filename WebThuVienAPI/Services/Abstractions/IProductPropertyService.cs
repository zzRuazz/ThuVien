using Models.Common;
using Models.Entities;
using Models.Filter;

namespace WebThuVienAPI.Services.Abstractions;

public interface IProductPropertyService
{

    /// <summary>
    /// CreateAsync
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<string> CreateAsync(ProductProperty entity);

    /// <summary>
    /// GetAsync
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<ProductProperty?> GetAsync(string id);

    /// <summary>
    /// GetAllAsync
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<ProductProperty>?> GetAllAsync();

    /// <summary>
    /// UpdateAsync
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<bool> UpdateAsync(ProductProperty entity);

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
    Task<DataPaging<ProductProperty>?> FilterDataPaging(ProductPropertyFilter filter);
}
