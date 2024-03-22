using Microsoft.AspNetCore.Mvc;
using Trendimaa.API.Extension;
using Trendimaa.BLL.Abstract;
using Trendimaa.BLL.Interface;
using Trendimaa.Common.Enum;

namespace Trendimaa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainHomeController : ControllerBase
    {
        public readonly IProductService _productService;
        public readonly ICampaignService _campaignService;

        public MainHomeController(IProductService productService, ICampaignService campaignService)
        {
            _productService = productService;
            _campaignService = campaignService;
        }
        [HttpGet]
        [Route("/[controller]/[action]")]
        public async Task<ActionResult> GetHomeRelatedProducts(int userId)
        {
            var response = await _productService.GetHomeRelatedProducts(userId);
            return this.ResponseStatusWithData(response);
        }

        [HttpGet]
        [Route("/[controller]/[action]")]
        public async Task<ActionResult> GetLastLookedProducts(int userId)
        {
            var response = await _productService.GetLastLookedProducts(userId);
            return this.ResponseStatusWithData(response);
        }
        
        [HttpGet]
        [Route("/[controller]/[action]")]
        public async Task<ActionResult> GetAroundMostLooked(double latitude, double longitude)
        {
            var response = await _productService.GetAroundMostLooked(longitude,latitude);
            return this.ResponseStatusWithData(response);
        }

        [HttpGet]
        [Route("/[controller]/[action]")]
        public async Task<ActionResult> GetMainHomeCampaigns(Language language)
        {
            var response = await _campaignService.GetMainHomeCampaigns(language);
            return this.ResponseStatusWithData(response);
        }

    }
}
