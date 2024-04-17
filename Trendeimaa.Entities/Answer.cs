using Trendeimaa.Entities.Interface;

namespace Trendeimaa.Entities
{
    public class Answer:BaseEntity
    {
        public string Description { get; set; }
        public int? ProductId { get; set; }
        public Product? Product { get; set; }
        public int? SellerId { get; set; }
        public Seller? Seller { get; set; } 
        public int? QuestionId { get; set; }
        public Question? Question { get; set; }
    }
}
