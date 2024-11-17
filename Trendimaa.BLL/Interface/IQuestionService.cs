using Trendeimaa.Entities;
using Trendimaa.Common;
using Trendimaa.DTO;

namespace Trendimaa.BLL.Interface
{
    public interface IQuestionService:IService<Question>
    {
        Task<IResponse<List<QuestionDTO>>> GetProductQuestions(int? productId);
        Task<IResponse<List<QuestionDTO>>> GetSellerQuestions(int? sellerId);
        Task<IResponse<List<QuestionDTO>>> GetSellerNonAnsweredQuestions(int? sellerId);
        Task<IResponse<List<QuestionDTO>>> GetSellerAnsweredQuestions(int? sellerId);
    
    }
}
