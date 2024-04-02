using Microsoft.AspNetCore.Mvc;
using Common.Abstractions;
using Models.Common;
using Models.Entities;
using Models.Filter;
using Models.Request;
using Models.ViewModels;

namespace WebThuVienAdmin.Controllers;

public class CategoryController : Controller
{
    private readonly ILogProvider _logProvider;

    private readonly ICoreApiProvider _coreApiProvider;

    private string? ApiURL { get; set; }

    public CategoryController(ILogProvider logProvider, ICoreApiProvider coreApiProvider, IConfigProvider configProvider)
    {
        _logProvider = logProvider;
        _coreApiProvider = coreApiProvider;
        ApiURL = configProvider.GetConfigString("ApiUrl");
    }

    [HttpGet("/danh-muc/{page?}/{limit?}")]
    public async Task<IActionResult> Index(int? page, int? limit)
    {
        limit ??= 10;
        page ??= 1;
        var offset = (page - 1) * limit;

        var filter = new CategoryFilter
        {
            Limit = limit,
            Offset = offset,
            Page = page - 1
        };

        var data = await _coreApiProvider.PostCore<DataPaging<Category>>(ApiURL + "category/get-category-paging", filter, isExactUrl: true);

        if (data != null && data.Data != null)
        {
            var totalRecord = data.Data.PaginationCount;
            ViewBag.TotalRecord = totalRecord;
            var totalPage = totalRecord % limit == 0 ? totalRecord / limit : totalRecord / limit + 1;
            ViewBag.TotalPage = totalPage;
            ViewBag.Categories = data.Data.Data;
        }

        var activedCategories = await _coreApiProvider.GetCore<List<CategoryViewModel>>(ApiURL + "category/get-showing-categories", isExactUrl: true);
        ViewBag.ActivedCategories = activedCategories.Data;
        ViewBag.CurrentPage = page;
        ViewBag.Limit = limit;
        return View();
    }

    [HttpGet("/danh-muc/chi-tiet/{id}")]
    public async Task<IActionResult> GetAsync(string id)
    {
        var data = await _coreApiProvider.GetCore<Category>(ApiURL + "category/get-category-by-id/" + id, isExactUrl: true);
        return Ok(data);
    }

    [HttpPost("/danh-muc/tao-moi")]
    public async Task<IActionResult> CreateAsync(Category category)
    {
        category.CreatedAt = DateTime.Now;
        category.Id = Guid.NewGuid().ToString();
        var result = await _coreApiProvider.PostCore<string>(ApiURL + "category/create-category", category, isExactUrl: true);
        return Ok(result);
    }

    [HttpPost("/danh-muc/cap-nhat")]
    public async Task<IActionResult> UpdateAsync(Category category)
    {
        category.UpdatedAt = DateTime.Now;
        var result = await _coreApiProvider.PutCore<bool>(ApiURL + "category/update-category", category, isExactUrl: true);
        return Ok(result);
    }

    [HttpPost("/danh-muc/cap-nhat-trang-thai")]
    public async Task<IActionResult> UpdateStatusAsync(EntityStatusUpdate entityStatus)
    {
        var result = await _coreApiProvider.PutCore<bool>(ApiURL + "category/update-status", entityStatus, isExactUrl: true);
        return Ok(result);
    }

    [HttpGet("/danh-muc/xoa/{id}")]
    public async Task<IActionResult> DeleteAsync(string id)
    {
        var result = await _coreApiProvider.GetCore<bool>(ApiURL + "category/delete-category-by-id/" + id, isExactUrl: true);
        return Ok(result);
    }
}
