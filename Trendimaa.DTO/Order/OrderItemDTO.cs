using Trendimaa.DTO.Order;

namespace Trendimaa.DTO.OrderItem
{
    public class OrderItemDTO
    {
        public double? ItemPrice { get; set; }
        public int? Tax { get; set; }
        public int? ItemDiscount { get; set; }
        public string? ItemName { get; set; }
        public string? ItemImageUrl { get; set; }
        public int Quantity { get; set; }
        public int? OrderId { get; set; }
        public OrderDTO? Order { get; set; }

    }
}
