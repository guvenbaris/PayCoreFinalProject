using PayCore.Domain.Entities;

namespace PayCore.Application.Interfaces.Sessions
{
    public interface IProductSession : IMapperSession<ProductEntity>
    {
        List<ProductEntity> GetAllOver();
        
    }
}