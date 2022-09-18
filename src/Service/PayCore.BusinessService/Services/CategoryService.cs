using AutoMapper;
using PayCore.Application.Interfaces.Services;
using PayCore.Application.Interfaces.UnitOfWork;
using PayCore.Application.Models;
using PayCore.Application.Utilities.Results;
using PayCore.Domain.Entities;
using PayCore.Application.Validations.CategoryValidation;
using FluentValidation;

namespace PayCore.BusinessService.Services
{
    public class CategoryService : BusinessService<CategoryEntity, CategoryModel>, ICategoryService
    {
        public CategoryService(IUnitOfWork<CategoryEntity, CategoryModel> unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public override IDataResult Add(CategoryModel model)
        {
            var validator = new CategoryValidator();
            validator.ValidateAndThrow(model);

            var result = base.Add(model);

            if (!result.IsSuccess)
                return new ErrorDataResult { ErrorMessage = "Could not add category" };

            return result;

        }
        public override IDataResult Update(CategoryModel model)
        {
            var validator = new CategoryValidator();
            validator.ValidateAndThrow(model);

            var result = base.Update(model);

            if (!result.IsSuccess)
                return new ErrorDataResult { ErrorMessage = "Could not update category" };
            return result;
        }
    }
}
