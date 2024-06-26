using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trendeimaa.Entities;
using Trendimaa.API.Extension;
using Trendimaa.BLL.Interface;
using Notification = Trendeimaa.Entities.Notification;

namespace Trendimaa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        public readonly INotificationService _service;

        public NotificationController(INotificationService service)
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
        public async Task<ActionResult> CreateAsy(Notification entity)
        {
            var response = await _service.CreateAsync(entity);
            return this.ResponseStatusWithData(response);

        }

        [HttpPut]
        [Route("/[controller]/[action]")]
        public async Task<ActionResult> UpdateAsy(Notification entity)
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
        public async Task<BatchResponse> SendNotificationTestSeller(int sellerId, string title, string body)
        {

            var response = await _service.SendNotificationTestSeller(sellerId,title,body);
            return response;
        } 
        [HttpGet]
        [Route("/[controller]/[action]")]
        public async Task<ActionResult> GetUserNotification(int appuserId)
        {

            var response = await _service.GetUserNotification(appuserId);
            return this.ResponseStatusWithData(response);

        }
        [HttpGet]
        [Route("/[controller]/[action]")]
        public async Task<ActionResult> GetSellerNotification(int sellerId)
        {

            var response = await _service.GetSellerNotification(sellerId);
            return this.ResponseStatusWithData(response);

        }

    }
}
