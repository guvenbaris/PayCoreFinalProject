using PayCore.Application.Interfaces.Cache;
using PayCore.Application.Interfaces.Services;
using PayCore.Application.Models;
using PayCore.Application.Utilities.BaseApiTools;
using PayCore.Domain.Entities;

namespace Paycore.WebAPI.Controllers
{
    public class PersonController : BaseApiController<PersonEntity, PersonModel>
    {
        private readonly IPersonService _personService;


        public PersonController(IPersonService personService, ICacheService cache) : base(personService)
        {
            _personService = personService;
        }
    }
}
