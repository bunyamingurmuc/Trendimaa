using Trendimaa.Common.Enum;

namespace Trendimaa.DTO.Order
{
    public class SellerOrdersFilterDTO
    {
        public int sellerId { get; set; }
        public OrderStatus orderStatus { get; set; }
        public int quantity { get; set; }
        public int page { get; set; }
        public DateTime? orderStartDate { get; set; }
        public DateTime? orderEndDate { get; set; }
        public DateTime? shippingStartDate { get; set; }
        public DateTime? shippingEndDate { get; set; }

    }
}
