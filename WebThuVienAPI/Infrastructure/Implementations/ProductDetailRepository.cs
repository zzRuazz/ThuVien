using Common.Abstractions;
using WebThuVienAPI.Infrastructure.Abstractions;
using Models.Entities;
using Models.Filter;
using Models.Common;
using WebThuVienAPI.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace WebThuVienAPI.Infrastructure.Implementations;

/// <inheritdoc/>
internal class ProductDetailRepository : RepositoryBase<ProductDetail>, IProductDetailRepository
{
    public ProductDetailRepository(ApplicationDbContext context, ILogProvider logProvider, IConfiguration configuration) : base(context, logProvider, configuration)
    {
    }

    public async Task<DataPaging<ProductDetail>?> FilterDataPaging(ProductDetailFilter filter)
    {
        try
        {
            filter.Page ??= 0;
            filter.Limit ??= 50;
            int offset = filter.Page.Value * filter.Limit.Value;
            filter.Offset = offset;
            var res = _context.Set<ProductDetail>().Where(x => !string.IsNullOrEmpty(x.Id));

            if (!string.IsNullOrEmpty(filter.ProductPropertyId))
            {
                res = res.Where(x => x.ProductPropertyId.Equals(filter.ProductPropertyId));
            }

            if (!string.IsNullOrEmpty(filter.ProductId))
            {
                res = res.Where(x => x.ProductId.Equals(filter.ProductId));
            }

            if (filter.IsActived != null)
            {
                res = res.Where((x) => x.IsActived);
            }

            var count = await res.CountAsync();
            var result = await res.Skip(offset).Take(filter.Limit.Value).ToListAsync();
            return new DataPaging<ProductDetail> { Data = result, PaginationCount = count };
        }
        catch (Exception ex)
        {
            _logProvider.Error(ex);
        }

        return null;
    }

    public async Task<ProductDetail?> FindProductDetailValue(ProductDetailFilter filter)
    {
        try
        {
            var res = _context.Set<ProductDetail>().Where(x => !string.IsNullOrEmpty(x.Id));

            if (!string.IsNullOrEmpty(filter.ProductPropertyId))
            {
                res = res.Where(x => x.ProductPropertyId.Equals(filter.ProductPropertyId));
            }

            if (!string.IsNullOrEmpty(filter.ProductId))
            {
                res = res.Where(x => x.ProductId.Equals(filter.ProductId));
            }

            var result = await res.FirstOrDefaultAsync();
            return result;
        }
        catch (Exception ex)
        {
            _logProvider.Error(ex);
        }

        return null;
    }
}
