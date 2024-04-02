using Microsoft.AspNetCore.Mvc;
using Salvation.Presentation.WebApp.Models;
using System.Diagnostics;

namespace Salvation.Presentation.WebApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [Route("phan-hoi")]
    public IActionResult Feedback()
    {
        return View();
    }

    [Route("gioi-thieu")]
    public IActionResult AboutUs()
    {
        return View();
    }

    [Route("cau-hoi-thuong-gap")]
    public IActionResult Faq()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
