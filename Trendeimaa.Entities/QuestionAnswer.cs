using Trendeimaa.Entities.Interface;

namespace Trendeimaa.Entities
{
    public class QuestionAnswer:BaseEntity
    {
        public string Description { get; set; }
        public int? ProductId { get; set; }
        public Product? Product { get; set; }
        public int? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public int? SellerId { get; set; }
        public Seller? Seller { get; set; }

    }
}
