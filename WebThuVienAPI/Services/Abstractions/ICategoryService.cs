using Models.Common;
using Models.Entities;
using Models.Filter;
using Models.ViewModels;

namespace WebThuVienAPI.Abstractions;

/// <summary>
/// ICategoryService
/// </summary>
public interface ICategoryService
{
    /// <summary>
    /// CreateAsync
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<string> CreateAsync(Category entity);

    /// <summary>
    /// GetAsync
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<Category?> GetAsync(string id);

    /// <summary>
    /// GetAllAsync
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<Category>?> GetAllAsync();

    /// <summary>
    /// UpdateAsync
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<bool> UpdateAsync(Category entity);

    /// <summary>
    /// DeleteAsync
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<bool> DeleteAsync(string id);

    /// <summary>
    /// GetActiveCategories
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<CategoryViewModel>?> GetActiveCategories();

    /// <summary>
    /// GetShowingCategories
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<CategoryViewModel>?> GetShowingCategories();

    /// <summary>
    /// FilterDataPaging
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    Task<DataPaging<Category>?> FilterDataPaging(CategoryFilter filter);
}
