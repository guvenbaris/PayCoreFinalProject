using Microsoft.AspNetCore.Mvc;
using PayCore.Application.Interfaces.Services;
using PayCore.Application.Models;
using PayCore.Application.Utilities.BaseApiTools;
using PayCore.Domain.Entities;
using System.Security.Claims;

namespace Paycore.WebAPI.Controllers
{
    public class OfferController : BaseApiController<OfferEntity,OfferModel>
    {
        private readonly IOfferService _offerService;

        public OfferController(IOfferService offerService) :base(offerService)
        {
            _offerService = offerService;
        }

        public override IActionResult Add([FromBody] OfferModel model)
        {
            model.UserId = Convert.ToInt64(User?.FindFirstValue(ClaimTypes.NameIdentifier));
            return ApiResponse(_offerService.Add(model));
        }
        public override IActionResult Update([FromBody] OfferModel model)
        {
            model.UserId = Convert.ToInt64(User?.FindFirstValue(ClaimTypes.NameIdentifier));
            return ApiResponse(_offerService.Update(model));
        }
        
        [HttpPut("ApproveTheOffer")]
        public IActionResult ApproveTheOffer(long offerId)
        {
            return ApiResponse(_offerService.ApproveTheOffer(offerId));
        }

        [HttpPut("RejectTheOffer")]
        public IActionResult RejectTheOffer(long offerId)
        {
            return ApiResponse(_offerService.RejectTheOffer(offerId));
        }
    }
}
