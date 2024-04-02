using Models.Common;
using Models.Entities;
using Models.Filter;

namespace WebThuVienAPI.Infrastructure.Abstractions;

/// <summary>
/// ICategoryRepository
/// </summary>
public interface ICategoryRepository : IGenericRepository<Category>
{
    /// <summary>
    /// GetActiveCategories
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<Category>?> GetActiveParentCategories();

    /// <summary>
    /// GetActiveChildrenCategoriesByParentId
    /// </summary>
    /// <param name="parentId"></param>
    /// <returns></returns>
    Task<IEnumerable<Category>?> GetActiveChildrenCategoriesByParentId(string parentId);

    /// <summary>
    /// FilterDataPaging
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    Task<DataPaging<Category>?> FilterDataPaging(CategoryFilter filter);
}
