using AutoMapper;
using FluentValidation;
using Newtonsoft.Json;
using PayCore.Application.Extensions;
using PayCore.Application.Interfaces.Cache;
using PayCore.Application.Interfaces.Services;
using PayCore.Application.Interfaces.UnitOfWork;
using PayCore.Application.Models;
using PayCore.Application.Utilities.Results;
using PayCore.Application.Validations.ColorValidation;
using PayCore.Domain.Entities;

namespace PayCore.BusinessService.Services
{
    public class ColorService : BusinessService<ColorEntity, ColorModel>, IColorService
    {
        private readonly ICacheService _cacheService;
        public ColorService(IUnitOfWork<ColorEntity, ColorModel> unitOfWork, IMapper mapper, ICacheService cacheService) : base(unitOfWork, mapper)
        {
            _cacheService = cacheService;
        }
        public override IEnumerable<ColorModel> GetAll()
        {
            var cacheData = _cacheService.GetByKey("ColorServiceGetAll");

            if (cacheData.IsNotNullOrEmpty())
                return JsonConvert.DeserializeObject<IEnumerable<ColorModel>>(cacheData)!;

            var categories = base.GetAll().ToList();

            if (categories.IsNotNullOrEmpty())
                _cacheService.InsertValue("ColorServiceGetAll", JsonConvert.SerializeObject(categories), 1, 5);

            return categories;
        }

        public override IDataResult Add(ColorModel model)
        {
            var validator = new ColorValidator();
            validator.ValidateAndThrow(model);

            var result = base.Add(model);

            if (!result.IsSuccess)
                return new ErrorDataResult { ErrorMessage = "Could not add color" };

            _cacheService.DeleteIfContainName("ColorService");
            return result;
        }

        public override IDataResult Update(ColorModel model)
        {
            var validator = new ColorValidator();
            validator.ValidateAndThrow(model);

            var result = base.Update(model);

            if (!result.IsSuccess)
                return new ErrorDataResult { ErrorMessage = "Could not update color" };

            _cacheService.DeleteIfContainName("ColorService");
            return result;
        }
        public override IDataResult Delete(long id)
        {
            var result = base.Delete(id);

            if (!result.IsSuccess)
                return new ErrorDataResult { ErrorMessage = "Could not delete color" };

            _cacheService.DeleteIfContainName("ColorService");
            return result;
        }
    }
}
