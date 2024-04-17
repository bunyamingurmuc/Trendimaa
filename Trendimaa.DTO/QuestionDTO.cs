using Trendeimaa.Entities;
using Trendeimaa.Entities.Interface;

namespace Trendimaa.DTO
{
    public class QuestionDTO:BaseEntity
    {
        public string Description { get; set; }
        public int? AnswerId { get; set; }
        public AnswerDTO? Answer { get; set; }

    }
}
