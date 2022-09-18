using System.Linq.Expressions;
using PayCore.Application.Models;
using PayCore.Application.Utilities.Results;
using PayCore.Domain.Entities;

namespace PayCore.Application.Interfaces.Services
{
    public interface IBusinessService<TEntity,TModel>
        where TEntity : BaseEntity
        where TModel : BaseModel
    {
        IEnumerable<TModel> GetAll();
        TModel GetById(long id);
        IEnumerable<TModel> Where(Expression<Func<TEntity, bool>> filter);
        TModel GetFirstOrDefault(Expression<Func<TEntity, bool>> filter);
        IDataResult Add(TModel model);
        IDataResult Update(TModel model);
        IDataResult Delete(long id);
        IEnumerable<TModel> SearchWithIn(string columnName, string parameters);
    }
}
