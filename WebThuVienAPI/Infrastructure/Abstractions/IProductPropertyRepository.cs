using Models.Common;
using Models.Entities;
using Models.Filter;

namespace WebThuVienAPI.Infrastructure.Abstractions;

/// <summary>
/// IProductPropertyRepository
/// </summary>
public interface IProductPropertyRepository : IGenericRepository<ProductProperty>
{
    /// <summary>
    /// FilterDataPaging
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    Task<DataPaging<ProductProperty>?> FilterDataPaging(ProductPropertyFilter filter);
}
