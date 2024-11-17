using Microsoft.AspNetCore.Mvc;
using Trendeimaa.Entities;
using Trendimaa.API.Extension;
using Trendimaa.BLL.Interface;
using Trendimaa.DTO;

namespace Trendimaa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellerController : ControllerBase
    {
        public readonly ISellerService _service;

        public SellerController(ISellerService service)
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
        public async Task<ActionResult> SignUp(Seller entity)
        {
            var response = await _service.SignUp(entity);
            return this.ResponseStatusWithData(response);
        }
        [HttpPost]
        [Route("/[controller]/[action]")]
        public async Task<ActionResult> LogIn(CSellerLoginDto dto)
        {
            var response = await _service.LogIn(dto);
            return this.ResponseStatusWithData(response);

        }

        [HttpPut]
        [Route("/[controller]/[action]")]
        public async Task<ActionResult> UpdateAsy(Seller entity)
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
        [HttpPost]
        [Route("/[controller]/[action]")]
        public async Task<ActionResult> up()
        {

            var response = await _service.up();
            return this.ResponseStatusWithData(response);
        }

        [HttpGet]
        [Route("/[controller]/[action]")]
        public async Task<ActionResult> GetSellers(int quantity, int page, string? word)
        {

            var response = await _service.GetSellers(quantity,page,word);
            return this.ResponseStatusWithData(response);
        }
        [HttpGet]
        [Route("/[controller]/[action]")]
        public async Task<ActionResult> GetSellerInfo(int sellerId)
        {

            var response = await _service.GetSellerInfo(sellerId);
            return this.ResponseStatusWithData(response);
        }
        [HttpGet]
        [Route("/[controller]/[action]")]
        public async Task<ActionResult> GetSellerProducts(int sellerId, int quantity, int page, string? word, bool isStock)
        {

            var response = await _service.GetSellerProducts(sellerId,quantity,page,word,isStock);
            return this.ResponseStatusWithData(response);
        }
        [HttpGet]
        [Route("/[controller]/[action]")]
        public async Task<ActionResult> GetSellerOrders(int sellerId, int quantity, int page, string? word)
        {

            var response = await _service.GetSellerOrders(sellerId, quantity,page,word);
            return this.ResponseStatusWithData(response);
        } 
        [HttpGet]
        [Route("/[controller]/[action]")]
        public async Task<ActionResult> GetSellerMainPageInfo(int sellerId)
        {

            var response = await _service.GetSellerMainPageInfo(sellerId);
            return this.ResponseStatusWithData(response);
        }
    }
}
