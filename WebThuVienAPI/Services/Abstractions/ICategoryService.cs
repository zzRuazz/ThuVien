using Models.Common;
using Models.Entities;
using Models.Filter;
using Models.ViewModels;
using WebThuVienAPI.Services.Abstractions;

namespace WebThuVienAPI.Services.Abstractions;

/// <summary>
/// ICategoryService
/// </summary>
public interface ICategoryService : IBaseService<Category>
{
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
