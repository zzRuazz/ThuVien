using Common.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Models.Entities;
using Models.Filter;
using WebThuVienAPI.Controllers.Base;
using WebThuVienAPI.Services.Abstractions;

namespace WebApi_EF.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductImageController : BaseController
{
    private readonly IProductImageService _productImageService;

    private readonly ILogProvider _logProvider;

    public ProductImageController(IProductImageService productImageService, ILogProvider logProvider)
    {
        _productImageService = productImageService;
        _logProvider = logProvider;
    }

    [HttpGet("find-by-product-id/{productId}")]
    public async Task<IActionResult> FindProductImageByProductId(string productId)
    {
        var res = await _productImageService.FilterDataPaging(new ProductImageFilter { ProductId = productId });
        return Ok(SuccessData(res.Data));
    }

    [HttpPost("find-by-filter")]
    public async Task<IActionResult> FindProductImageValue(ProductImageFilter filter)
    {
        var res = await _productImageService.FindProductImageValue(filter);
        return Ok(SuccessData(res));
    }

    [HttpPost("update-data")]
    public async Task<IActionResult> UpdateProductImageValue(ProductImage entity)
    {
        var res = await _productImageService.UpdateProductImageValue(entity);
        return Ok(SuccessData(res));
    }

    [HttpGet("delete/{id}")]
    public async Task<IActionResult> DeleteProductImage(string id)
    {
        var res = await _productImageService.DeleteAsync(id);
        return Ok(SuccessData(res));
    }
}
