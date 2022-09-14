using PayCore.Application.Interfaces.Cache;
using PayCore.Application.Interfaces.Services;
using PayCore.Application.Models;
using PayCore.Application.Utilities.BaseApiTools;
using PayCore.Domain.Entities;

namespace Paycore.WebAPI.Controllers
{
    public class ApartmentController : BaseApiController<ApartmentEntity, ApartmentModel>
    {
        private readonly IApartmentService _apartmentService;


        public ApartmentController(IApartmentService apartmentService, ICacheService cache) : base(apartmentService)
        {
            _apartmentService = apartmentService;
        }
    }
}
