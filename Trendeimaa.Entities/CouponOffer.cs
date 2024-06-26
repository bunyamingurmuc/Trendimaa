using Orde.Entity.RelatedTables;
using Trendeimaa.Entities.Interface;
using Trendimaa.Common.Enum;

namespace Trendeimaa.Entities
{
    public class CouponOffer:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }


        public DateTime ExpireTime { get; set; }
        public Language Language { get; set; }
        public Currency Currency { get; set; }
        public long? RemainingSeconds { get; set; }
        public int? Piece { get; set; }
        public double CouponAmount { get; set; }
        public double? MinOrderAmount { get; set; }
        public bool IsOneTime { get; set; }
        public bool IsJustForSeller { get; set; }
        public bool IsForFirstTimeUser { get; set; }
        public List<Coupon> Coupons { get; set; }
        public List<SellerCouponOffer> SellerCouponOffers { get; set; }
    }
}
