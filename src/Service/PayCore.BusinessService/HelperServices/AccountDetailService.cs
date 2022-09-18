using PayCore.Application.Interfaces.HelperServices;
using PayCore.Application.Interfaces.Services;
using PayCore.Application.Models;

namespace PayCore.BusinessService.HelperServices;

public class AccountDetailService : IAccountDetailService
{
    private readonly IProductService _productService;
    private readonly IOfferService _offerService;

    public AccountDetailService(IProductService productService, IOfferService offerService)
    {
        _productService = productService;
        _offerService = offerService;
    }

    public IList<OfferModel> GetUserOffersOnProducts(long userId)
    {
        var userProducts = _productService.Where(x => x.User!.Id == userId);

        if (userProducts is null || userProducts.Select(x=>x.Id).Count() <= 0)
            return null!;

        string Ids = String.Join(",", userProducts.Select(x => x.Id!.Value).ToList());

        return _offerService.SearchWithIn("product_id", Ids).ToList();
    }

    public IList<OfferModel> GetUserProductOffers(long userId)
    {
        return _offerService.Where(x => x.User!.Id == userId).ToList();
    }
}
