using PayCore.Application.Interfaces.Services;
using PayCore.Application.Models;
using PayCore.Application.Utilities.BaseApiTools;
using PayCore.Domain.Entities;

namespace Paycore.WebAPI.Controllers
{
    public class ManagerController :BaseApiController<ManagerEntity,ManagerModel>
    {
        private readonly IManagerService _managerService;

        public ManagerController(IManagerService managerService) : base(managerService)
        {
            _managerService = managerService;
        }
    }
}
