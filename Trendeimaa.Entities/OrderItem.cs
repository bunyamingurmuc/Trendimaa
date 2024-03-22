using Trendeimaa.Entities.Interface;

namespace Trendeimaa.Entities
{
    public class OrderItem:BaseEntity
    {
        public double? ItemPrice { get; set; }
        public int? Tax { get; set; }
        public int? ItemDiscount { get; set; }
        public string? ItemName { get; set; }
        public string? ItemImageUrl { get; set; }
        public string? PortionName { get; set; }
        public int Quantity { get; set; }
        public string? MenuName { get; set; }
        public int? OrderId { get; set; }
        public Order? Order { get; set; }
        public int? ProductId { get; set; }
        public Product? Product { get; set; }
        public int? ProductMId { get; set; }
    }
}
