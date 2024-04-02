using Models.Common;
using Models.Entities;
using Models.Filter;

namespace WebThuVienAPI.Abstractions
{
    /// <summary>
    /// IManufactureService
    /// </summary>
    public interface IManufactureService
    {
        /// <summary>
        /// CreateAsync
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> CreateAsync(Manufacture entity);

        /// <summary>
        /// GetAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Manufacture?> GetAsync(string id);

        /// <summary>
        /// GetAllAsync
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Manufacture>?> GetAllAsync();

        /// <summary>
        /// FilterDataPaging
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<DataPaging<Manufacture>?> FilterDataPaging(ManufactureFilter filter);

        /// <summary>
        /// UpdateAsync
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(Manufacture entity);

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(string id);
    }
}
