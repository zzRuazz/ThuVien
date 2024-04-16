using Microsoft.AspNetCore.Mvc;

namespace WebThuVien.Presentation.WebApp.Controllers;

/// <summary>
/// NewController
/// </summary>
[Route("tin-tuc")]
public class NewController : Controller
{
    [Route("danh-sach")]
    public IActionResult Index()
    {
        return View();
    }

    [Route("chi-tiet/{slug}")]
    public IActionResult Detail(string slug)
    {
        return View();
    }
}
