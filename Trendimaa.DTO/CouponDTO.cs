using Trendeimaa.Entities;
using Trendimaa.Common.Enum;
using Trendimaa.DTO.Order;
using Trendimaa.DTO.Seller;

namespace Trendimaa.DTO
{
    public class CouponDTO
    {
        public double Price { get; set; }
        public string Description { get; set; }
        public double MinOrderAmount { get; set; }

        public Language Language { get; set; }
        public Currency Currency { get; set; }
        public DateTime ExpireTime { get; set; }
        public bool IsExist { get; set; }
        public int? SellerId { get; set; }
        public SellerCardDTO? Seller { get; set; }

        public int? OrderId { get; set; }
        public OrderDTO? Order { get; set; }
        public int? CouponOfferId { get; set; }
        public CouponOffer? CouponOffer { get; set; }
    }
}
