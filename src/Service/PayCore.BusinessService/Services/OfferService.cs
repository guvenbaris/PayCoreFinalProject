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
    public class OfferService : BusinessService<OfferEntity, OfferModel>, IOfferService
    {
        private readonly IProductService _productService;
        private readonly IOfferSession _offerSession;
        public OfferService(IUnitOfWork<OfferEntity, OfferModel> unitOfWork, IMapper mapper, IProductService productService, IOfferSession offerSession) : base(unitOfWork, mapper)
        {
            _productService = productService;
            _offerSession = offerSession;
        }

        public override IDataResult Add(OfferModel model)
        {
            // Validasyon yazılacak ... var validator = 

            var dataResult = BusinessRuleEngine.Validate(CheckProduct(model));

            if (!dataResult.IsSuccess)
                return dataResult;

           return base.Add(model);
        }

        public IDataResult ApproveTheOffer(long offerId)
        {
            var offer = base.GetFirstOrDefault(x=>x.Id == offerId);

            if (offer is null)
                return new ErrorDataResult { ErrorMessage = "Offer didn't find"}; 

            var product = _productService.GetById(offerId);
            product.IsOfferable = false;
            product.IsSold = true;
            product.UserId = offer.UserId;

            return _productService.Update(product);
        }

        public IEnumerable<OfferModel> GetUserOffersOnProducts(long userId)
        {
            userId = 1;
            var userProducts = _productService.Where(x => x.User.Id == userId);
            var offers = base.Where(x => userProducts.Select(y => y.Id).ToList().Contains(1));

            return offers;
        }

        public IList<OfferModel> GetUserProductOffers(long userId)
        {
            return base.Where(x=>x.User.Id == userId).ToList();
        }

        public IDataResult RejectTheOffer(long offerId)
        {
            var offer = base.GetFirstOrDefault(x=>x.Id == offerId);

            if (offer is null)
                return new ErrorDataResult { ErrorMessage = "Offer didn't find" };

            var product = _productService.GetById(offerId);
            product.IsOfferable = true;

            return base.Delete(offerId);
        }

        public override IDataResult Update(OfferModel model)
        {
            // validasyon yazılacak 
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
                return new ErrorDataResult { ErrorMessage = "Product status is not offered" };

            var offeredPrice = (product.Price * model.PercentRate) / 100;

            if (model.OfferedPrice < offeredPrice)
                return new ErrorDataResult { ErrorMessage = "Offer is much lower to for price" };

            return new SuccessDataResult();
        }

    }

}
