using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Trendeimaa.Entities;
using Trendimaa.API.Extension;
using Trendimaa.BLL.Interface;

namespace Trendimaa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        public readonly IAnswerService _service;

        public AnswerController(IAnswerService service)
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
        public async Task<ActionResult> CreateAsy(Answer entity)
        {
            var response = await _service.CreateAsync(entity);
            return this.ResponseStatusWithData(response);

        }

        [HttpPut]
        [Route("/[controller]/[action]")]
        public async Task<ActionResult> UpdateAsy(Answer entity)
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
        public async Task<ActionResult> CreateRangeAsync(List<Answer> list)
        {

            var response = await _service.CreateRangeAsync(list);
            return this.ResponseStatusWithData(response);
        }
    }   
}
