using NHibernate;
using PayCore.Application.Interfaces.Sessions;
using PayCore.Domain.Entities;

namespace PayCore.Infrastructure.Sessions
{
    public class CategorySession : MapperSession<CategoryEntity>, ICategorySession
    {
        public CategorySession(ISession session) : base(session)
        {
        }
    }
}
