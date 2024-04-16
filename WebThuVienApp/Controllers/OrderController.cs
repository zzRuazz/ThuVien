using Microsoft.AspNetCore.Mvc;

namespace WebThuVien.Presentation.WebApp.Controllers;

/// <summary>
/// OrderController
/// </summary>
[Route("don-hang")]
public class OrderController : Controller
{
    [Route("chi-tiet")]
    public IActionResult Index()
    {
        return View();
    }

    [Route("thanh-toan")]
    public IActionResult CheckOut()
    {
        return View();
    }
}
