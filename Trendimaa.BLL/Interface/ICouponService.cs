
using Trendeimaa.Entities;
using Trendimaa.Common;
using Trendimaa.DTO;
using Trendimaa.DTO.Product;

namespace Trendimaa.BLL.Interface
{
    public interface ICouponService:IService<Coupon>
    {
        Task<Response<List<CouponDTO>>> GetUserCoupons(int userId);
        Task<Response<List<BasicProductCardDTO>>> GetCouponProducts(int couponId);
        Task<Response<List<ImageDTO>>> GetCouponProductImages(int couponId);
    }
}
