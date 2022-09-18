using Microsoft.AspNetCore.Mvc;
using PayCore.Application.Interfaces.Services;
using PayCore.Application.Utilities.BaseApiTools;
using System.Security.Claims;

namespace Paycore.WebAPI.Controllers
{
    public class AccountDetailController : BaseApiResponse
    {
        private readonly IOfferService _offerService;
        public AccountDetailController(IOfferService offerService)
        {
            _offerService = offerService;
        }

        [HttpGet("GetUserProductOffers")]
        public IActionResult GetUserProductOffers()
        {
            long userId = Convert.ToInt64(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return ApiResponse(_offerService.GetUserProductOffers(userId));
        }

        [HttpGet("GetUserOffersOnProducts")]
        public IActionResult GetOffersOnUserProducts()
        {
            long userId = Convert.ToInt64(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return ApiResponse(_offerService.GetUserOffersOnProducts(userId));
        }
    }
}
