using FluentValidation;
using Trendeimaa.Entities;
using Trendimaa.BLL.Interface;
using Trendimaa.DAL.Context;
using Trendimaa.DAL.UnitOfWork;

namespace Trendimaa.BLL.Abstract
{
    public class CardItemService : Service<CardItem>, ICardItemService
    {
        public CardItemService(IValidator<CardItem> validator, IUOW uow, TrendimaaContext context) : base(validator, uow, context)
        {
        }
    }
}
