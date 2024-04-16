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
    private readonly ICategoryRepository _categoryRepository;

    private readonly IManufactureRepository _manufactureRepository;

    private readonly IProductRepository _productRepository;

    private readonly ILogProvider _logProvider;

    public ProductService(ICategoryRepository categoryRepository, IManufactureRepository manufactureRepository, IProductRepository productRepository, ILogProvider logProvider)
    {
        _categoryRepository = categoryRepository;
        _manufactureRepository = manufactureRepository;
        _productRepository = productRepository;
        _logProvider = logProvider;
    }

    /// <inheritdoc/>
    public async Task<string> CreateAsync(Product entity)
    {
        var categoryFind = await _categoryRepository.GetAsync(entity.CategoryId);
        if (categoryFind != null)
        {
            entity.CategoryName = categoryFind.Name;
        }

        var manufactureFind = await _manufactureRepository.GetAsync(entity.ManufactureId);
        if (manufactureFind != null)
        {
            entity.ManufactureName = manufactureFind.Name;
        }

        var createResult = await _productRepository.CreateAsync(entity);
        return createResult;
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(string id)
    {
        var deleteResult = await _productRepository.DeleteAsync(id);
        return deleteResult;
    }

    public async Task<DataPaging<Product>?> FilterDataPaging(ProductFilter filter)
    {
        var res = await _productRepository.FilterDataPaging(filter);
        return res;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Product>?> GetAllAsync()
    {
        var getAllResult = await _productRepository.GetAllAsync();
        return getAllResult;
    }

    /// <inheritdoc/>
    public async Task<Product?> GetAsync(string id)
    {
        var getOneResult = await _productRepository.GetAsync(id);
        return getOneResult;
    }

    /// <inheritdoc/>
    public async Task<bool> UpdateAsync(Product entity)
    {
        var categoryFind = await _categoryRepository.GetAsync(entity.CategoryId);
        if (categoryFind != null)
        {
            entity.CategoryName = categoryFind.Name;
        }

        var manufactureFind = await _manufactureRepository.GetAsync(entity.ManufactureId);
        if (manufactureFind != null)
        {
            entity.ManufactureName = manufactureFind.Name;
        }

        var updateResult = await _productRepository.UpdateAsync(entity);
        return updateResult;
    }
}
