using PayCore.Application.Models;
using PayCore.Application.Utilities.Results;
using PayCore.Domain.Entities;

namespace PayCore.Application.Interfaces.Services
{
    public interface IProductService : IBusinessService<ProductEntity, ProductModel>
    {
        IDataResult SellTheProduct(long productId,long userId);
    }
}
