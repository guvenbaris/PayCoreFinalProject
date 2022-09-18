using NHibernate;
using PayCore.Application.Interfaces.Sessions;
using PayCore.Domain.Entities;

namespace PayCore.Infrastructure.Sessions
{
    public class BrandSession : MapperSession<BrandEntity>, IBrandSesion
    {
        public BrandSession(ISession session) : base(session)
        {
        }
    }
}
