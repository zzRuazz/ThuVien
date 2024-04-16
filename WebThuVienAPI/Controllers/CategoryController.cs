using Microsoft.AspNetCore.Mvc;
using Common.Abstractions;
using Models.Entities;
using Models.Filter;
using Models.Request;
using WebThuVienAPI.Services.Abstractions;
using WebThuVienAPI.Controllers.Base;

namespace WebThuVien.Controllers;

/// <summary>
/// CategoryController
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class CategoryController : BaseController
{
    /// <summary>
    /// IProductHandler
    /// </summary>
    private readonly ICategoryService _categoryService;

    /// <summary>
    /// ILogProvider
    /// </summary>
    private readonly ILogProvider _logProvider;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="categoryService"></param>
    /// <param name="logProvider"></param>
    public CategoryController(ICategoryService categoryService, ILogProvider logProvider)
    {
        _categoryService = categoryService;
        _logProvider = logProvider;
    }

    [HttpGet("get-active-categories")]
    public async Task<IActionResult> GetActiveCategories()
    {
        try
        {
            var categories = await _categoryService.GetActiveCategories();
            
            if (categories != null)
            {
                return Ok(SuccessData(categories));
            }

            return Ok(SuccessData());
        }
        catch (Exception ex)
        {
            _logProvider.Error(ex);
            return Ok(ErrorMessage(ex.Message));
        }
    }

    [HttpGet("get-showing-categories")]
    public async Task<IActionResult> GetShowingCategories()
    {
        try
        {
            var categories = await _categoryService.GetShowingCategories();

            if (categories != null)
            {
                return Ok(SuccessData(categories));
            }

            return Ok(SuccessData());
        }
        catch (Exception ex)
        {
            _logProvider.Error(ex);
            return Ok(ErrorMessage(ex.Message));
        }
    }

    [HttpPost("get-category-paging")]
    public async Task<IActionResult> GetDataPaging(CategoryFilter filter)
    {
        try
        {
            var result = await _categoryService.FilterDataPaging(filter);
            return Ok(SuccessData(result));
        }
        catch (Exception ex)
        {
            _logProvider.Error(ex);
            return Ok(ErrorMessage(ex.Message));
        }
    }

    [HttpGet("get-category-by-id/{id}")]
    public async Task<IActionResult> GetCategoryById(string id)
    {
        var result = await _categoryService.GetAsync(id);
        return Ok(SuccessData(result));
    }

    [HttpGet("get-all-categories")]
    public async Task<IActionResult> GetAllCategories()
    {
        var result = await _categoryService.GetAllAsync();
        return Ok(SuccessData(result));
    }

    [HttpPost("create-category")]
    public async Task<IActionResult> CreateNewCategory(Category category)
    {
        var result = await _categoryService.CreateAsync(category);
        return Ok(SuccessData(result));
    }

    [HttpPut("update-category")]
    public async Task<IActionResult> UpdateCategory(Category category)
    {
        var result = await _categoryService.UpdateAsync(category);
        return Ok(SuccessData(result));
    }

    [HttpPut("update-status")]
    public async Task<IActionResult> UpdateCategoryStatus(EntityStatusUpdate entityStatus)
    {
        var find = await _categoryService.GetAsync(entityStatus.Id);
        
        if (find != null)
        {
            find.IsActived = !find.IsActived;
            find.IsDeleted = !find.IsActived;
            find.UpdatedAt = DateTime.Now;
            var result = await _categoryService.UpdateAsync(find);
            return Ok(SuccessData(result));
        }

        return Ok(ErrorMessage("Object is not found!"));
    }

    [HttpGet("delete-category-by-id/{id}")]
    public async Task<IActionResult> DeleteCategoryById(string id)
    {
        var result = await _categoryService.DeleteAsync(id);
        return Ok(SuccessData(result));
    }
}
