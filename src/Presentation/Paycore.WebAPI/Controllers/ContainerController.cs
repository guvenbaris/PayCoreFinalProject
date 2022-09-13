using Microsoft.AspNetCore.Mvc;
using PayCore.Application.Interfaces.Cache;
using PayCore.Application.Interfaces.Services;
using PayCore.Application.Models;
using PayCore.Application.Utilities.BaseApiTools;
using PayCore.Domain.Entities;

namespace Paycore.WebAPI.Controllers
{
    public class ContainerController :BaseApiController<Container,ContainerModel>
    {
        private readonly IContainerService _containerService;
        private readonly ICacheService cache;


        public ContainerController(IContainerService containerService, ICacheService cache) : base(containerService)
        {
            _containerService = containerService;
            this.cache = cache;
        }

        [HttpGet("CacheleBeni")]
        public IActionResult CacheDenem()
        {
            this.cache.Delete("a");
            return Ok();
        }

    }
}
