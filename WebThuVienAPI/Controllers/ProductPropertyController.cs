using Microsoft.AspNetCore.Mvc;
using Common.Abstractions;
using Models.Entities;
using Models.Filter;
using WebThuVienAPI.Services.Abstractions;
using Models.Request;
using WebThuVIenAPI.Controllers.Base;

namespace Salvation.Services.ProductApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductPropertyController : BaseController
{
    private readonly ILogProvider _logProvider;

    private readonly IProductPropertyService _productPropertyService;

    public ProductPropertyController(ILogProvider logProvider, IProductPropertyService productPropertyService)
    {
        _logProvider = logProvider;
        _productPropertyService = productPropertyService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync(ProductProperty entity)
    {
        try
        {
            var res = await _productPropertyService.CreateAsync(entity);
            return Ok(SuccessData(res));
        }
        catch (Exception ex)
        {
            _logProvider.Error(ex);
        }

        return Ok(ErrorMessage("Có lỗi xảy ra!"));
    }

    [HttpGet("get/{id}")]
    public async Task<IActionResult> GetAsync(string id)
    {
        try
        {
            var res = await _productPropertyService.GetAsync(id);
            return Ok(SuccessData(res));
        }
        catch (Exception ex)
        {
            _logProvider.Error(ex);
        }

        return Ok(ErrorMessage("Có lỗi xảy ra!"));
    }

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllAsync()
    {
        try
        {
            var res = await _productPropertyService.GetAllAsync();
            return Ok(SuccessData(res));
        }
        catch (Exception ex)
        {
            _logProvider.Error(ex);
        }

        return Ok(ErrorMessage("Có lỗi xảy ra!"));
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync(ProductProperty entity)
    {
        try
        {
            var res = await _productPropertyService.UpdateAsync(entity);
            return Ok(SuccessData(res));
        }
        catch (Exception ex)
        {
            _logProvider.Error(ex);
        }

        return Ok(ErrorMessage("Có lỗi xảy ra!"));
    }

    [HttpGet("delete/{id}")]
    public async Task<IActionResult> DeleteAsync(string id)
    {
        try
        {
            var res = await _productPropertyService.DeleteAsync(id);
            return Ok(SuccessData(res));
        }
        catch (Exception ex)
        {
            _logProvider.Error(ex);
        }

        return Ok(ErrorMessage("Có lỗi xảy ra!"));
    }

    [HttpPost("filter")]
    public async Task<IActionResult> FilterDataPaging(ProductPropertyFilter filter)
    {
        try
        {
            var res = await _productPropertyService.FilterDataPaging(filter);
            return Ok(SuccessData(res));
        }
        catch (Exception ex)
        {
            _logProvider.Error(ex);
        }

        return Ok(ErrorMessage("Có lỗi xảy ra!"));
    }

    [HttpPut("update-status")]
    public async Task<IActionResult> UpdateStatus(EntityStatusUpdate entityStatus)
    {
        var find = await _productPropertyService.GetAsync(entityStatus.Id);

        if (find != null)
        {
            find.IsActived = !find.IsActived;
            find.IsDeleted = !find.IsDeleted;
            find.UpdatedAt = DateTime.Now;
            var result = await _productPropertyService.UpdateAsync(find);
            return Ok(SuccessData(result));
        }

        return Ok(ErrorMessage("Object is not found!"));
    }
}
