using Microsoft.AspNetCore.Mvc;
using PayCore.Application.Extensions;
using PayCore.Application.Utilities.Results;
namespace PayCore.Application.Utilities.BaseApiTools;

[ApiController]
[Route("api/[controller]s")]
public  class BaseApiResponse : ControllerBase
{
    protected virtual IActionResult ApiResponse<IModel>(IEnumerable<IModel> data)
    {
        if (data.IsNotNullOrEmpty())
            return Ok(data);
        else return NotFound();
    }
    protected virtual IActionResult ApiResponse<IModel>(IModel data)
    {
        if (data.IsNotNull())
            return Ok(data);
        else return NotFound();
    }
    protected virtual IActionResult ApiResponse(IDataResult result)
    {
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}


