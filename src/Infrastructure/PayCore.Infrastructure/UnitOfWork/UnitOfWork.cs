using PayCore.Application.Exceptions;
using PayCore.Application.Interfaces.Sessions;
using PayCore.Application.Interfaces.UnitOfWork;
using PayCore.Application.Models;
using PayCore.Domain.Entities;
using PayCore.Infrastructure.Sessions;

namespace PayCore.Infrastructure.UnitOfWork;

public class UnitOfWork<TEntity,TModel> : IUnitOfWork<TEntity,TModel>
    where TEntity : BaseEntity where TModel : BaseModel
{
    public IMapperSession<TEntity> Session { get; }

    public UnitOfWork(NHibernate.ISession session)
    {
        Session = new MapperSession<TEntity>(session);
    }

    public TEntity Add(TEntity entity)
    {
        try
        {
            this.Session.BeginTransaction();
            this.Session.Add(entity);
            this.Session.Commit();
        }
        catch (Exception)
        {
            this.Session.Rollback();
            throw new CustomException("Nhibernate Add Error.");
        }
        finally
        {
            this.Session.CloseTransaction();
        }

        return entity;
    }

    public TEntity Update(TEntity entity)
    {
        try
        {
            this.Session.BeginTransaction();
            this.Session.Update(entity);
            this.Session.Commit();
        }
        catch (Exception)
        {
            this.Session.Rollback();
            throw new CustomException("Nhibernate Update Error.");
        }
        finally
        {
            this.Session.CloseTransaction();
        }
        return entity;
    }

    public TEntity Delete(TEntity entity)
    {
        try
        {
            this.Session.BeginTransaction();
            this.Session.Update(entity);
            this.Session.Commit();
        }
        catch (Exception)
        {
            this.Session.Rollback();
            throw new CustomException("Nhibernate Delete Error.");
        }
        finally
        {
            this.Session.CloseTransaction();
        }
        return entity;
    }
}
