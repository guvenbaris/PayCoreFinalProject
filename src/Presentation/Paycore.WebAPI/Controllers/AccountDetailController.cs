using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PayCore.Application.Constant.Roles;
using PayCore.Application.Interfaces.HelperServices;
using PayCore.Application.Utilities.BaseApiTools;
using System.Security.Claims;

namespace Paycore.WebAPI.Controllers
{
    [Authorize(Roles = Role.Member)]
    public class AccountDetailController : BaseApiResponse
    {
        private readonly IAccountDetailService _accountDetailService;
        public AccountDetailController(IAccountDetailService accountDetailService)
        {
            _accountDetailService = accountDetailService;
        }

        [HttpGet("GetUserProductOffers")]
        public IActionResult GetUserProductOffers()
        {
            long userId = Convert.ToInt64(User?.FindFirstValue(ClaimTypes.NameIdentifier));
            return ApiResponse(_accountDetailService.GetUserProductOffers(userId));
        }

        [HttpGet("GetUserOffersOnProducts")]
        public IActionResult GetUserOffersOnProducts()
        {
            long userId = Convert.ToInt64(User?.FindFirstValue(ClaimTypes.NameIdentifier));
            return ApiResponse(_accountDetailService.GetUserOffersOnProducts(userId));
        }
    }
}

