using Common.Abstractions;
using WebThuVienAPI.Infrastructure.Abstractions;
using Models.Common;
using Models.Entities;
using Models.Filter;
using WebThuVienAPI.Services.Abstractions;

namespace WebThuVienAPI.Services.Implementations;

public class ProductPropertyService : IProductPropertyService
{
    private readonly IProductPropertyRepository _productPropertyRepository;

    private readonly ICategoryRepository _categoryRepository;

    private readonly ILogProvider _logProvider;

    public ProductPropertyService(IProductPropertyRepository productPropertyRepository, ICategoryRepository categoryRepository, ILogProvider logProvider)
    {
        _productPropertyRepository = productPropertyRepository;
        _categoryRepository = categoryRepository;
        _logProvider = logProvider;
    }

    /// <inheritdoc/>
    public async Task<string> CreateAsync(ProductProperty entity)
    {
        try
        {
            var categoryFind = await _categoryRepository.GetAsync(entity.CategoryId);

            if (categoryFind != null)
            {
                entity.CategoryName = categoryFind.Name;
                var result = await _productPropertyRepository.CreateAsync(entity);
                return result;
            }
        }
        catch (Exception ex)
        {
            _logProvider.Error(ex);
        }

        return string.Empty;
    }

    /// <inheritdoc/>
    public async Task<ProductProperty?> GetAsync(string id)
    {
        try
        {
            var res = await _productPropertyRepository.GetAsync(id);
            return res;
        }
        catch (Exception ex)
        {
            _logProvider.Error(ex);
        }

        return null;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<ProductProperty>?> GetAllAsync()
    {
        try
        {
            var res = await _productPropertyRepository.GetAllAsync();
            return res;
        }
        catch (Exception ex)
        {
            _logProvider.Error(ex);
        }

        return null;
    }

    /// <inheritdoc/>
    public async Task<bool> UpdateAsync(ProductProperty entity)
    {
        try
        {
            var categoryFind = await _categoryRepository.GetAsync(entity.CategoryId);

            if (categoryFind != null)
            {
                entity.CategoryName = categoryFind.Name;
                var res = await _productPropertyRepository.UpdateAsync(entity);
                return res;
            }
        }
        catch (Exception ex)
        {
            _logProvider.Error(ex);
        }

        return false;
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(string id)
    {
        try
        {
            var res = await _productPropertyRepository.DeleteAsync(id);
            return res;
        }
        catch (Exception ex)
        {
            _logProvider.Error(ex);
        }

        return false;
    }

    /// <inheritdoc/>
    public async Task<DataPaging<ProductProperty>?> FilterDataPaging(ProductPropertyFilter filter)
    {
        try
        {
            var res = await _productPropertyRepository.FilterDataPaging(filter);
            return res;
        }
        catch (Exception ex)
        {
            _logProvider.Error(ex);
        }

        return null;
    }
}
