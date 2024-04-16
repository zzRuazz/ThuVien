using Models.Common;
using Models.Entities;
using Models.Filter;
using Models.ViewModels;
using WebThuVienAPI.Services.Abstractions;

namespace WebThuVienAPI.Services.Abstractions
{
    /// <summary>
    /// IManufactureService
    /// </summary>
    public interface IManufactureService : IBaseService<Manufacture>
    {
        /// <summary>
        /// FilterDataPaging
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<DataPaging<Manufacture>?> FilterDataPaging(ManufactureFilter filter);

        /// <summary>
        /// GetActiveManufactures
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Manufacture>?> GetActiveManufactures();
    }
}
