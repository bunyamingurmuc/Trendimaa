using Trendeimaa.Entities;
using Trendeimaa.Entities.Interface;

namespace Trendimaa.DTO
{
    public class AnswerDTO:BaseEntity
    {
        public int? QuestionAnswerId { get; set; }
        public QuestionDTO? Question{ get; set; }
        public string Description { get; set; }
    }
}
