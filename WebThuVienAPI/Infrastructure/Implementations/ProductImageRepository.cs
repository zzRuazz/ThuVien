using Common.Abstractions;
using Microsoft.EntityFrameworkCore;
using Models.Common;
using Models.Entities;
using Models.Filter;
using WebThuVienAPI.Infrastructure.Abstractions;
using WebThuVienAPI.Models.Context;

namespace WebThuVienAPI.Infrastructure.Implementations;

/// <inheritdoc/>
internal class ProductImageRepository : RepositoryBase<ProductImage>, IProductImageRepository
{
    public ProductImageRepository(ApplicationDbContext context, ILogProvider logProvider, IConfiguration configuration) : base(context, logProvider, configuration)
    {
    }

    public async Task<DataPaging<ProductImage>?> FilterDataPaging(ProductImageFilter filter)
    {
        try
        {
            filter.Page ??= 0;
            filter.Limit ??= 50;
            int offset = filter.Page.Value * filter.Limit.Value;
            filter.Offset = offset;
            var res = _context.Set<ProductImage>().Where(x => !string.IsNullOrEmpty(x.Id));

            if (!string.IsNullOrEmpty(filter.ProductId))
            {
                res = res.Where(x => x.ProductId.Contains(filter.ProductId));
            }

            if (filter.IsActived != null)
            {
                res = res.Where((x) => x.IsActived);
            }

            var count = await res.CountAsync();
            var result = await res.Skip(offset).Take(filter.Limit.Value).OrderByDescending(x => x.CreatedAt).ToListAsync();
            return new DataPaging<ProductImage> { Data = result, PaginationCount = count };
        }
        catch (Exception ex)
        {
            _logProvider.Error(ex);
        }

        return null;
    }

    public async Task<ProductImage?> FindProductImageValue(ProductImageFilter filter)
    {
        try
        {
            filter.Page ??= 0;
            filter.Limit ??= 50;
            int offset = filter.Page.Value * filter.Limit.Value;
            filter.Offset = offset;
            var res = _context.Set<ProductImage>().Where(x => !string.IsNullOrEmpty(x.Id));

            if (!string.IsNullOrEmpty(filter.ProductId))
            {
                res = res.Where(x => x.ProductId.Contains(filter.ProductId));
            }

            if (filter.IsActived != null)
            {
                res = res.Where((x) => x.IsActived);
            }

            var result = await res.OrderByDescending(x => x.CreatedAt).FirstOrDefaultAsync();
            return result;
        }
        catch (Exception ex)
        {
            _logProvider.Error(ex);
        }

        return null;
    }
}
