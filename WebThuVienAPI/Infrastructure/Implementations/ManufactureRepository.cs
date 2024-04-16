using Common.Abstractions;
using Models.Entities;
using Models.Common;
using Models.Filter;
using Models.ViewModels;
using WebThuVienAPI.Infrastructure.Abstractions;
using WebThuVienAPI.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace WebThuVienAPI.Infrastructure.Implementations;

/// <inheritdoc/>
internal class ManufactureRepository : RepositoryBase<Manufacture>, IManufactureRepository
{
    public ManufactureRepository(ApplicationDbContext context, ILogProvider logProvider, IConfiguration configuration) : base(context, logProvider, configuration)
    {
    }

    public async Task<DataPaging<Manufacture>?> FilterDataPaging(ManufactureFilter filter)
    {
        try
        {
            filter.Page ??= 0;
            filter.Limit ??= 50;
            int offset = filter.Page.Value * filter.Limit.Value;
            filter.Offset = offset;
            var res = _context.Set<Manufacture>().Where(x => !string.IsNullOrEmpty(x.Id));

            if (!string.IsNullOrEmpty(filter.Name))
            {
                res = res.Where(x => x.Name.Contains(filter.Name));
            }

            if (!string.IsNullOrEmpty(filter.Website))
            {
                res = res.Where(x => x.Website.Contains(filter.Website));
            }

            if (filter.IsActived != null)
            {
                res = res.Where((x) => x.IsActived);
            }

            var count = await res.CountAsync();
            var result = await res.Skip(offset).Take(filter.Limit.Value).ToListAsync();
            return new DataPaging<Manufacture> { Data = result, PaginationCount = count };
        }
        catch (Exception ex)
        {
            _logProvider.Error(ex);
        }

        return null;
    }

    public async Task<IEnumerable<Manufacture>?> GetActiveManufactures()
    {
        try
        {
            var res = await _context.Set<Manufacture>().Where(x => x.IsActived).ToListAsync();
            return res;
        }
        catch (Exception ex)
        {
            _logProvider.Error(ex);
        }

        return null;
    }
}
