using PayCore.Application.Models;
using PayCore.Domain.Entities;

namespace PayCore.Application.Interfaces.Services;

public interface ICategoryService : IBusinessService<CategoryEntity, CategoryModel>
{
}
