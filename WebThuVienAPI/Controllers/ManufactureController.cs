using Microsoft.AspNetCore.Mvc;
using Common.Abstractions;
using Models.Entities;
using Models.Filter;
using Models.Request;
using WebThuVienAPI.Services.Abstractions;
using WebThuVienAPI.Controllers.Base;

namespace WebThuVien.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ManufactureController : BaseController
{
    /// <summary>
    /// IManufactureService
    /// </summary>
    private readonly IManufactureService _manufactureService;

    /// <summary>
    /// ILogProvider
    /// </summary>
    private readonly ILogProvider _logProvider;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="manufactureService"></param>
    /// <param name="logProvider"></param>
    public ManufactureController(IManufactureService manufactureService, ILogProvider logProvider)
    {
        _manufactureService = manufactureService;
        _logProvider = logProvider;
    }

    [HttpGet("get-active-manufactures")]
    public async Task<IActionResult> GetActiveManufactures()
    {
        try
        {
            var manufactures = await _manufactureService.GetActiveManufactures();

            if (manufactures != null)
            {
                return Ok(SuccessData(manufactures));
            }

            return Ok(SuccessData());
        }
        catch (Exception ex)
        {
            _logProvider.Error(ex);
            return Ok(ErrorMessage(ex.Message));
        }
    }

    [HttpPost("get-manufacture-paging")]
    public async Task<IActionResult> GetDataPaging(ManufactureFilter filter)
    {
        try
        {
            var result = await _manufactureService.FilterDataPaging(filter);
            return Ok(SuccessData(result));
        }
        catch (Exception ex)
        {
            _logProvider.Error(ex);
            return Ok(ErrorMessage(ex.Message));
        }
    }

    [HttpGet("get-manufacture-by-id/{id}")]
    public async Task<IActionResult> GetManufactureById(string id)
    {
        var result = await _manufactureService.GetAsync(id);
        return Ok(SuccessData(result));
    }

    [HttpGet("get-all-manufactures")]
    public async Task<IActionResult> GetAllManufactures()
    {
        var result = await _manufactureService.GetAllAsync();
        return Ok(SuccessData(result));
    }

    [HttpPost("create-manufacture")]
    public async Task<IActionResult> CreateNewManufacture(Manufacture Manufacture)
    {
        var result = await _manufactureService.CreateAsync(Manufacture);
        return Ok(SuccessData(result));
    }

    [HttpPut("update-manufacture")]
    public async Task<IActionResult> UpdateManufacture(Manufacture Manufacture)
    {
        var result = await _manufactureService.UpdateAsync(Manufacture);
        return Ok(SuccessData(result));
    }

    [HttpGet("delete-manufacture-by-id/{id}")]
    public async Task<IActionResult> DeleteManufactureById(string id)
    {
        var result = await _manufactureService.DeleteAsync(id);
        return Ok(SuccessData(result));
    }

    [HttpPut("update-status")]
    public async Task<IActionResult> UpdateCategoryStatus(EntityStatusUpdate entityStatus)
    {
        var find = await _manufactureService.GetAsync(entityStatus.Id);

        if (find != null)
        {
            find.IsActived = !find.IsActived;
            find.IsDeleted = !find.IsActived;
            find.UpdatedAt = DateTime.Now;
            var result = await _manufactureService.UpdateAsync(find);
            return Ok(SuccessData(result));
        }

        return Ok(ErrorMessage("Object is not found!"));
    }
}
