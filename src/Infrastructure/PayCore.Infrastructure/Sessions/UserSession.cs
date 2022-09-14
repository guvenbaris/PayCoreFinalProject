using NHibernate;
using PayCore.Application.Interfaces.Sessions;
using PayCore.Domain.Entities;

namespace PayCore.Infrastructure.Sessions
{
    public class UserSession : MapperSession<UserEntity>, IUserSession
    {
        public UserSession(ISession session) : base(session)
        {
        }
    }
}
