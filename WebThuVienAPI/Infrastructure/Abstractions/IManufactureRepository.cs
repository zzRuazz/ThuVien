using Models.Common;
using Models.Entities;
using Models.Filter;
using Models.ViewModels;

namespace WebThuVienAPI.Infrastructure.Abstractions;

/// <summary>
/// IManufactureRepository
/// </summary>
public interface IManufactureRepository : IGenericRepository<Manufacture>
{
    /// <summary>
    /// GetActiveCategories
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<Manufacture>?> GetActiveManufactures();

    /// <summary>
    /// FilterDataPaging
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    Task<DataPaging<Manufacture>?> FilterDataPaging(ManufactureFilter filter);
}
