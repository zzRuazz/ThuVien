using Microsoft.AspNetCore.Mvc;
using Models.Filter;
using Models.Request;
using WebThuVienAPI.Controllers.Base;
using WebThuVienAPI.Services.Abstractions;

namespace WebThuVienAPI.Controllers;

[Route("api/[controller]")]
public class ProductDetailController : BaseController
{
    private readonly IProductDetailService _productDetailService;

    public ProductDetailController(IProductDetailService productDetailService)
    {
        _productDetailService = productDetailService;
    }

    [HttpPost("find-by-filter")]
    public async Task<IActionResult> FindProductDetailValue(ProductDetailFilter filter)
    {
        var res = await _productDetailService.FindProductDetailValue(filter);
        return Ok(SuccessData(res));
    }

    [HttpPost("update-data")]
    public async Task<IActionResult> UpdateProductDetailValue(ProductDetailUpdateRequest request)
    {
        var res = await _productDetailService.UpdateProductDetailValue(request);
        return Ok(SuccessData(res));
    }
}
