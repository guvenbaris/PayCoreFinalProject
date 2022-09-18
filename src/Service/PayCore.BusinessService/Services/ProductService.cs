using AutoMapper;
using FluentValidation;
using PayCore.Application.Constant.Product;
using PayCore.Application.Interfaces.Services;
using PayCore.Application.Interfaces.UnitOfWork;
using PayCore.Application.Models;
using PayCore.Application.Utilities.BusinessRuleEngine;
using PayCore.Application.Utilities.Results;
using PayCore.Application.Validations.ProductValidation;
using PayCore.Domain.Entities;

namespace PayCore.BusinessService.Services
{
    public class ProductService : BusinessService<ProductEntity, ProductModel>, IProductService
    {
        private readonly ICategoryService _categoryService;
        public ProductService(IUnitOfWork<ProductEntity, ProductModel> unitOfWork, IMapper mapper, ICategoryService categoryService) : base(unitOfWork, mapper)
        {
            _categoryService = categoryService;
        }

        public override IDataResult Add(ProductModel model)
        {
            var validator = new ProductValidator();
            validator.ValidateAndThrow(model);

            var dataResult = BusinessRuleEngine.Validate(CheckCategory(model));

            if (!dataResult.IsSuccess)
                return dataResult;

            var result = base.Add(model);

            return result.IsSuccess ? new SuccessDataResult() : result;
        }

        public IDataResult SellTheProduct(long productId, long userId)
        {
            var product = base.GetById(productId);

            if (product is null)
                return new ErrorDataResult { ErrorMessage = ProductConstant.ProductNotFound};

            product.UserId = userId;
            product.IsSold = true;
            product.IsOfferable = false;

            var result =  base.Update(product);

            return result.IsSuccess ? new SuccessDataResult {Message = ProductConstant.ProductSold } : result;
        }

        public override IDataResult Update(ProductModel model)
        {
            var validator = new ProductValidator();
            validator.ValidateAndThrow(model);

            var dataResult = BusinessRuleEngine.Validate(CheckCategory(model));

            if (!dataResult.IsSuccess)
                return dataResult;

            var result =  base.Update(model);

            return result.IsSuccess ? new SuccessDataResult() : result;
        }

        private IDataResult CheckCategory(ProductModel model)
        {
            var category = _categoryService.GetById(model.CategoryId!.Value);

            if (category is null)
                return new ErrorDataResult { ErrorMessage = CategoryConstant.CategoryNotFound};
            return new SuccessDataResult();
        }
    }
}
