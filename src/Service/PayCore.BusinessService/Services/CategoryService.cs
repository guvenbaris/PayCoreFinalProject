using AutoMapper;
using PayCore.Application.Interfaces.Services;
using PayCore.Application.Interfaces.UnitOfWork;
using PayCore.Application.Models;
using PayCore.Application.Utilities.Results;
using PayCore.Domain.Entities;

namespace PayCore.BusinessService.Services
{
    public class CategoryService : BusinessService<CategoryEntity, CategoryModel>, ICategoryService
    {
        public CategoryService(IUnitOfWork<CategoryEntity, CategoryModel> unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public override IDataResult Add(CategoryModel model)
        {
           // validasyonlar eklenmeli 
           return base.Add(model);
        }
        public override IDataResult Update(CategoryModel model)
        {
            //validasyonlar eklenmeli
            return base.Update(model);  
        }
    }
}
