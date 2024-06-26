using Trendeimaa.Entities.Interface;

namespace Trendeimaa.Entities
{
    public class Notification:BaseEntity
    {
        public int? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public int? SellerId { get; set; }
        public Seller? Seller { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public int? ImageId { get; set; }
        public Image? Image { get; set; }
    }
}
