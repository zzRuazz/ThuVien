using Microsoft.AspNetCore.Mvc;
using Common.Abstractions;
using Models.Entities;
using Models.Filter;
using WebThuVienAPI.Services.Abstractions;
using WebThuVienAPI.Controllers.Base;

namespace WebThuVien.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : BaseController
{
    private readonly ILogProvider _logProvider;

    private readonly IProductService _productService;

    public ProductController(ILogProvider logProvider, IProductService productService)
    {
        _logProvider = logProvider;
        _productService = productService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync(Product entity)
    {
        try
        {
            var res = await _productService.CreateAsync(entity);
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
            var res = await _productService.GetAsync(id);
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
            var res = await _productService.GetAllAsync();
            return Ok(SuccessData(res));
        }
        catch (Exception ex)
        {
            _logProvider.Error(ex);
        }

        return Ok(ErrorMessage("Có lỗi xảy ra!"));
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync(Product entity)
    {
        try
        {
            var res = await _productService.UpdateAsync(entity);
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
            var res = await _productService.DeleteAsync(id);
            return Ok(SuccessData(res));
        }
        catch (Exception ex)
        {
            _logProvider.Error(ex);
        }

        return Ok(ErrorMessage("Có lỗi xảy ra!"));
    }

    [HttpPost("filter")]
    public async Task<IActionResult> FilterDataPaging(ProductFilter filter)
    {
        try
        {
            var res = await _productService.FilterDataPaging(filter);
            return Ok(SuccessData(res));
        }
        catch (Exception ex)
        {
            _logProvider.Error(ex);
        }

        return Ok(ErrorMessage("Có lỗi xảy ra!"));
    }
}
