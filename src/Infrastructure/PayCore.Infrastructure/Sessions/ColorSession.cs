using NHibernate;
using PayCore.Application.Interfaces.Sessions;
using PayCore.Domain.Entities;

namespace PayCore.Infrastructure.Sessions
{
    public class ColorSession : MapperSession<ColorEntity>, IColorSession
    {
        public ColorSession(ISession session) : base(session)
        {
        }
    }
}
