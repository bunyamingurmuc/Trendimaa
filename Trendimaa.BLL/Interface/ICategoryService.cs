using Trendeimaa.Entities;
using Trendimaa.Common;
using Trendimaa.Common.Enum;
using Trendimaa.DTO.CategoryDTOs;
using Trendimaa.DTO.Listing;

namespace Trendimaa.BLL.Interface
{
    public interface ICategoryService:IService<Category>
    {
        //public Task<IResponse<CSellerProductsDto>> GetSellerProductListsWithCount(int sellerId, int page, int quantity, string? word, int? catId, int? subCatId, int? subSubCatId);
       public Task<IResponse<List<CategoryHomeDTO>>> GetMainHomeCategories(Language language);
       public Task<IResponse<List<CategoryHomeDTO>>> GetCategories(Language language);
    }
}
