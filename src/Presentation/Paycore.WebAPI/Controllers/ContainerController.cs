using PayCore.Application.Interfaces.Services;
using PayCore.Application.Models;
using PayCore.Application.Utilities.BaseApiTools;
using PayCore.Domain.Entities;

namespace Paycore.WebAPI.Controllers
{
    public class ContainerController :BaseApiController<Container,ContainerModel>
    {
        private readonly IContainerService _containerService;

        public ContainerController(IContainerService containerService) : base(containerService)
        {
            _containerService = containerService;
        }

    }
}
