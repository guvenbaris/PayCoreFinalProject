using NHibernate;
using PayCore.Application.Interfaces.Sessions;
using PayCore.Domain.Entities;

namespace PayCore.Infrastructure.Sessions
{
    public class ManagerSession : MapperSession<ManagerEntity>, IManagerSession
    {
        public ManagerSession(ISession session) : base(session)
        {
        }
    }
}
