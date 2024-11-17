using Trendeimaa.Entities;
using Trendimaa.Common;
using Trendimaa.Common.Enum;
using Trendimaa.DTO.Listing;
using Trendimaa.DTO.Order;

namespace Trendimaa.BLL.Interface
{
    public interface IOrderService:IService<Order>
    {
        Task<IResponse<Order>> CreateOrder(Order order, int? couponOfferId);
        Task<IResponse<List<OrderDTO>>> GetUserOrders(int userId);
        Task<IResponse<ListingDTO<OrderDTO>>> GetSellerOrders(SellerOrdersFilterDTO dto);


    }
}
