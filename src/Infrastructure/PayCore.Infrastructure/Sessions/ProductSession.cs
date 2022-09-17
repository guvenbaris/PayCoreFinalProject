using NHibernate;
using PayCore.Application.Interfaces.Sessions;
using PayCore.Domain.Entities;

namespace PayCore.Infrastructure.Sessions
{
    public class ProductSession : MapperSession<ProductEntity>, IProductSession
    {
        private readonly ISession _session;
        public ProductSession(ISession session) : base(session)
        {
            _session = session;
        }

        public List<ProductEntity> GetAllOver()
        {
            //var deneme = _session.QueryOver<ProductEntity>().Where(x => x.IsDeleted == false).Fetch(SelectMode.Fetch, x => x.Category).List();
            //var deneme = from conf in _session.Query<ProductEntity>()
            //             .Fetch(FetchMode.Select);

            return null;
        }
    }
}
//var result = (from conf in s.Query<A>()
//    .FetchMany(x => x.Bset)
//    .ThenFetch(x => x.Dprop)
//    .FetchMany(x => x.Bset)
//    .ThenFetchMany(x => x.Cset)
//              where conf.Id == 42
//              select conf).SingleOrDefault();
