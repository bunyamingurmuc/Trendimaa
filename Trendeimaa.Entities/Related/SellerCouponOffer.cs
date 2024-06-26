using Trendeimaa.Entities;

namespace Orde.Entity.RelatedTables
{
    public class SellerCouponOffer
    {
        public int? SellerId { get; set; }
        public Seller? Seller { get; set; }  
        public int? CouponOfferId { get; set; }
        public CouponOffer? CouponOffer { get; set; }
     
 
    }
}
