using Trendeimaa.Entities.Interface;
using Trendimaa.Common.Enum;

namespace Trendeimaa.Entities
{
    public class Coupon:BaseEntity
    {
        public double Price { get; set; }
        public string Description { get; set; }
        public double MinOrderAmount { get; set; } 

        public Language Language{ get; set; }
        public Currency Currency{ get; set; }
        public DateTime ExpireTime { get; set; }
        public bool IsExist { get; set; }
        public int? SellerId { get; set; }
        public Seller? Seller { get; set; }
        public int? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }  
        public int? OrderId { get; set; }
        public Order? Order { get; set; }
        public int? CouponOfferId { get; set; }
        public CouponOffer? CouponOffer { get; set; }


                              
    }
}
