using Trendeimaa.Entities;
using Trendimaa.Common.Enum;
using Trendimaa.DTO.OrderItem;
using Trendimaa.DTO.Seller;

namespace Trendimaa.DTO.Order
{
    public class OrderDTO
    {
        public string? OrderNumber { get; set; }
        public string? OrderImage { get; set; }
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
        public OrderStatus OrderStatus { get; set; } 
        public DateTime? CompletionTime { get; set; }
        public DateTime? EstimatedDeliveryTime { get; set; }

        public int? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public int? SellerId { get; set; }
        public SellerCardDTO? Seller { get; set; }
       
        public List<OrderItemDTO> OrderItems { get; set; }
    }
}
