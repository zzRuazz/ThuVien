using Common.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Models.Common;
using Models.Entities;
using Models.Filter;
using WebThuVienAPI.Infrastructure.Abstractions;
using WebThuVienAPI.Models.Context;

namespace WebThuVienAPI.Infrastructure.Implementations;

/// <inheritdoc/>
internal class ProductPropertyRepository : RepositoryBase<ProductProperty>, IProductPropertyRepository
{
    public ProductPropertyRepository(ApplicationDbContext context, ILogProvider logProvider, IConfiguration configuration) : base(context, logProvider, configuration)
    {
    }

    public async Task<DataPaging<ProductProperty>?> FilterDataPaging(ProductPropertyFilter filter)
    {
        try
        {
            filter.Page ??= 0;
            filter.Limit ??= 50;
            int offset = filter.Page.Value * filter.Limit.Value;
            filter.Offset = offset;
            var res = _context.Set<ProductProperty>().Where(x => !string.IsNullOrEmpty(x.Id));

            if (!string.IsNullOrEmpty(filter.Name))
            {
                res = res.Where(x => x.Name.Contains(filter.Name));
            }

            if (!string.IsNullOrEmpty(filter.CategoryId))
            {
                res = res.Where(x => x.CategoryId.Equals(filter.CategoryId));
            }

            if (filter.IsActived != null)
            {
                res = res.Where((x) => x.IsActived);
            }

            var count = await res.CountAsync();
            var result = await res.Skip(offset).Take(filter.Limit.Value).ToListAsync();
            return new DataPaging<ProductProperty> { Data = result, PaginationCount = count };
        }
        catch (Exception ex)
        {
            _logProvider.Error(ex);
        }

        return null;
    }
}
