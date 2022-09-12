using PayCore.Application.Interfaces.Sessions;
using PayCore.Application.Models;
using PayCore.Domain.Entities;

namespace PayCore.Application.Interfaces.UnitOfWork
{
    public interface IUnitOfWork<TEntity,TModel> 
        where TEntity : BaseEntity
        where TModel : BaseModel
    {
        IMapperSession<TEntity> Session { get; }
        TEntity Add(TEntity entity);
        TEntity Update(TEntity entity);
        TEntity Delete(TEntity entity);
    }
}
