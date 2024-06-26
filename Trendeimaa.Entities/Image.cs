using Trendeimaa.Entities.Interface;

namespace Trendeimaa.Entities
{
    public class Image:BaseEntity
    {
        public string? Path { get; set; }
        public Product? Product { get; set; }
        public int? ProductId { get; set; } 
        public Category? Category { get; set; }
        public int? CategoryId { get; set; } 
        public Seller? Seller{ get; set; }
        public int? SellerId { get; set; } 
        public Comment? Comment { get; set; } 
        public int? CommentId { get; set; } 
        public int? CampaignId { get; set; } 
        public Campaign? Campaign{ get; set; } 
        public int? NotificationId { get; set; }
        public Notification? Notification{ get; set; }

    }
}
