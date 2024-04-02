using Common.Abstractions;
using WebThuVienAPI.Infrastructure.Abstractions;
using Models.Common;
using Models.Entities;
using Models.Filter;
using WebThuVienAPI.Services.Abstractions;

namespace WebThuVienAPI.Services.Implementations;

/// <inheritdoc/>
internal class ProductService : IProductService
{
    /// <summary>
    /// IUnitOfWork
    /// </summary>
    private readonly IUnitOfWork _unitOfWork;

    private readonly ILogProvider _logProvider;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="unitOfWork"></param>
    /// <param name="hashProvider"></param>
    /// <param name="jwtProvider"></param>
    public ProductService(IUnitOfWork unitOfWork, ILogProvider logProvider)
    {
        _unitOfWork = unitOfWork;
        _logProvider = logProvider;
    }

    #region Product
    /// <inheritdoc/>
    public async Task<int> CreateAsync(Product entity)
    {
        var createResult = await _unitOfWork.Product.CreateAsync(entity);
        return createResult;
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(string id)
    {
        var deleteResult = await _unitOfWork.Product.DeleteAsync(id);
        return deleteResult;
    }

    public async Task<DataPaging<Product>?> FilterDataPaging(ProductFilter filter)
    {
        var res = await _unitOfWork.Product.FilterDataPaging(filter);
        return res;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Product>?> GetAllAsync()
    {
        var getAllResult = await _unitOfWork.Product.GetAllAsync();
        return getAllResult;
    }

    /// <inheritdoc/>
    public async Task<Product?> GetAsync(string id)
    {
        var getOneResult = await _unitOfWork.Product.GetAsync(id);
        return getOneResult;
    }

    /// <inheritdoc/>
    public async Task<bool> UpdateAsync(Product entity)
    {
        var updateResult = await _unitOfWork.Product.UpdateAsync(entity);
        return updateResult;
    }
    #endregion
}
