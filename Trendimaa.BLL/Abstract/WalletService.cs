using AutoMapper;
using Azure;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Trendeimaa.Entities;
using Trendimaa.BLL.Interface;
using Trendimaa.Common;
using Trendimaa.DAL.Context;
using Trendimaa.DAL.UnitOfWork;
using Trendimaa.DTO.WalletDTOs;

namespace Trendimaa.BLL.Abstract
{
    public class WalletService : Service<Wallet>, IWalletService
    {
        public readonly IMapper _mapper;
        public readonly TrendimaaContext _context;
        public readonly IValidator<Wallet> _validator;
        public readonly IUOW _uow;
        public WalletService(IValidator<Wallet> validator, IUOW uow,IMapper mapper, TrendimaaContext context) : base(validator, uow, context)
        {
            _mapper = mapper;
            _context = context;
            _validator = validator;
            _uow= uow;
        }

        public async Task<Common.Response<WalletDTO>> GeyUserWallet(int userId)
        {
          var wallet= await _context.Wallets.Where(i=>i.AppUserId==userId).Include(i=>i.WalletItems).FirstOrDefaultAsync();
        var mapped=_mapper.Map<WalletDTO>(wallet);
            return new Common.Response<WalletDTO>(ResponseType.Success,mapped);
        }
    }
}
