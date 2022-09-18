using AutoMapper;
using FluentValidation;
using PayCore.Application.Constant.Offer;
using PayCore.Application.Constant.Product;
using PayCore.Application.Interfaces.Services;
using PayCore.Application.Interfaces.Sessions;
using PayCore.Application.Interfaces.UnitOfWork;
using PayCore.Application.Models;
using PayCore.Application.Utilities.BusinessRuleEngine;
using PayCore.Application.Utilities.Results;
using PayCore.Application.Validations.OfferValidation;
using PayCore.Domain.Entities;

namespace PayCore.BusinessService.Services
{
    public class OfferService : BusinessService<OfferEntity, OfferModel>, IOfferService
    {
        private readonly IProductService _productService;
        public OfferService(IUnitOfWork<OfferEntity, OfferModel> unitOfWork, IMapper mapper, IProductService productService, IOfferSession offerSession) : base(unitOfWork, mapper)
        {
            _productService = productService;
        }

        public override IDataResult Add(OfferModel model)
        {
            var validator = new OfferValidator();
            validator.ValidateAndThrow(model);

            var dataResult = BusinessRuleEngine.Validate(CheckProduct(model));

            if (!dataResult.IsSuccess)
                return dataResult;

            var result =  base.Add(model);

            return result.IsSuccess ? new SuccessDataResult() : result;
        }

        public IDataResult ApproveTheOffer(long offerId)
        {
            var offer = base.GetFirstOrDefault(x=>x.Id == offerId);

            if (offer is null)
                return new ErrorDataResult { ErrorMessage = OfferConstant.OfferNotFound}; 

            var product = _productService.GetById(offerId);
            product.IsOfferable = false;
            product.IsSold = true;
            product.UserId = offer.UserId;

            var result = _productService.Update(product);

            return result.IsSuccess ? new SuccessDataResult() : result;
        }

        public IList<OfferModel> GetUserOffersOnProducts(long userId)
        {
            var userProducts = _productService.Where(x => x.User!.Id == userId);

            if (userProducts is null)
                return null!;

            string Ids = String.Join(",", userProducts.Select(x => x.Id!.Value).ToList());

            return base.SearchWithIn("product_id",Ids).ToList();
        }

        public IList<OfferModel> GetUserProductOffers(long userId)
        {
            return base.Where(x=>x.User!.Id == userId).ToList(); 
        }

        public IDataResult RejectTheOffer(long offerId)
        {
            var offer = base.GetFirstOrDefault(x=>x.Id == offerId);

            if (offer is null)
                return new ErrorDataResult { ErrorMessage = OfferConstant.OfferNotFound };

            var product = _productService.GetById(offerId);

            if (product is null)
                return new ErrorDataResult { ErrorMessage = ProductConstant.ProductNotFound};

            var result = base.Delete(offerId);

            return result.IsSuccess ? new SuccessDataResult() : result;
        }

        public override IDataResult Update(OfferModel model)
        {
            var validator = new OfferValidator();
            validator.ValidateAndThrow(model);

            var dataResult = BusinessRuleEngine.Validate(CheckProduct(model));
            if (!dataResult.IsSuccess)
                return dataResult;

            return base.Update(model);
        }
        private IDataResult CheckProduct(OfferModel model)
        {
            var product = _productService.GetById(model.ProductId.Value);

            if (product is null)
                return new ErrorDataResult();

            if (!product.IsOfferable)
                return new ErrorDataResult { ErrorMessage = ProductConstant.NotOfferable };

            var offeredPrice = (product.Price * model.PercentRate) / 100;

            if (model.OfferedPrice < offeredPrice)
                return new ErrorDataResult { ErrorMessage = OfferConstant.OfferNotValid};

            return new SuccessDataResult();
        }

    }

}
