using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PayCore.Application.Interfaces.Services;
using PayCore.Application.Models;
using PayCore.Domain.Entities;

namespace PayCore.Application.Utilities.BaseApiTools;

[Authorize]
public  abstract class BaseApiController<TEntity, TModel> : BaseApiResponse
    where TModel : BaseModel
    where TEntity : BaseEntity
{
    private readonly IBusinessService<TEntity, TModel> _businessService;

    protected BaseApiController(IBusinessService<TEntity, TModel> businessService)
    {
        _businessService = businessService;
    }

    [HttpGet]
    public virtual IActionResult GetAll()
    {
        return ApiResponse<TModel>(_businessService.GetAll());
    }

    [HttpGet("{id}")]
    public virtual IActionResult GetById(long id)
    {
        return ApiResponse<TModel>(_businessService.GetById(id));
    }

    [HttpPost]
    public virtual IActionResult Add([FromBody] TModel model)
    {
        return ApiResponse(_businessService.Add(model));
    }
    [HttpPut]
    public virtual IActionResult Update([FromBody] TModel model)
    {
        return ApiResponse(_businessService.Update(model));
    }
    [HttpDelete("{id}")]
    public virtual IActionResult Delete(long id)
    {
        return ApiResponse(_businessService.Delete(id));
    }
}


