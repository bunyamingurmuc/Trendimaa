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
    public class CardService : Service<Card>, ICardService
    {
        public readonly IMapper _mapper;
        public readonly TrendimaaContext _context;
        public readonly IValidator<Card> _validator;
        public readonly IUOW _uow;
        public CardService(IValidator<Card> validator, IUOW uow, IMapper mapper, TrendimaaContext context) : base(validator, uow, context)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
            _uow = uow;
        }

        public async Task<IResponse<Card>> GetUserCard(int appuserId)
        {
            var card =await _context.Cards
                .Where(i => i.AppUserId == appuserId)
                .Include(i=>i.CardItems)

                .ThenInclude(i=>i.Product).ThenInclude(i=>i.Images)
                
                .FirstOrDefaultAsync();
            return new Response<Card>(ResponseType.Success,card);
        }

        //public async Task<IResponse<int>> GetProductCardCount(int id)
        //{
        //    var count =  _context.Products.Where(i => i.Id == id)
        //        .Select(i=>i.CardItems.Count());


        //        ;

        //}
    }
}
