using Models.Common;
using Models.Entities;
using Models.Filter;

namespace WebThuVienAPI.Infrastructure.Abstractions;

/// <summary>
/// IManufactureRepository
/// </summary>
public interface IManufactureRepository : IGenericRepository<Manufacture>
{
    /// <summary>
    /// FilterDataPaging
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    Task<DataPaging<Manufacture>?> FilterDataPaging(ManufactureFilter filter);
}
