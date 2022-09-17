using NHibernate;
using NHibernate.Transform;
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

        public IList<OfferEntity> GetUserOffers(long userId)
        {
            var data2 =  _session.CreateCriteria<OfferEntity>().SetFetchMode("Product", FetchMode.Eager).List();
            var deneme = _session.QueryOver<OfferEntity>().Fetch(SelectMode.FetchLazyProperties, x => x.Product).List();
            return null;
        }
    }
}
//var productFuture = CurrentSession.QueryOver<Product>()
//    .JoinAlias(x => x.Recommendations, () => recommendationAlias, JoinType.LeftOuterJoin)
//    .JoinAlias(() => recommendationAlias.Images, () => imageAlias, JoinType.LeftOuterJoin)
//    .Where(x => x.Id == ID)
//    .TransformUsing(Transformers.DistinctRootEntity)
//    .FutureValue();