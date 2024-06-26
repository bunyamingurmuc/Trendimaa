using Microsoft.AspNetCore.Mvc;
using Trendeimaa.Entities;
using Trendimaa.API.Extension;
using Trendimaa.BLL.Interface;

namespace Trendimaa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public readonly IProductService _service;

        public ProductController(IProductService service)
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
        public async Task<ActionResult> CreateAsy(Product entity)
        {
            var response = await _service.CreateAsync(entity);
            return this.ResponseStatusWithData(response);

        }

        [HttpPut]
        [Route("/[controller]/[action]")]
        public async Task<ActionResult> UpdateAsy(Product entity)
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
        public async Task<ActionResult> CreateRangeAsync(List<Product> products)
        {

            var response = await _service.CreateRangeAsync(products);
            return this.ResponseStatusWithData(response);
        }
        [HttpGet]
        [Route("/[controller]/[action]")]
        public async Task<ActionResult> CompareProducts(List<int> productIds)
        {

            var response = await _service.CompareProducts(productIds);
            return this.ResponseStatusWithData(response);
        }
        [HttpGet]
        [Route("/[controller]/[action]")]
        public async Task<ActionResult> GetProductDetail(int productId)
        {

            var response = await _service.GetProductDetail(productId);
            return this.ResponseStatusWithData(response);
        }

        [HttpGet]
        [Route("/[controller]/[action]")]
        public async Task<ActionResult> GetSellerProductListsWithCount(int sellerId, int page, int quantity, string? word, int? catId, int? subCatId, int? subSubCatId)
        {

            var response = await _service.GetSellerProductListsWithCount(sellerId,page,quantity,word,catId,subCatId,subSubCatId);
            return this.ResponseStatusWithData(response);
        }  
        [HttpGet]
        [Route("/[controller]/[action]")]
        public async Task<ActionResult> GetSameProducts(int productId)
        {

            var response = await _service.GetSameProducts(productId);
            return this.ResponseStatusWithData(response);
        } 
        [HttpGet]
        [Route("/[controller]/[action]")]
        public async Task<ActionResult> GetCategoriesProducts(int? cateid, int? subcateid, int? subsubcateid)
        {

            var response = await _service.GetCategoriesProduducts(cateid,subcateid,subsubcateid);
            return this.ResponseStatusWithData(response);
        }
        [HttpGet]
        [Route("/[controller]/[action]")]
        public async Task<ActionResult> GetOtherProductSellers(int? productId)
        {

            var response = await _service.GetOtherProductSellers(productId);
            return this.ResponseStatusWithData(response);
        }
         [HttpGet]
        [Route("/[controller]/[action]")]
        public async Task<ActionResult> GetSellerStockProducts(int? sellerId)
        {

            var response = await _service.GetSellerStockProducts(sellerId);
            return this.ResponseStatusWithData(response);
        }
         [HttpGet]
        [Route("/[controller]/[action]")]
        public async Task<ActionResult> GetSellerNotStockProducts(int? sellerId)
        {

            var response = await _service.GetSellerNotStockProducts(sellerId);
            return this.ResponseStatusWithData(response);
        }

          [HttpGet]
        [Route("/[controller]/[action]")]
        public async Task<ActionResult> GetSellerNotConfirmedProducts(int? sellerId)
        {

            var response = await _service.GetSellerNotConfirmedProducts(sellerId);
            return this.ResponseStatusWithData(response);
        }
           [HttpGet]
        [Route("/[controller]/[action]")]
        public async Task<ActionResult> GetProductWithBarcode(string StockCode)
        {

            var response = await _service.GetProductWithBarcode(StockCode);
            return this.ResponseStatusWithData(response);
        }   
        [HttpGet]
        [Route("/[controller]/[action]")]
        public async Task<ActionResult> ApplyDiscount(List<int> productIds, int percent, double price)
        {

           await _service.ApplyDiscount(productIds,percent,price);
            return Ok();
        }
        [HttpGet]
        [Route("/[controller]/[action]")]
        public async Task<ActionResult> Free()
        {

           await _service.Free();
            return Ok();
        }


        //[HttpGet]
        //[Route("/[controller]/[action]")]
        //public async Task<ActionResult> CompareProducts(int? subcategoryId, int? subSubcategoryId, Language language)
        //{
        //   byte[] excelData= await _service.GenerateExcel(subcategoryId, subSubcategoryId, language);
        //    if (excelData!=null)
        //    {
        //        return File(excelData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Product_Template.xlsx");
        //    }
        //    else
        //    {
        //        return NotFound();
        //    }

        //}

    }
}
