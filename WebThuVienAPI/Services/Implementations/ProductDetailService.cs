using Common.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Models.Common;
using Models.Entities;
using Models.Filter;
using Models.Request;
using WebThuVienAPI.Services.Abstractions;
using WebThuVienAPI.Infrastructure.Abstractions;
using WebThuVienAPI.Infrastructure.Implementations;

namespace WebThuVienAPI.Services.Implementations;

public class ProductDetailService : IProductDetailService
{
    private readonly IProductDetailRepository _productDetailRepository;

    private readonly IProductPropertyRepository _productPropertyRepository;

    private readonly ILogProvider _logProvider;

    public ProductDetailService(IProductDetailRepository productDetailRepository, IProductPropertyRepository productPropertyRepository, ILogProvider logProvider)
    {
        _productDetailRepository = productDetailRepository;
        _productPropertyRepository = productPropertyRepository;
        _logProvider = logProvider;
    }

    public async Task<string> CreateAsync(ProductDetail entity)
    {
        var res = await _productDetailRepository.CreateAsync(entity);
        return res;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var res = await _productDetailRepository.DeleteAsync(id);
        return res;
    }

    public async Task<DataPaging<ProductDetail>?> FilterDataPaging(ProductDetailFilter filter)
    {
        var res = await _productDetailRepository.FilterDataPaging(filter);
        return res;
    }

    public async Task<ProductDetail?> FindProductDetailValue(ProductDetailFilter filter)
    {
        var res = await _productDetailRepository.FindProductDetailValue(filter);
        return res;
    }

    public async Task<IEnumerable<ProductDetail>?> GetAllAsync()
    {
        var res = await _productDetailRepository.GetAllAsync();
        return res;
    }

    public async Task<ProductDetail?> GetAsync(string id)
    {
        var res = await _productDetailRepository.GetAsync(id);
        return res;
    }

    public async Task<bool> UpdateAsync(ProductDetail entity)
    {
        var res = await _productDetailRepository.UpdateAsync(entity);
        return res;
    }

    public async Task<bool> UpdateProductDetailValue(ProductDetailUpdateRequest request)
    {
        try
        {
            var productPropertyFind = await _productPropertyRepository.GetAsync(request.ProductPropertyId);

            if (productPropertyFind != null)
            {
                var filter = new ProductDetailFilter
                {
                    ProductPropertyId = request.ProductPropertyId,
                    ProductId = request.ProductId
                };

                var find = await FindProductDetailValue(filter);
                var entity = new ProductDetail();

                if (find != null)
                {
                    //update
                    entity = find;
                    entity.Value = request.Value;
                    entity.UpdatedAt = DateTime.Now;
                    return await UpdateAsync(entity);
                }
                else
                {
                    //insert
                    entity.Id = Guid.NewGuid().ToString();
                    entity.Value = request.Value;
                    entity.ProductId = request.ProductId;
                    entity.ProductPropertyId = request.ProductPropertyId;
                    entity.ProductProperty = productPropertyFind.Name;
                    entity.CategoryId = productPropertyFind.CategoryId;
                    entity.CreatedAt = DateTime.Now;
                    entity.IsActived = true;
                    entity.IsDeleted = false;
                    var result = await CreateAsync(entity);

                    if (!string.IsNullOrEmpty(result))
                    {
                        return true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            _logProvider.Error(ex);
        }

        return false;
    }
}
