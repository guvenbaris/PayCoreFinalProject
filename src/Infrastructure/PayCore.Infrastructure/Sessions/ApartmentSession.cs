using NHibernate;
using PayCore.Application.Interfaces.Sessions;
using PayCore.Domain.Entities;

namespace PayCore.Infrastructure.Sessions
{
    public class ApartmentSession : MapperSession<ApartmentEntity>, IApartmentSession
    {
        public ApartmentSession(ISession session) : base(session)
        {
        }
    }
}
