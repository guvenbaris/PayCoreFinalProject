using PayCore.Domain.Entities;
using System.Linq.Expressions;

namespace PayCore.Application.Interfaces.Sessions
{
    public interface IMapperSession<T> where T : BaseEntity
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
        void CloseTransaction();
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        IQueryable<T> Queries { get; }
        T GetById(long id);
        IEnumerable<T> Where(Expression<Func<T, bool>> filter);
        T GetFirstOrDefault(Expression<Func<T, bool>> filter);
        IEnumerable<T> SearchWithIn(string columnName, string parameters);
    }
}