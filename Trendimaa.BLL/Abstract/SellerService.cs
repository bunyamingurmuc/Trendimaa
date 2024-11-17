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
using Trendimaa.DTO.Listing;
using Trendimaa.DTO.Order;
using Trendimaa.DTO.Product;
using Trendimaa.DTO.Seller;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            var seller = await _context.Sellers.Where(i => i.Id == sellerId)
                 .Include(i => i.Orders)
                 .FirstOrDefaultAsync();
            var outofStockCount = await _context.Products.Where(i => i.SellerId == sellerId && i.StockPiece == 0).CountAsync();
            var pendingReturns = seller.Orders.Where(i => i.OrderStatus == Common.Enum.OrderStatus.Returned).Count();
            var delayedOrders = seller.Orders.Where(i => i.EstimatedDeliveryTime >= DateTime.Now && i.OrderStatus == Common.Enum.OrderStatus.OnTheRoad).Count();
            var pendingOrders = seller.Orders.Where(i => i.OrderStatus == Common.Enum.OrderStatus.Paid).Count();
            var dailySalesAmount = seller.Orders.Where(i => i.CreatedDate.Day == DateTime.Now.Day).Sum(i => i.TotalPrice);
            var MountlySalesAmount = seller.Orders.Where(i => i.CreatedDate.Day == DateTime.Now.Day).Sum(i => i.TotalPrice);
            var PreviousdailySalesAmount = seller.Orders.Where(i => i.CreatedDate.Day == DateTime.Now.AddDays(-1).Day).Sum(i => i.TotalPrice);
            var PreviousSalesAmount = seller.Orders.Where(i => i.CreatedDate.Month == DateTime.Now.Month).Sum(i => i.TotalPrice);
            return new Response<SellerMainHomeDataDTO>(ResponseType.Success, new SellerMainHomeDataDTO());



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

        public async Task<IResponse<ListingDTO<SellerCardDTO>>> GetSellers(int quantity, int page, string? word)
        {
            var data = await _context.Sellers
                .Include(i => i.Image)
                .OrderBy(i => i.CreatedDate)
                .ToListAsync();

            if (word != null && word.Length >= 2)
            {
                word = word.ToLower().Trim();
                data = data
               .Where(i => i.CompanyFullName.ToLower().Contains(word) || i.CompanyName.ToLower().Contains(word) || i.TaxIdentificationNumber.ToString().ToLower().Contains(word))
               .OrderByDescending(i => i.CreatedDate)
               .ToList();
            }
            var mapped = _mapper.Map<List<SellerCardDTO>>(data);
            ListingDTO<SellerCardDTO> dto = new ListingDTO<SellerCardDTO>()
            {
                Count = mapped.Count,
                List = mapped.Skip((page - 1) * quantity).Take(quantity).ToList(),
            };
            return new Response<ListingDTO<SellerCardDTO>>(ResponseType.Success, dto);

        }

        public async Task<IResponse<Seller>> GetSellerInfo(int sellerId)
        {
            var seller = await _context.Sellers.Where(i => i.Id == sellerId)
                .Include(i => i.Image)
                .FirstOrDefaultAsync();
            return new Response<Seller>(ResponseType.Success, seller);
        }

        public async Task<IResponse<ListingDTO<BasicProductCardDTO>>> GetSellerProducts(int sellerId, int quantity, int page, string? word, bool isStock)
        {
            var data = await _context.Products.Where(i => i.SellerId == sellerId && i.IsStock == isStock)
                .Include(i => i.Images)
                .ToListAsync();

            var Count = 0;
            if (word != null && word.Length >= 3)
            {
                word = word.ToLower().Trim();
                data = data
               .Where(i => i.Name.ToLower().Contains(word) || i.Description.ToLower().Contains(word))
               .OrderByDescending(i => i.CreatedDate)
               .ToList();
            }
            var mapped = _mapper.Map<List<BasicProductCardDTO>>(data);
            ListingDTO<BasicProductCardDTO> dto = new ListingDTO<BasicProductCardDTO>()
            {
                Count = Count,
                List = mapped.Skip((page - 1) * quantity).Take(quantity).ToList(),
            };
            return new Response<ListingDTO<BasicProductCardDTO>>(ResponseType.Success, dto);
        }
        public async Task<IResponse<ListingDTO<OrderDTO>>> GetSellerOrders(int sellerId, int quantity, int page, string? word)
        {
            var data = await _context.Orders.Where(i => i.SellerId == sellerId)
                .Include(i => i.OrderItems)
                .AsNoTracking()
                .ToListAsync();
            var Count = 0;

            if (word != null && word.Length >= 3)
            {
                word = word.ToLower().Trim();
                data = data
               .Where(i => i.Name.ToLower().Contains(word) || i.OrderNumber.ToLower().Contains(word))
               .OrderByDescending(i => i.CreatedDate)
               .ToList();
            }
            var mapped = _mapper.Map<List<OrderDTO>>(data);
            ListingDTO<OrderDTO> dto = new ListingDTO<OrderDTO>()
            {
                Count = Count,
                List = mapped.Skip((page - 1) * quantity).Take(quantity).ToList(),
            };
            return new Response<ListingDTO<OrderDTO>>(ResponseType.Success, dto);
        }

        public async Task<IResponse<SellerMainPageDto>> GetSellerMainPageInfo(int sellerId)
        {
            var totalQuestionCount = _context.Sellers
                  .Where(i => i.Id == sellerId)
                  .SelectMany(i => i.Products.Select(i => i.Questions)).Count();

            var nonAnsweredQuestionCount = _context.Sellers
                  .Where(i => i.Id == sellerId)
                  .SelectMany(i => i.Products)
                  .SelectMany(p => p.Questions)
                  .Count(q => q.AnswerId == null);
            var totalProductCount = _context.Sellers.
                Where(i => i.Id == sellerId)
                .Select(i => i.Products)
                .Count();
            var totalOrderCount = _context.Sellers.
                Where(i => i.Id == sellerId)
                .Select(i => i.Orders)
                .Count();
             var delayedOrderCount = _context.Sellers.
                Where(i => i.Id == sellerId)
                .Select(i => i.Orders.Where(i=>i.EstimatedShippingTime>=DateTime.Now))
                .Count();

            var returnedOrderCount = _context.Sellers.
                Where(i => i.Id == sellerId)
                .Select(i => i.Orders.Where(i=>i.OrderStatus==Common.Enum.OrderStatus.Returned))
                .Count();

            var sendedOrderCount = _context.Sellers.
                Where(i => i.Id == sellerId)
                .Select(i => i.Orders.Where(i => i.OrderStatus == Common.Enum.OrderStatus.OnTheRoad))
                .Count();

            var totalPaymentCount = _context.Sellers.
                Where(i => i.Id == sellerId)
                .Select(i => i.Orders.Sum(i=>i.TotalPrice))
                .Count();

            var willPaymentCount = _context.Sellers.
                Where(i => i.Id == sellerId)
                .Select(i => i.Orders.Where(i=>i.OrderStatus==Common.Enum.OrderStatus.Aproved).Sum(i => i.TotalPrice))
                .Count();

            var montlyOrderCount = _context.Sellers.
               Where(i => i.Id == sellerId)
               .Select(i => i.Orders.Where(i => i.CreatedDate<=DateTime.Now.AddDays(-30)))
               .Count();

             var montlyCanceledOrderCount = _context.Sellers.
               Where(i => i.Id == sellerId)
               .Select(i => i.Orders.Where(i => i.CreatedDate<=DateTime.Now.AddDays(-30)&&i.OrderStatus==Common.Enum.OrderStatus.Returned))
               .Count();
            
            var settedAlarmProductCount =await _context.Sellers.
               Where(i => i.Id == sellerId)
               .SelectMany(i=>i.Products)
               .Select(i=>i.AlarmItems)
               .CountAsync();
            var dto = new SellerMainPageDto()
            {
                TotalOrderCount = totalOrderCount,
                NonAnsweredQuestionCount = nonAnsweredQuestionCount,
                TotalProductCount = totalProductCount,
                TotalQuestionCount = totalQuestionCount,
                DelayedOrderCount = delayedOrderCount,
                ReturnedOrderCount = returnedOrderCount,
                SendedOrderCount = sendedOrderCount,
                TotalPaymentCount= totalPaymentCount,
                WillPaymentCount = willPaymentCount,
                MontlyOrderCount = montlyOrderCount,
                MontlyCanceledOrderCount= montlyCanceledOrderCount,
                SettedAlarmProductCount=settedAlarmProductCount
            };
            return new Response<SellerMainPageDto>(ResponseType.Success, dto);

        }
    
    
    
    }
}
