using Common.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Models.Common;
using Models.Entities;
using Models.Filter;
using Models.Request;

namespace WebThuVienAdmin.Controllers;

[Route("hang-san-xuat")]
public class ManufactureController : Controller
{
    private readonly ILogProvider _logProvider;

    private readonly ICoreApiProvider _coreApiProvider;

    private string? ApiURL { get; set; }

    public ManufactureController(ILogProvider logProvider, ICoreApiProvider coreApiProvider, IConfigProvider configProvider)
    {
        _logProvider = logProvider;
        _coreApiProvider = coreApiProvider;
        ApiURL = configProvider.GetConfigString("ApiUrl");
    }

    [HttpGet("/hang-san-xuat/{page?}/{limit?}")]
    public async Task<IActionResult> Index(int? page, int? limit)
    {
        limit ??= 10;
        page ??= 1;
        var offset = (page - 1) * limit;

        var filter = new ManufactureFilter
        {
            Limit = limit,
            Offset = offset,
            Page = page - 1
        };

        var data = await _coreApiProvider.PostCore<DataPaging<Manufacture>>(ApiURL + "Manufacture/get-manufacture-paging", filter, isExactUrl: true);

        if (data != null && data.Data != null)
        {
            var totalRecord = data.Data.PaginationCount;
            ViewBag.TotalRecord = totalRecord;
            var totalPage = totalRecord % limit == 0 ? totalRecord / limit : totalRecord / limit + 1;
            ViewBag.TotalPage = totalPage;
            ViewBag.Manufactures = data.Data.Data;
        }

        ViewBag.CurrentPage = page;
        ViewBag.Limit = limit;
        return View();
    }

    [HttpGet("chi-tiet/{id}")]
    public async Task<IActionResult> GetAsync(string id)
    {
        var data = await _coreApiProvider.GetCore<Manufacture>(ApiURL + "Manufacture/get-manufacture-by-id/" + id, isExactUrl: true);
        return Ok(data);
    }

    [HttpPost("tao-moi")]
    public async Task<IActionResult> CreateAsync(Manufacture Manufacture)
    {
        Manufacture.CreatedAt = DateTime.Now;
        Manufacture.Id = Guid.NewGuid().ToString();
        var result = await _coreApiProvider.PostCore<string>(ApiURL + "Manufacture/create-manufacture", Manufacture, isExactUrl: true);
        return Ok(result);
    }

    [HttpPost("cap-nhat")]
    public async Task<IActionResult> UpdateAsync(Manufacture Manufacture)
    {
        Manufacture.UpdatedAt = DateTime.Now;
        var result = await _coreApiProvider.PutCore<bool>(ApiURL + "Manufacture/update-manufacture", Manufacture, isExactUrl: true);
        return Ok(result);
    }

    [HttpPost("cap-nhat-trang-thai")]
    public async Task<IActionResult> UpdateStatusAsync(EntityStatusUpdate entityStatus)
    {
        var result = await _coreApiProvider.PutCore<bool>(ApiURL + "Manufacture/update-status", entityStatus, isExactUrl: true);
        return Ok(result);
    }

    [HttpGet("xoa/{id}")]
    public async Task<IActionResult> DeleteAsync(string id)
    {
        var result = await _coreApiProvider.GetCore<bool>(ApiURL + "Manufacture/delete-manufacture-by-id/" + id, isExactUrl: true);
        return Ok(result);
    }
}
