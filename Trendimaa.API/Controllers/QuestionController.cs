using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trendeimaa.Entities;
using Trendimaa.API.Extension;
using Trendimaa.BLL.Interface;

namespace Trendimaa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        public readonly IQuestionService _service;

        public QuestionController(IQuestionService service)
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
        public async Task<ActionResult> CreateAsy(Question entity)
        {
            var response = await _service.CreateAsync(entity);
            return this.ResponseStatusWithData(response);

        }

        [HttpPut]
        [Route("/[controller]/[action]")]
        public async Task<ActionResult> UpdateAsy(Question entity)
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
        public async Task<ActionResult> CreateRangeAsync(List<Question> list)
        {

            var response = await _service.CreateRangeAsync(list);
            return this.ResponseStatusWithData(response);
        }  
        [HttpGet]
        [Route("/[controller]/[action]")]
        public async Task<ActionResult> GetProductQuestions(int? productId)
        {

            var response = await _service.GetProductQuestions(productId);
            return this.ResponseStatusWithData(response);
        } 
        [HttpGet]
        [Route("/[controller]/[action]")]
        public async Task<ActionResult> GetSellerQuestions(int? sellerId)
        {

            var response = await _service.GetSellerQuestions(sellerId);
            return this.ResponseStatusWithData(response);
        }
         [HttpGet]
        [Route("/[controller]/[action]")]
        public async Task<ActionResult> GetSellerNonAnsweredQuestions(int? sellerId)
        {

            var response = await _service.GetSellerNonAnsweredQuestions(sellerId);
            return this.ResponseStatusWithData(response);
        }
        [HttpGet]
        [Route("/[controller]/[action]")]
        public async Task<ActionResult> GetSellerAnsweredQuestions(int? sellerId)
        {

            var response = await _service.GetSellerAnsweredQuestions(sellerId);
            return this.ResponseStatusWithData(response);
        }
    }
}
