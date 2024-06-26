using Microsoft.AspNetCore.Mvc;
using Trendeimaa.Entities;
using Trendimaa.API.Extension;
using Trendimaa.BLL.Interface;
using Trendimaa.Common.Enum;

namespace Trendimaa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public readonly IOrderService _service;

        public OrderController(IOrderService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("/[controller]/[action]")]
        public async Task<ActionResult> GetAllAsy()
        {
            var response = await _service.GetAllAsync();
            return this.ResponseStatusWithData(response);
        }

        [HttpGet]
        [Route("/[controller]/[action]")]
        public async Task<ActionResult> GetByIdAsy(int id)
        {
            var response = await _service.GetByIdAsync(id);
            return this.ResponseStatusWithData(response);
        }

        [HttpPost]
        [Route("/[controller]/[action]")]
        public async Task<ActionResult> CreateOrder(Order order, int? couponOfferId)
        {
            var response = await _service.CreateOrder(order,couponOfferId);
            return this.ResponseStatusWithData(response);

        }

        [HttpPut]
        [Route("/[controller]/[action]")]
        public async Task<ActionResult> UpdateAsy(Order entity)
        {
            var response = await _service.UpdateAsync(entity);
            return this.ResponseStatusWithData(response);
        }

        [HttpDelete]
        [Route("/[controller]/[action]")]
        public async Task<ActionResult> DeleteAsy(int id)
        {

            var response = await _service.RemoveAsync(id);
            return this.ResponseStatusWithData(response);
        } 
        [HttpGet]
        [Route("/[controller]/[action]")]
        public async Task<ActionResult> GetSellerOrders(int sellerId, OrderStatus orderStatus)
        {

            var response = await _service.GetSellerOrders(sellerId,orderStatus);
            return this.ResponseStatusWithData(response);
        } 
        [HttpGet]
        [Route("/[controller]/[action]")]
        public async Task<ActionResult> GetUserOrders(int userId)
        {

            var response = await _service.GetUserOrders(userId);
            return this.ResponseStatusWithData(response);
        }
    }
}
