using FluentValidation;
using Trendeimaa.Entities;
using Trendimaa.BLL.Interface;
using Trendimaa.DAL.Context;
using Trendimaa.DAL.UnitOfWork;

namespace Trendimaa.BLL.Abstract
{
    public class QuestionAnswerService : Service<QuestionAnswer>, IQuestionAnswerService
    {
        public QuestionAnswerService(IValidator<QuestionAnswer> validator, IUOW uow, TrendimaaContext context) : base(validator, uow, context)
        {
        }
    }
}
