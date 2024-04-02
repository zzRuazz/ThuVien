using Microsoft.AspNetCore.Mvc;
using Common.Abstractions;
using Models.ViewModels;

namespace Salvation.Presentation.WebApp.Controllers;

/// <summary>
/// ProductController
/// </summary>
[Route("san-pham")]
public class ProductController : Controller
{
    /// <summary>
    /// ICoreApiProvider
    /// </summary>
    private readonly ICoreApiProvider _coreApiProvider;

    /// <summary>
    /// ILogProvider
    /// </summary>
    private readonly ILogProvider _logProvider;

    public ProductController(ICoreApiProvider coreApiProvider, ILogProvider logProvider)
    {
        _coreApiProvider = coreApiProvider;
        _logProvider = logProvider;
    }

    [Route("danh-muc/{slug?}/{displayMode?}")]
    public async Task<IActionResult> Index(string? slug, string? displayMode = "grid")
    {
        try
        {
            var categoriesResult = await _coreApiProvider.GetCore<List<CategoryViewModel>>("https://localhost:7041/api/category/get-active-categories", isExactUrl: true);

            if (categoriesResult != null && categoriesResult.Code == 0 && categoriesResult.Data != null)
            {
                ViewBag.Categories = categoriesResult.Data;
            }
        }
        catch (Exception ex)
        {
            _logProvider.Error(ex);
        }

        return View();
    }

    [Route("chi-tiet/{slug}")]
    public IActionResult Detail(string slug)
    {
        return View();
    }

    [Route("xay-dung-cau-hinh")]
    public IActionResult Build()
    {
        return View();
    }
}
