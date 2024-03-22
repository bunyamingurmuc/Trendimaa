using FluentValidation;
using Trendeimaa.Entities;
using Trendimaa.BLL.Interface;
using Trendimaa.DAL.Context;
using Trendimaa.DAL.UnitOfWork;

namespace Trendimaa.BLL.Abstract
{
    public class OrderService : Service<Order>, IOrderService
    {
        public OrderService(IValidator<Order> validator, IUOW uow, TrendimaaContext context) : base(validator, uow, context)
        {
        }
    }
}
