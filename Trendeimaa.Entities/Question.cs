using Trendeimaa.Entities.Interface;

namespace Trendeimaa.Entities
{
    public class Question:BaseEntity
    {
        public string FirstLatter { get; set; }
        public string LastLatter { get; set; }
        public string Description { get; set; }
        public DateTime? replyDate { get; set; } = DateTime.Now.AddDays(14);
        public int? ProductId { get; set; }
        public Product? Product { get; set; }
        public int? AppUserId { get; set; }
        public AppUser? AppUser { get; set; } 
        public int? AnswerId { get; set; }
        public Answer? Answer { get; set; }
    }
}
