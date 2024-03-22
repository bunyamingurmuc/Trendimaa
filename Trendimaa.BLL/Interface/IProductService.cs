
using Trendeimaa.Entities;
using Trendimaa.Common;
using Trendimaa.DTO;
using Trendimaa.DTO.Product;
using Trendimaa.DTO.SearchDTOs;

namespace Trendimaa.BLL.Interface
{
    public interface IProductService : IService<Product>
    {
        public Task<IResponse<List<HomeProductCardDto>>> GetHomeRelatedProducts(int userId);
        public Task<IResponse<List<HomeProductCardDto>>> GetLastLookedProducts(int userId);
        public Task<IResponse<List<HomeProductCardDto>>> GetAroundMostLooked(double latitude, double longitude);
        public Task<IResponse<List<CompareProductDTO>>> CompareProducts(List<int> productIds);
        public Task<IResponse<List<BasicProductCardDTO>>> GetLatestSearchProducts(int userId);
        public Task<IResponse<List<LatestSearchWord>>> GetLastestSearchedWords(int userId);

        public Task<IResponse<List<SearchCategoryDTO>>> GetSearchCategoryList(string word);
        public Task<IResponse<List<SearchSubCategoryDTO>>> GetSearchSubCategoryList(string word);
        public Task<IResponse<List<SearchSubSubCategoryDTO>>> GetSearchSubSubCategoryList(string word);
        public Task<IResponse<List<SearchProductDTO>>> GetSearchProductList(string word);
        public Task<IResponse<List<SearchProductDTO>>> GetSearchProductResult(string word, int? catId, int? subCatId, int? subsubCatId);
        public Task<IResponse<List<BasicProductCardDTO>>> ChangeSearchProductResult(ChangeSearchListDTO dto);
    }
}
