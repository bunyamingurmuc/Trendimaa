using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trendimaa.API.Extension;
using Trendimaa.BLL.Interface;
using Trendimaa.DTO.SearchDTOs;

namespace Trendimaa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        public readonly IProductService _service;
        public readonly IVarietyService _varietyService;
        public readonly ISpecificationService _specificationService;

        public SearchController(IProductService service, IVarietyService varietyService, ISpecificationService specificationService)
        {
            _service = service;
            _varietyService = varietyService;
            _specificationService = specificationService;
        }
        [HttpGet]
        [Route("/[controller]/[action]")]
        public async Task<ActionResult> GetLatestSearchProducts(int userId)
        {

            var response = await _service.GetLatestSearchProducts(userId);
            return this.ResponseStatusWithData(response);
        }
        [HttpGet]
        [Route("/[controller]/[action]")]
        public async Task<ActionResult> GetLastestSearchedWords(int userId)
        {

            var response = await _service.GetLastestSearchedWords(userId);
            return this.ResponseStatusWithData(response);
        }
        [HttpGet]
        [Route("/[controller]/[action]")]
        public async Task<ActionResult> GetSearchCategoryList(string word)
        {

            var response = await _service.GetSearchCategoryList(word);
            return this.ResponseStatusWithData(response);
        }
        [HttpGet]
        [Route("/[controller]/[action]")]
        public async Task<ActionResult> GetSearchSubCategoryList(string word)
        {

            var response = await _service.GetSearchSubCategoryList(word);
            return this.ResponseStatusWithData(response);
        } 
        [HttpGet]
        [Route("/[controller]/[action]")]
        public async Task<ActionResult> GetSearchSubSubCategoryList(string word)
        {

            var response = await _service.GetSearchSubSubCategoryList(word);
            return this.ResponseStatusWithData(response);
        }
        [HttpGet]
        [Route("/[controller]/[action]")]
        public async Task<ActionResult> GetSearchProductList(string word)
        {

            var response = await _service.GetSearchProductList(word);
            return this.ResponseStatusWithData(response);
        }
         [HttpGet]
        [Route("/[controller]/[action]")]
        public async Task<ActionResult> GetSearchProductResult(string word, int? catId, int? subCatId, int? subsubCatId)
        {

            var response = await _service.GetSearchProductResult(word,catId,subCatId,subsubCatId);
            return this.ResponseStatusWithData(response);
        }
        [HttpGet]
        [Route("/[controller]/[action]")]
        public async Task<ActionResult> ChangeSearchProductResult(ChangeSearchListDTO dto)
        {

            var response = await _service.ChangeSearchProductResult(dto);
            return this.ResponseStatusWithData(response);
        } 
        [HttpGet]
        [Route("/[controller]/[action]")]
        public async Task<ActionResult> GetSpecificationsForSearch(int? subcatid, int? subsubcatid)
        {

            var response = await _specificationService.GetSpecificationsForSearch(subcatid, subsubcatid);
            return this.ResponseStatusWithData(response);

        }


    }
}
