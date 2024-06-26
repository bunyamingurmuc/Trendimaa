using Trendeimaa.Entities.Interface;
using Trendimaa.DTO.Product;

namespace Trendimaa.DTO
{
    public class QuestionDTO:BaseEntity
    {
        public string FirstLatter { get; set; }
        public DateTime? replyDate { get; set; }
        public string LastLatter { get; set; }
        public string Description { get; set; }
        public int? AnswerId { get; set; }
        public int? ProductId { get; set; }
        public BasicProductCardDTO? Product { get; set; }
        public AnswerDTO? Answer { get; set; }
  

    }
}
