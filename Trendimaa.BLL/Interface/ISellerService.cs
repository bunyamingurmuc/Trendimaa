using Orde.BLL.Extension.Token;
using Trendeimaa.Entities;
using Trendimaa.Common;
using Trendimaa.DTO;

namespace Trendimaa.BLL.Interface
{
    public interface ISellerService:IService<Seller>
    {
        Task<IResponse<Seller>> SignUp(Seller seller);
        
        Task<IResponse<JwtTokenResponse>> LogIn(CSellerLoginDto dto);
        Task<IResponse> up();
    }
}
