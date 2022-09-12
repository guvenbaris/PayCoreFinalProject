using NHibernate;
using PayCore.Application.Interfaces.Sessions;
using PayCore.Domain.Entities;

namespace PayCore.Infrastructure.Sessions
{
    public class ContainerSession : MapperSession<Container>, IContainerSession
    {
        public ContainerSession(ISession session) : base(session)
        {
        }
    }
}
