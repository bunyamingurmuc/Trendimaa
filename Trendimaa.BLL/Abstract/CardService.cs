using FluentValidation;
using Trendeimaa.Entities;
using Trendimaa.BLL.Interface;
using Trendimaa.DAL.Context;
using Trendimaa.DAL.UnitOfWork;

namespace Trendimaa.BLL.Abstract
{
    public class CardService : Service<Card>, ICardService
    {
        public CardService(IValidator<Card> validator, IUOW uow, TrendimaaContext context) : base(validator, uow, context)
        {
        }
    }
}
