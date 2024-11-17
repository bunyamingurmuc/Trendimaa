using Orde.BLL.Extension.Token;
using Trendeimaa.Entities;
using Trendimaa.Common;
using Trendimaa.DTO;
using Trendimaa.DTO.Listing;
using Trendimaa.DTO.Order;
using Trendimaa.DTO.Product;
using Trendimaa.DTO.Seller;

namespace Trendimaa.BLL.Interface
{
    public interface ISellerService:IService<Seller>
    {
        Task<IResponse<Seller>> SignUp(Seller seller);
        
        Task<IResponse<JwtTokenResponse>> LogIn(CSellerLoginDto dto);
        Task<IResponse<SellerMainHomeDataDTO>> GetSellerMainHomeData(int sellerId);
        Task<IResponse> up();
        Task<IResponse<ListingDTO<SellerCardDTO>>> GetSellers(int quantity,int page, string? word);
        Task<IResponse<Seller>> GetSellerInfo(int sellerId);
        Task<IResponse<ListingDTO<BasicProductCardDTO>>> GetSellerProducts(int sellerId, int quantity, int page, string word, bool isStock);
        Task<IResponse<ListingDTO<OrderDTO>>> GetSellerOrders(int sellerId, int quantity, int page,string word);
        Task<IResponse<SellerMainPageDto>> GetSellerMainPageInfo(int sellerId);
    }
}
