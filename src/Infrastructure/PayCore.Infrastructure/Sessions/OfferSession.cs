using NHibernate;
using PayCore.Application.Interfaces.Sessions;
using PayCore.Domain.Entities;

namespace PayCore.Infrastructure.Sessions
{
    public class OfferSession : MapperSession<OfferEntity>, IOfferSession
    {
        private readonly ISession _session;
        public OfferSession(ISession session) : base(session)
        {
            _session = session; 
        }
    }
}
