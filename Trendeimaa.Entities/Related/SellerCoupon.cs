using Trendeimaa.Entities;

namespace Trendeimaa.Entities.Related
{
    public class SellerCoupon
    {
        public int? SellerId { get; set; }
        public Seller Seller { get; set; }
        public int CouponId { get; set; }
        public Coupon Coupon { get; set; }


    }
}
