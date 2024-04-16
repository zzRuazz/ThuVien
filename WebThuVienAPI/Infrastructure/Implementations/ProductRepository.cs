using Common.Abstractions;
using Models.Entities;
using Models.Common;
using Models.Filter;
using WebThuVienAPI.Infrastructure.Abstractions;
using WebThuVienAPI.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace WebThuVienAPI.Infrastructure.Implementations;

internal class ProductRepository : RepositoryBase<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext context, ILogProvider logProvider, IConfiguration configuration) : base(context, logProvider, configuration)
    {
    }

    public async Task<DataPaging<Product>?> FilterDataPaging(ProductFilter filter)
    {
        try
        {
            filter.Page ??= 0;
            filter.Limit ??= 50;
            int offset = filter.Page.Value * filter.Limit.Value;
            var res = _context.Set<Product>().Where(x => !string.IsNullOrEmpty(x.Id));

            if (!string.IsNullOrEmpty(filter.Name))
            {
                res = res.Where(x => x.Name.Contains(filter.Name));
            }

            if (!string.IsNullOrEmpty(filter.CategoryId))
            {
                res = res.Where(x => x.CategoryId.Equals(filter.CategoryId));
            }

            if (!string.IsNullOrEmpty(filter.CategoryName))
            {
                res = res.Where(x => x.CategoryName.Contains(filter.CategoryName));
            }

            if (!string.IsNullOrEmpty(filter.ManufactureId))
            {
                res = res.Where(x => x.ManufactureId.Equals(filter.ManufactureId));
            }

            if (!string.IsNullOrEmpty(filter.ManufactureName))
            {
                res = res.Where(x => x.ManufactureName.Contains(filter.ManufactureName));
            }

            if (filter.UpPrice != null)
            {
                res = res.Where(x => x.Price <= filter.UpPrice);
            }

            if (filter.DownPrice != null)
            {
                res = res.Where(x => x.Price >= filter.DownPrice);
            }

            if (filter.IsActived != null)
            {
                res = res.Where((x) => x.IsActived);
            }

            var count = await res.CountAsync();
            var result = await res.Skip(offset).Take(filter.Limit.Value).ToListAsync();
            return new DataPaging<Product> { Data = result, PaginationCount = count };
        }
        catch (Exception ex)
        {
            _logProvider.Error(ex);
        }

        return null;
    }
}
