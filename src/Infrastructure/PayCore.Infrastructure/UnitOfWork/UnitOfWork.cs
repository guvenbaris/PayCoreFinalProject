using PayCore.Application.Constant.Error;
using PayCore.Application.Exceptions;
using PayCore.Application.Interfaces.Sessions;
using PayCore.Application.Interfaces.UnitOfWork;
using PayCore.Application.Models;
using PayCore.Domain.Entities;
using PayCore.Infrastructure.Sessions;
using Serilog;
using System.Net;

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
            Log.Error($"UnitOfWork.Add, {ErrorConstant.NhibernateAddError}");
            throw new CustomException(ErrorConstant.NhibernateAddError, HttpStatusCode.InternalServerError);
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
            Log.Error($"UnitOfWork.Update, {ErrorConstant.NhibernateUpdateError}");
            throw new CustomException(ErrorConstant.NhibernateUpdateError,HttpStatusCode.InternalServerError);
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
            Log.Error($"UnitOfWork.Delete, {ErrorConstant.NhibernateDeleteError}");
            throw new CustomException(ErrorConstant.NhibernateDeleteError, HttpStatusCode.InternalServerError);
        }
        finally
        {
            this.Session.CloseTransaction();
        }
        return entity;
    }
}
