using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Trendeimaa.Entities;
using Trendimaa.BLL.Interface;
using Trendimaa.Common;
using Trendimaa.DAL.Context;
using Trendimaa.DAL.UnitOfWork;

namespace Trendimaa.BLL.Abstract
{
    public class CreditCardService : Service<CreditCard>, ICreditCardService
    {
        public readonly IMapper _mapper;
        public readonly TrendimaaContext _context;
        public readonly IValidator<CreditCard> _validator;
        public readonly IUOW _uow;
        public CreditCardService(IValidator<CreditCard> validator, IUOW uow,IMapper mapper, TrendimaaContext context) : base(validator, uow, context)
        {
            _mapper = mapper;
            _context = context;
            _validator = validator;
            _uow = uow;
        }

        public async Task<Response<List<CreditCard>>> GetUserCCs(int id)
        {
            var ccs=await _context.CreditCards.Where(i=>i.AppUserId == id).ToListAsync();
            return new Response<List<CreditCard>>(ResponseType.Success,ccs);
        }
    }
}
