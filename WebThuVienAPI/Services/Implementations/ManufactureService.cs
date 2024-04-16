using WebThuVienAPI.Infrastructure.Abstractions;
using Models.Common;
using Models.Entities;
using Models.Filter;
using WebThuVienAPI.Services.Abstractions;
using Models.ViewModels;

namespace WebThuVienAPI.Services.Implementations;

/// <inheritdoc/>
internal class ManufactureService : IManufactureService
{
    private readonly IManufactureRepository _manufactureRepository;

    public ManufactureService(IManufactureRepository manufactureRepository)
    {
        _manufactureRepository = manufactureRepository;
    }

    /// <inheritdoc/>
    public async Task<string> CreateAsync(Manufacture entity)
    {
        var createResult = await _manufactureRepository.CreateAsync(entity);
        return createResult;
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(string id)
    {
        var deleteResult = await _manufactureRepository.DeleteAsync(id);
        return deleteResult;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Manufacture>?> GetAllAsync()
    {
        var getAllResult = await _manufactureRepository.GetAllAsync();
        return getAllResult;
    }

    /// <inheritdoc/>
    public async Task<Manufacture?> GetAsync(string id)
    {
        var getOneResult = await _manufactureRepository.GetAsync(id);
        return getOneResult;
    }

    public async Task<DataPaging<Manufacture>?> FilterDataPaging(ManufactureFilter filter)
    {
        var result = await _manufactureRepository.FilterDataPaging(filter);
        return result;
    }

    /// <inheritdoc/>
    public async Task<bool> UpdateAsync(Manufacture entity)
    {
        var updateResult = await _manufactureRepository.UpdateAsync(entity);
        return updateResult;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Manufacture>?> GetActiveManufactures()
    {
        var result = await _manufactureRepository.GetActiveManufactures();
        return result;
    }
}
