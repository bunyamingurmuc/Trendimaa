using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Trendeimaa.Entities;
using Trendeimaa.Entities.Related;
using Trendimaa.BLL.Extension;
using Trendimaa.BLL.Interface;
using Trendimaa.Common;
using Trendimaa.Common.Enum;
using Trendimaa.DAL.Context;
using Trendimaa.DAL.UnitOfWork;
using Trendimaa.DTO.Order;

namespace Trendimaa.BLL.Abstract
{
    public class OrderService : Service<Order>, IOrderService
    {
        public readonly IValidator<Order> _validator;
        public readonly IUOW _uow;
        public readonly TrendimaaContext _context;
        public readonly IMapper _mapper;
        public readonly INotificationService _notificationService;
        public OrderService(IValidator<Order> validator, IUOW uow, IMapper mapper, TrendimaaContext context, INotificationService notificationService) : base(validator, uow, context)
        {
            _validator = validator;
            _mapper = mapper;
            _context = context;
            _uow = uow;
            _notificationService = notificationService;
        }

        public async Task<IResponse<Order>> CreateOrder(Order order, int? couponOfferId)
        {
            var result = await _validator.ValidateAsync(order);
            var rnd = new Random();
            if (result.IsValid)
            {
                var myTransaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    order.OrderNumber = "ORF" + rnd.Next(0000000, 9999999);
                    order.OrderStatus = OrderStatus.GettingReady;
                    order.SellerName = await _context.Sellers.Where(i => i.Id == order.SellerId).Select(i => i.CompanyName).FirstOrDefaultAsync();
                    order.Address = await _context.Sellers.Where(i => i.Id == order.SellerId).Select(i => i.Address.AddressTopic).FirstOrDefaultAsync();

                    var created = await _uow.GetRepository<Order>().CreateAsync(order);

                    //check is AppUser login?
                    if (order.AppUserId != null)
                    {

                        //if user used coupon, getExist
                        if (order.CouponId != null)
                        {
                            var coupon = await _context.Coupons.FirstOrDefaultAsync(i => i.Id == order.CouponId);
                            var changedCoupoun = coupon;
                            changedCoupoun.IsExist = false;

                            _uow.GetRepository<Coupon>().Update(changedCoupoun, coupon);
                        }

                        //Remove user cartItems
                        var cardItems = await _context.CardItems.Where(i => i.Card.AppUserId == order.AppUserId).ToListAsync();
                        await _uow.GetRepository<CardItem>().RemoveRange(cardItems);


                        if (couponOfferId != null)
                        {
                            var couponOffer = await _context.CouponOffers
                                .Where(i => i.Id == couponOfferId)
                                .Include(i => i.SellerCouponOffers).ThenInclude(i => i.Seller)
                                .FirstOrDefaultAsync();
                            var sellerCoupons = new List<SellerCoupon>();
                            foreach (var sellercouponoffer in couponOffer.SellerCouponOffers)
                            {
                                sellerCoupons.Add(new SellerCoupon()
                                {
                                    SellerId = sellercouponoffer.SellerId
                                });
                            }

                            var coupon = new Coupon()
                            {
                                Description = couponOffer.Description,
                                Language = couponOffer.Language,
                                Currency = couponOffer.Currency,
                                Price = couponOffer.CouponAmount,
                                ExpireTime = couponOffer.ExpireTime,
                                MinOrderAmount = couponOffer.MinOrderAmount.Value,
                                AppUserId = order.AppUserId,
                            };
                            await _uow.GetRepository<Coupon>().CreateAsync(coupon);
                            await _notificationService.SendNotificationEmployee(created.Id.Value);
                            if (couponOffer.Piece > 1)
                                couponOffer.Piece -= 1;

                            _uow.GetRepository<CouponOffer>().Update(couponOffer, _context.CouponOffers.FirstOrDefault(i => i.Id == couponOfferId));

                        }

                        var orderItems=order.OrderItems.Select(i=>i.Id).ToList();
                        var products = _context.Products.Where(i => orderItems.Contains(i.Id)).ToList();
                        if (products.Any(i=>i.CashbackAmount!=0))
                        {
                            var wallet = await _context.Wallets.Where(i => i.AppUserId == order.AppUserId).Include(i => i.WalletItems).FirstOrDefaultAsync();
                            foreach (var orderItem in order.OrderItems.Where(i => i.Product.CashbackAmount != 0).ToList())
                            {
                                var walletItem=new WalletItem
                                {
                                    IsPlus = true,
                                    OrderId = order.Id,
                                    WalletId = wallet.Id,
                                    WalletItemAmount =orderItem.Product.CashbackAmount.Value,
                                };
                                await _uow.GetRepository<WalletItem>().CreateAsync(walletItem);

                                var cashbackmountProducts = order.OrderItems.Where(i => i.Product.CashbackAmount != 0).ToList();
                               var cashbacktotalmount = cashbackmountProducts.Sum(i => i.Product.CashbackAmount);
                                var unc = wallet;
                                wallet.Amount += cashbacktotalmount.Value;
                               _uow.GetRepository<Wallet>().Update(wallet,unc);

                            }

                        }

                    }

                    await myTransaction.CommitAsync();
                    await _notificationService.SendNotificationSeller(created.Id.Value);
                    return new Response<Order>(ResponseType.Success, created);

                }
                catch (Exception e)
                {
                    await myTransaction.RollbackAsync();
                    return new Response<Order>(ResponseType.NotFound, e.ToString());
                }
            }
            else
                return new Response<Order>(order, result.ConvertToCustomValidationError());
        }

        public async Task<IResponse<List<OrderDTO>>> GetUserOrders(int userId)

        {
            var data = await _context.Orders.Where(i => i.AppUserId == userId)
                .Include(i => i.OrderItems).ThenInclude(i => i.Product)
                .AsNoTracking()
                .ToListAsync();
            var mapped = _mapper.Map<List<OrderDTO>>(data);
            return new Response<List<OrderDTO>>(ResponseType.Success, mapped);

        }

        public async Task<IResponse<List<OrderDTO>>> GetSellerOrders(int sellerId, OrderStatus orderStatus)
        {
            var data = await _context.Orders.Where(i => i.SellerId == sellerId && i.OrderStatus == orderStatus)
                 .Include(i => i.OrderItems).ThenInclude(i => i.Product)
                 .AsNoTracking()
                 .ToListAsync();
            var mapped = _mapper.Map<List<OrderDTO>>(data);
            return new Response<List<OrderDTO>>(ResponseType.Success, mapped);
        }
    }
}
