using System.Linq.Expressions;
using AutoMapper;
using PayCore.Application.Interfaces.Services;
using PayCore.Application.Interfaces.UnitOfWork;
using PayCore.Application.Models;
using PayCore.Application.Utilities.Results;
using PayCore.Domain.Entities;

namespace PayCore.BusinessService.Services
{
    public abstract class BusinessService<TEntity, TModel> : IBusinessService<TEntity, TModel>
        where TEntity : BaseEntity
        where TModel : BaseModel
    {
        private readonly IUnitOfWork<TEntity, TModel> _unitOfWork;
        private readonly IMapper _mapper;

        public BusinessService(IUnitOfWork<TEntity, TModel> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public virtual TModel Get(int id)
        {
            var rawData = _unitOfWork.Session.GetById(id);
            return _mapper.Map<TModel>(rawData);
        }
        public virtual TModel GetById(int id)
        {
            var rawData = _unitOfWork.Session.GetById(id);
            return _mapper.Map<TModel>(rawData);
        }
        public virtual IEnumerable<TModel> GetAll()
        {
            var rawData = _unitOfWork.Session.Queries.ToList();
            return _mapper.Map<List<TModel>>(rawData);
        }

        public virtual TModel GetFirstOrDefault(Expression<Func<TEntity, bool>> filter)
        {
            var rawData = _unitOfWork.Session.GetFirstOrDefault(filter);
            return _mapper.Map<TModel>(rawData);
        }

        public virtual IEnumerable<TModel> Where(Expression<Func<TEntity, bool>> filter)
        {
            var rawData = _unitOfWork.Session.Where(filter);
            return _mapper.Map<List<TModel>>(rawData);
        }

        public virtual IDataResult Add(TModel model)
        {
            var addedEntity = _unitOfWork.Update(_mapper.Map<TEntity>(model));
            if (addedEntity is not null)
            {
                return new SuccessDataResult { Data = _mapper.Map<TModel>(addedEntity) };
            }

            return new ErrorDataResult { ErrorMessage = "Entity didn't added." };
        }

        public virtual IDataResult Update(TModel model)
        {
            var updatedEntity = _unitOfWork.Update(_mapper.Map<TEntity>(model));

            if (updatedEntity is not null)
            {
                return new SuccessDataResult { Data = _mapper.Map<TModel>(updatedEntity) };
            }

            return new ErrorDataResult { ErrorMessage = "Entity didn't update." };
        }

        public virtual IDataResult Delete(int id)
        {
            var deletedEntity = _unitOfWork.Session.GetById(id);
            if (deletedEntity is not null)
            {
                _unitOfWork.Update((TEntity)deletedEntity);
                return new SuccessDataResult { Data = _mapper.Map<TModel>(deletedEntity) };
            }
            else
            {
                return new ErrorDataResult { ErrorMessage = "Entity didn't deleted." };
            }
        }
    }
}
