using NHibernate;
using PayCore.Application.Interfaces.Sessions;
using PayCore.Domain.Entities;
using System.Linq.Expressions;

namespace PayCore.Infrastructure.Sessions
{
    public class MapperSession<T> : IMapperSession<T> where T : BaseEntity
    {
        private readonly NHibernate.ISession _session;
        private ITransaction transaction;

        public MapperSession(NHibernate.ISession session)
        {
            _session = session;
            transaction = _session.BeginTransaction();
        }

        public IQueryable<T> Queries => _session.Query<T>();

        public void BeginTransaction()
        {
            transaction = _session.BeginTransaction();
        }

        public void CloseTransaction()
        {
            if (transaction.IsActive)
            {
                transaction.Dispose();
            }
        }

        public void Commit()
        {
            transaction.Commit();
        }

        public void Delete(T entity)
        {
            _session.Update(entity);
        }

        public void Rollback()
        {
            transaction.Rollback();
        }

        public void Add(T entity)
        {
            _session.Save(entity);
        }

        public virtual void Update(T entity)
        {
            _session.Merge(entity);
        }

        public T GetById(long id)
        {
            return _session.Get<T>(id);
        }

        public IEnumerable<T> Where(Expression<Func<T, bool>> filter)
        {
            return _session.Query<T>().Where(filter).AsEnumerable();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter)
        {
            return _session.Query<T>().FirstOrDefault(filter)!;
        }

        public IEnumerable<T> SearchWithIn(string columnName, string parameters)
        {
           return  _session.CreateSQLQuery($"SELECT * FROM offer where {columnName} in({parameters})").AddEntity(typeof(T)).List<T>();
        }
    }
}
