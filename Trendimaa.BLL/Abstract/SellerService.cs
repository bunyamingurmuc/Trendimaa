using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Orde.BLL.Extension.Token;
using System.Diagnostics;
using System.Linq;
using Trendeimaa.Entities;
using Trendimaa.BLL.Extension;
using Trendimaa.BLL.Extension.Token;
using Trendimaa.BLL.Interface;
using Trendimaa.Common;
using Trendimaa.DAL.Context;
using Trendimaa.DAL.UnitOfWork;
using Trendimaa.DTO;
using Trendimaa.DTO.Seller;

namespace Trendimaa.BLL.Abstract
{
    public class SellerService : Service<Seller>, ISellerService
    {
        public readonly IValidator<Seller> _validator;
        public readonly IUOW _uow;
        public readonly TrendimaaContext _context;
        public readonly IMapper _mapper;
        public SellerService(IValidator<Seller> validator, IUOW uow, TrendimaaContext context, IMapper mapper) : base(validator, uow, context)
        {
            _validator = validator;
            _uow = uow;
            _context = context;
            _mapper = mapper;
        }

        public async Task<IResponse<Seller>> SignUp(Seller seller)
        {
            var result = _validator.Validate(seller);
            if (!result.IsValid)
                return new Response<Seller>(seller, errors: result.ConvertToCustomValidationError());

            var rnd = new Random();
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            seller.Password = BCrypt.Net.BCrypt.HashPassword(seller.Password, salt);

            var data = await _uow.GetRepository<Seller>().CreateAsync(seller);
            return new Response<Seller>(ResponseType.Success, data);
        }
        public async Task<IResponse<JwtTokenResponse>> LogIn(CSellerLoginDto dto)
        {
            var sellers = await _uow.GetRepository<Seller>().GetAllAsync();
            var seller = sellers.FirstOrDefault(i => i.EMail == dto.Email);
            if (seller == null)
                return new Response<JwtTokenResponse>(ResponseType.NotFound, "Kullanıcı bulunamadı");
            bool isValidPassword = BCrypt.Net.BCrypt.Verify(dto.Password, seller.Password);
            if (isValidPassword == false)
                return new Response<JwtTokenResponse>(ResponseType.NotFound, "Kullanıcı adı veya porala yanış");
            if (seller.IsActive == false)
                return new Response<JwtTokenResponse>(ResponseType.NotFound, "Üyeliğiniz daha aktif edilmedi");

            dto.Id = seller.Id;
            dto.Role = "Seller";
            dto.Currency = seller.Currency;
            dto.Language = seller.Language;
            var token = JwtTokenGenerator.GenerateTokenSeller(dto);
            return new Response<JwtTokenResponse>(ResponseType.Success, token);
        }
        public async Task<IResponse> up()
        {
            var seller = _context.Sellers.Where(i => i.Id == 2).FirstOrDefault();
            var unc = seller;
            seller.IsActive = true;
            _uow.GetRepository<Seller>().Update(seller, unc);
            _uow.SaveChangesAsync();
            return new Response(ResponseType.Success);
        }

        public async Task<IResponse<SellerMainHomeDataDTO>> GetSellerMainHomeData(int sellerId)
        {
           var seller=await _context.Sellers.Where(i=>i.Id== sellerId)
                .Include(i=>i.Orders)
                .FirstOrDefaultAsync();
            var outofStockCount=await _context.Products.Where(i=>i.SellerId==sellerId&&i.StockPiece==0).CountAsync();
            var pendingReturns = seller.Orders.Where(i=>i.OrderStatus==Common.Enum.OrderStatus.Returned).Count();
            var delayedOrders = seller.Orders.Where(i=>i.EstimatedDeliveryTime>=DateTime.Now&&i.OrderStatus==Common.Enum.OrderStatus.OnTheRoad).Count();
            var pendingOrders = seller.Orders.Where(i=>i.OrderStatus==Common.Enum.OrderStatus.Paid).Count();
            var dailySalesAmount = seller.Orders.Where(i => i.CreatedDate.Day == DateTime.Now.Day).Sum(i=>i.TotalPrice);
            var MountlySalesAmount = seller.Orders.Where(i => i.CreatedDate.Day == DateTime.Now.Day).Sum(i=>i.TotalPrice);
            var PreviousdailySalesAmount = seller.Orders.Where(i => i.CreatedDate.Day == DateTime.Now.AddDays(-1).Day).Sum(i=>i.TotalPrice);
            var PreviousSalesAmount = seller.Orders.Where(i => i.CreatedDate.Month == DateTime.Now.Month).Sum(i=>i.TotalPrice);
            return new Response<SellerMainHomeDataDTO>(ResponseType.Success ,new SellerMainHomeDataDTO());



        //         public int OutofStockCount { get; set; }
        //public int PendingReturns { get; set; }
        //public int DelayedOrders { get; set; }
        //public int PendingOrders { get; set; }
        //public double DailySalesAmount { get; set; }
        //public double WeeklySalesAmount { get; set; }
        //public double MountlySalesAmount { get; set; }
        //public double RateWeeklySalesAmount { get; set; }
        //public double RateMountlySalesAmount { get; set; }
    }
    }
}
