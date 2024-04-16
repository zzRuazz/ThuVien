using Microsoft.AspNetCore.Mvc;
using Common.Abstractions;
using Models.ViewModels;

namespace WebThuVien.Presentation.WebApp.Components;

/// <summary>
/// CategoryComponent
/// </summary>
public class CategoryComponent : ViewComponent
{
    /// <summary>
    /// ICoreApiProvider
    /// </summary>
    private readonly ICoreApiProvider _coreApiProvider;

    /// <summary>
    /// ILogProvider
    /// </summary>
    private readonly ILogProvider _logProvider;

    private string? ApiURL { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="restProvider"></param>
    public CategoryComponent(ICoreApiProvider coreApiProvider, ILogProvider logProvider, IConfigProvider configProvider)
    {
        _coreApiProvider = coreApiProvider;
        _logProvider = logProvider;
        ApiURL = configProvider.GetConfigString("ApiUrl");
    }

    /// <summary>
    /// Invoke
    /// </summary>
    /// <returns></returns>
    public async Task<IViewComponentResult> InvokeAsync()
    {
        try
        {
            var categoriesResult = await _coreApiProvider.GetCore<List<CategoryViewModel>>(ApiURL + "category/get-active-categories", isExactUrl: true);
            
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
}
