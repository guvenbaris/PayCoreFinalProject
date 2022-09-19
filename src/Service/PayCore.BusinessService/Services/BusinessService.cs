using System.Linq.Expressions;
using AutoMapper;
using PayCore.Application.Constant.Error;
using PayCore.Application.Interfaces.Services;
using PayCore.Application.Interfaces.UnitOfWork;
using PayCore.Application.Models;
using PayCore.Application.Utilities.Results;
using PayCore.Domain.Entities;
using Serilog;

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

        public virtual TModel GetById(long id)
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
            var rawData = _unitOfWork.Session.Where(filter).ToList();
            return _mapper.Map<List<TModel>>(rawData);
        }

        public virtual IDataResult Add(TModel model)
        {
            var entity = _mapper.Map<TEntity>(model);

            var addedEntity = _unitOfWork.Add(entity);

            if (addedEntity is null)
            {
                Log.Warning($"BusinessService.Add : {ErrorConstant.BusinessAddError}");
                return new ErrorDataResult { ErrorMessage = ErrorConstant.BusinessAddError };
            }
            return new SuccessDataResult { Data = _mapper.Map<TModel>(addedEntity) };
        }

        public virtual IDataResult Update(TModel model)
        {
            if (model.Id is null)
                return new ErrorDataResult { ErrorMessage = "Entity didn't update." };

            var updatedEntity = _unitOfWork.Update(_mapper.Map<TEntity>(model));

            if (updatedEntity is null)
            {
                Log.Warning($"BusinessService.Update : {ErrorConstant.BusinessUpdateError}");
                return new ErrorDataResult { ErrorMessage = ErrorConstant.BusinessUpdateError };
            }

            return new SuccessDataResult { Data = _mapper.Map<TModel>(updatedEntity) };
        }

        public virtual IDataResult Delete(long id)
        {
            var deletedEntity = _unitOfWork.Session.GetById(id);
            if (deletedEntity is not null)
            {
                deletedEntity.IsDeleted = true;
                _unitOfWork.Update((TEntity)deletedEntity);
                return new SuccessDataResult { Data = _mapper.Map<TModel>(deletedEntity) };
            }
            else
            {
                Log.Warning($"BusinessService.Delete : {ErrorConstant.BusinessDeleteError}");
                return new ErrorDataResult { ErrorMessage = ErrorConstant.BusinessDeleteError };
            }
        }

        public IEnumerable<TModel> SearchWithIn(string columnName, string parameters)
        {
            var entities = _unitOfWork.Session.SearchWithIn(columnName, parameters).ToList();
            return  _mapper.Map<List<TModel>>(entities);
        }
    }
}
