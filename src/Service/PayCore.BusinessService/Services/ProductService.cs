using AutoMapper;
using PayCore.Application.Interfaces.Services;
using PayCore.Application.Interfaces.Sessions;
using PayCore.Application.Interfaces.UnitOfWork;
using PayCore.Application.Models;
using PayCore.Application.Utilities.BusinessRuleEngine;
using PayCore.Application.Utilities.Results;
using PayCore.Domain.Entities;

namespace PayCore.BusinessService.Services
{
    public class ProductService : BusinessService<ProductEntity, ProductModel>, IProductService
    {
        private readonly IUnitOfWork<ProductEntity, ProductModel> _unitOfWork;
        private readonly IProductSession _productSession;
        private readonly ICategoryService _categoryService;
        public ProductService(IUnitOfWork<ProductEntity, ProductModel> unitOfWork, IMapper mapper, IProductSession productSession, ICategoryService categoryService) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
            _productSession = productSession;
            _categoryService = categoryService;
        }

        public override IDataResult Add(ProductModel model)
        {
            var dataResult = BusinessRuleEngine.Validate(CheckCategory(model));

            if (!dataResult.IsSuccess)
                return dataResult;

            return base.Add(model);
        }

        public IDataResult SellTheProduct(long productId, long userId)
        {
            var product = base.GetFirstOrDefault(x=>x.Id == productId);

            if (product is null)
                return new ErrorDataResult { ErrorMessage = "Product didn't find."};

            product.UserId = userId;
            product.IsSold = true;
            product.IsOfferable = false;

            return base.Update(product);
        }

        public override IDataResult Update(ProductModel model)
        {
            var dataResult = BusinessRuleEngine.Validate(CheckCategory(model));

            if (!dataResult.IsSuccess)
                return dataResult;

            return base.Update(model);
        }

        private IDataResult CheckCategory(ProductModel model)
        {
            var category = _categoryService.GetFirstOrDefault(x=>x.Id == model.CategoryId);

            if (category is null)
                return new ErrorDataResult { ErrorMessage = "Category didn't find"};
            return new SuccessDataResult();
        }
    }
}
