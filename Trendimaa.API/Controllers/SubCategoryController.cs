using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trendeimaa.Entities;
using Trendeimaa.Entities.CategoryFolder;
using Trendimaa.API.Extension;
using Trendimaa.BLL.Interface;
using Trendimaa.Common.Enum;

namespace Trendimaa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {
        public readonly ISubCategoryService _service;

        public SubCategoryController(ISubCategoryService service)
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
        public async Task<ActionResult> CreateAsy(SubCategory entity)
        {
            var response = await _service.CreateAsync(entity);
            return this.ResponseStatusWithData(response);

        }

        [HttpPut]
        [Route("/[controller]/[action]")]
        public async Task<ActionResult> UpdateAsy(SubCategory entity)
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
        public async Task<ActionResult> CreateRangeAsync(List<SubCategory> subCategories)
        {

            var response = await _service.CreateRangeAsync(subCategories);
            return this.ResponseStatusWithData(response);
        }  [HttpPost]
        [Route("/[controller]/[action]")]
        public async Task<ActionResult> GetSubCategories(Language language, int catId)
        {

            var response = await _service.GetSubCategories(language,catId);
            return this.ResponseStatusWithData(response);
        }
    }
}
