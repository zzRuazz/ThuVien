using Models.Common;
using Models.Entities;
using Models.Filter;
using WebThuVienAPI.Services.Abstractions;
namespace WebThuVienAPI.Services.Abstractions;

public interface IProductPropertyService : IBaseService<ProductProperty>
{
    /// <summary>
    /// FilterDataPaging
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    Task<DataPaging<ProductProperty>?> FilterDataPaging(ProductPropertyFilter filter);
}
