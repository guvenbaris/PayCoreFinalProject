using NHibernate;
using PayCore.Application.Interfaces.Sessions;
using PayCore.Domain.Entities;


namespace PayCore.Infrastructure.Sessions
{
    public class PersonSession : MapperSession<PersonEntity>, IPersonSession
    {
        public PersonSession(ISession session) : base(session)
        {
        }
    }
}
