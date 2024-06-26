using Orde.BLL.Extension.Token;
using Trendeimaa.Entities;
using Trendimaa.Common;
using Trendimaa.DTO;
using Trendimaa.DTO.Seller;

namespace Trendimaa.BLL.Interface
{
    public interface ISellerService:IService<Seller>
    {
        Task<IResponse<Seller>> SignUp(Seller seller);
        
        Task<IResponse<JwtTokenResponse>> LogIn(CSellerLoginDto dto);
        Task<IResponse<SellerMainHomeDataDTO>> GetSellerMainHomeData(int sellerId);
        Task<IResponse> up();
    }
}
