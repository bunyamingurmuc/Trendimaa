using Trendeimaa.Entities.Interface;
using Trendimaa.Common.Enum;

namespace Trendeimaa.Entities
{
    public class Order:BaseEntity
    {
        public Order()
        {
            WalletItems = new List<WalletItem>();
        }
        public string? OrderNumber { get; set; }
        public string? SellerName { get; set; }
        public string? Name { get; set; }
        public Currency? Currency { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public string? Note { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Suburb { get; set; }
        public string? Region { get; set; }
        public int? Discount { get; set; }
        public double? CouponPrice { get; set; }
        public double? TotalPrice { get; set; }
        public DateTime? EstimatedDeliveryTime { get; set; } = DateTime.Now.AddDays(2);
        public DateTime? EstimatedShippingTime { get; set; } = DateTime.Now.AddDays(2);
        public OrderStatus OrderStatus { get; set; } = OrderStatus.GettingReady;
        //public OrderCancelReason? OrderCancelReason { get; set; }
        public DateTime? CompletionTime { get; set; }
        public int? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public int? SellerId { get; set; }
        public Seller? Seller { get; set; }
        public int? CouponId { get; set; }
        public Coupon? Coupon { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public List<WalletItem> WalletItems { get; set; }

    }
}
