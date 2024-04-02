using Common.Abstractions;
using WebThuVienAPI.Infrastructure.Abstractions;
using Models.Common;
using Models.Entities;
using Models.Filter;
using WebThuVienAPI.Services.Abstractions;

namespace WebThuVienAPI.Services.Implementations;

public class ProductPropertyService : IProductPropertyService
{
    /// <summary>
    /// IUnitOfWork
    /// </summary>
    private readonly IUnitOfWork _unitOfWork;

    private readonly ILogProvider _logProvider;

    public ProductPropertyService(IUnitOfWork unitOfWork, ILogProvider logProvider)
    {
        _unitOfWork = unitOfWork;
        _logProvider = logProvider;
    }

    /// <inheritdoc/>
    public async Task<string> CreateAsync(ProductProperty entity)
    {
        try
        {
            var categoryFind = await _unitOfWork.Category.GetAsync(entity.CategoryId);

            if (categoryFind != null)
            {
                entity.CategoryName = categoryFind.Name;
                var result = await _unitOfWork.ProductProperty.CreateAsync(entity);

                if (result > 0)
                {
                    return entity.Id;
                }
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
            var res = await _unitOfWork.ProductProperty.GetAsync(id);
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
            var res = await _unitOfWork.ProductProperty.GetAllAsync();
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
            var categoryFind = await _unitOfWork.Category.GetAsync(entity.CategoryId);

            if (categoryFind != null)
            {
                entity.CategoryName = categoryFind.Name;
                var res = await _unitOfWork.ProductProperty.UpdateAsync(entity);
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
            var res = await _unitOfWork.ProductProperty.DeleteAsync(id);
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
            var res = await _unitOfWork.ProductProperty.FilterDataPaging(filter);
            return res;
        }
        catch (Exception ex)
        {
            _logProvider.Error(ex);
        }

        return null;
    }
}
