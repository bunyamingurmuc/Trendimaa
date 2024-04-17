using Trendeimaa.Entities.Interface;

namespace Trendeimaa.Entities
{
    public class Question:BaseEntity
    {
        public string Description { get; set; }
        public int? ProductId { get; set; }
        public Product? Product { get; set; }
        public int? AppUserId { get; set; }
        public AppUser? AppUser { get; set; } 
        public int? AnswerId { get; set; }
        public Answer? Answer { get; set; }
    }
}
