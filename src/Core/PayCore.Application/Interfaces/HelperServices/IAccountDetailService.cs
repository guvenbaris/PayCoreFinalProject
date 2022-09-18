using PayCore.Application.Models;

namespace PayCore.Application.Interfaces.HelperServices;

public interface IAccountDetailService
{
    IList<OfferModel> GetUserProductOffers(long userId);
    IList<OfferModel> GetUserOffersOnProducts(long userId);
}
