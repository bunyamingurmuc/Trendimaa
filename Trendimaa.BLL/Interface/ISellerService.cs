using Trendeimaa.Entities;
using Trendimaa.Common;

namespace Trendimaa.BLL.Interface
{
    public interface ISellerService:IService<Seller>
    {
        Task<IResponse<Seller>> SignUp(Seller seller);
    }
}
