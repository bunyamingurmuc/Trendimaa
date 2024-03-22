using FluentValidation;
using Trendeimaa.Entities;
using Trendimaa.BLL.Interface;
using Trendimaa.DAL.Context;
using Trendimaa.DAL.UnitOfWork;

namespace Trendimaa.BLL.Abstract
{
    public class CommentService : Service<Comment>, ICommentService
    {
        public CommentService(IValidator<Comment> validator, IUOW uow, TrendimaaContext context) : base(validator, uow, context)
        {
        }
    }
}
