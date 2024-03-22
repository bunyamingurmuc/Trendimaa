using Trendeimaa.Entities.Interface;

namespace Trendeimaa.Entities
{
    public class Card:BaseEntity
    {

        public double? TotalPrice { get; set; }
        public int? Discount { get; set; }
        public int? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public double? CouponPrice { get; set; }
        public List<CardItem> CardItems { get; set; }
    }
}
