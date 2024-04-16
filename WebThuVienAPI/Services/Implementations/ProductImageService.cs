using Models.Common;
using Models.Entities;
using Models.Filter;
using WebThuVienAPI.Services.Abstractions;
using WebThuVienAPI.Infrastructure.Abstractions;

namespace WebThuVienAPI.Services.Implementations;

public class ProductImageService : IProductImageService
{
    private readonly IProductImageRepository _productImageRepository;

    public ProductImageService(IProductImageRepository productImageRepository)
    {
        _productImageRepository = productImageRepository;
    }

    /// <inheritdoc/>
    public async Task<string> CreateAsync(ProductImage entity)
    {
        var res = await _productImageRepository.CreateAsync(entity);
        return res;
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(string id)
    {
        var res = await _productImageRepository.DeleteAsync(id);
        return res;
    }

    /// <inheritdoc/>
    public async Task<DataPaging<ProductImage>?> FilterDataPaging(ProductImageFilter filter)
    {
        var res = await _productImageRepository.FilterDataPaging(filter);
        return res;
    }

    /// <inheritdoc/>
    public Task<ProductImage?> FindProductImageValue(ProductImageFilter filter)
    {
        var res = _productImageRepository.FindProductImageValue(filter);
        return res;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<ProductImage>?> GetAllAsync()
    {
        var res = await _productImageRepository.GetAllAsync();
        return res;
    }

    /// <inheritdoc/>
    public async Task<ProductImage?> GetAsync(string id)
    {
        var res = await _productImageRepository.GetAsync(id);
        return res;
    }

    /// <inheritdoc/>
    public async Task<bool> UpdateAsync(ProductImage entity)
    {
        var res = await _productImageRepository.UpdateAsync(entity);
        return res;
    }

    /// <inheritdoc/>
    public async Task<bool> UpdateProductImageValue(ProductImage entity)
    {
        var find = await GetAsync(entity.Id);

        if (find != null)
        {
            find.Image = entity.Image;
            find.UpdatedAt = DateTime.Now;
            return await _productImageRepository.UpdateAsync(find);
        }
        else
        {
            entity.CreatedAt = DateTime.Now;
            var res = await _productImageRepository.CreateAsync(entity);
            
            if (!string.IsNullOrEmpty(res))
            {
                return true;
            }
        }

        return false;
    }
}
