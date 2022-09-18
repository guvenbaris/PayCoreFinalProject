using PayCore.Application.Models;
using PayCore.Application.Utilities.Results;
using PayCore.Domain.Entities;

namespace PayCore.Application.Interfaces.Services
{
    public interface IOfferService : IBusinessService<OfferEntity, OfferModel>
    {
        IDataResult ApproveTheOffer(long offerId);
        IDataResult RejectTheOffer(long offerId);
    }
}