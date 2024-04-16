using Common.Abstractions;
using Microsoft.EntityFrameworkCore;
using Models.Common;
using Models.Entities;
using Models.Filter;
using WebThuVienAPI.Infrastructure.Abstractions;
using WebThuVienAPI.Models.Context;

namespace WebThuVienAPI.Infrastructure.Implementations;

/// <inheritdoc/>
internal class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
{
    public CategoryRepository(ApplicationDbContext context, ILogProvider logProvider, IConfiguration configuration) : base(context, logProvider, configuration)
    {
    }

    public async Task<DataPaging<Category>?> FilterDataPaging(CategoryFilter filter)
    {
        try
        {
            filter.Page ??= 0;
            filter.Limit ??= 50;
            int offset = filter.Page.Value * filter.Limit.Value;
            filter.Offset = offset;
            var res = _context.Set<Category>().Where(x => !string.IsNullOrEmpty(x.Id));

            if (!string.IsNullOrEmpty(filter.Name))
            {
                res = res.Where(x => x.Name.Contains(filter.Name));
            }

            if (filter.IsActived != null)
            {
                res = res.Where((x) => x.IsActived);
            }

            var count = await res.CountAsync();
            var result = await res.Skip(offset).Take(filter.Limit.Value).OrderByDescending(x => x.CreatedAt).ToListAsync();
            return new DataPaging<Category> { Data = result, PaginationCount = count };
        }
        catch (Exception ex)
        {
            _logProvider.Error(ex);
        }

        return null;
    }

    public async Task<IEnumerable<Category>?> GetActiveChildrenCategoriesByParentId(string parentId)
    {
        try
        {
            var res = await _context.Set<Category>().Where(x => x.ParentId.Equals(parentId)).OrderByDescending(x => x.CreatedAt).ToListAsync();
            return res;
        }
        catch (Exception ex)
        {
            _logProvider.Error(ex);
        }

        return null;
    }

    public async Task<IEnumerable<Category>?> GetActiveParentCategories()
    {
        try
        {
            var res = await _context.Set<Category>().Where(x => string.IsNullOrEmpty(x.ParentId) && x.IsActived).OrderByDescending(x => x.CreatedAt).ToListAsync();
            return res;
        }
        catch (Exception ex)
        {
            _logProvider.Error(ex);
        }

        return null;
    }
}
