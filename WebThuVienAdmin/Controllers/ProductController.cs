using Microsoft.AspNetCore.Mvc;
using Common.Abstractions;
using Models.Common;
using Models.Entities;
using Models.Filter;
using Models.ViewModels;
using Models.Request;

namespace WebThuVienAdmin.Controllers;

[Route("san-pham")]
public class ProductController : Controller
{
    private readonly ILogProvider _logProvider;

    private readonly ICoreApiProvider _coreApiProvider;

    private string? ApiURL { get; set; }

    public ProductController(ILogProvider logProvider, ICoreApiProvider coreApiProvider, IConfigProvider configProvider)
    {
        _logProvider = logProvider;
        _coreApiProvider = coreApiProvider;
        ApiURL = configProvider.GetConfigString("ApiUrl");
    }

    [HttpGet("danh-sach")]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("thuoc-tinh/{page?}/{limit?}")]
    public async Task<IActionResult> Property(int? page, int? limit)
    {
        limit ??= 10;
        page ??= 1;
        var offset = (page - 1) * limit;

        var filter = new ProductPropertyFilter
        {
            Limit = limit,
            Offset = offset,
            Page = page - 1,
        };

        var data = await _coreApiProvider.PostCore<DataPaging<ProductProperty>>(ApiURL + "ProductProperty/filter", filter, isExactUrl: true);
        
        if (data != null && data.Data != null)
        {
            var totalRecord = data.Data.PaginationCount;
            ViewBag.TotalRecord = totalRecord;
            var totalPage = totalRecord % limit == 0 ? totalRecord / limit : totalRecord / limit + 1;
            ViewBag.TotalPage = totalPage <= 0 ? 1 : totalPage;
            ViewBag.ProductProperties = data.Data.Data;
        }

        var activedCategories = await _coreApiProvider.GetCore<List<CategoryViewModel>>(ApiURL + "category/get-showing-categories", isExactUrl: true);
        ViewBag.ActivedCategories = activedCategories.Data;
        ViewBag.CurrentPage = page;
        ViewBag.Limit = limit;
        return View();
    }

    #region API
    #region Product
    #endregion

    #region ProductImage
    #endregion

    #region Product Property
    [HttpGet("thuoc-tinh/chi-tiet/{id}")]
    public async Task<IActionResult> ProductPropertyDetail(string id)
    {
        var data = await _coreApiProvider.GetCore<ProductProperty>(ApiURL + "ProductProperty/get/" + id, isExactUrl: true);
        return Ok(data);
    }

    [HttpPost("thuoc-tinh/tao-moi")]
    public async Task<IActionResult> CreateProductProperty(ProductProperty entity)
    {
        entity.Id = Guid.NewGuid().ToString();
        entity.CreatedAt = DateTime.Now;
        var result = await _coreApiProvider.PostCore<string>(ApiURL + "ProductProperty/create", entity, isExactUrl: true);
        return Ok(result);
    }

    [HttpPost("thuoc-tinh/cap-nhat")]
    public async Task<IActionResult> UpdateProductProperty(ProductProperty entity)
    {
        entity.UpdatedAt = DateTime.Now;
        var result = await _coreApiProvider.PutCore<bool>(ApiURL + "ProductProperty/update", entity, isExactUrl: true);
        return Ok(result);
    }

    [HttpGet("thuoc-tinh/xoa/{id}")]
    public async Task<IActionResult> DeleteProductProperty(string id)
    {
        var result = await _coreApiProvider.GetCore<bool>(ApiURL + "ProductProperty/delete/" + id, isExactUrl: true);
        return Ok(result);
    }

    [HttpPost("thuoc-tinh/cap-nhat-trang-thai")]
    public async Task<IActionResult> UpdateStatusProductProperty(EntityStatusUpdate entityStatus)
    {
        var result = await _coreApiProvider.PutCore<bool>(ApiURL + "ProductProperty/update-status", entityStatus, isExactUrl: true);
        return Ok(result);
    }
    #endregion
    #endregion
}
