
using Trendeimaa.Entities;
using Trendimaa.Common;
using Trendimaa.Common.Enum;
using Trendimaa.DTO;
using Trendimaa.DTO.Listing;
using Trendimaa.DTO.Product;
using Trendimaa.DTO.SearchDTOs;

namespace Trendimaa.BLL.Interface
{
    public interface IProductService : IService<Product>
    {
        public Task<IResponse<Product>> UpdateProduct(Product product);

        public Task<IResponse<List<HomeProductCardDto>>> GetHomeRelatedProducts(int userId);
        public Task<IResponse<List<HomeProductCardDto>>> GetLastLookedProducts(int userId);
        public Task<IResponse<List<HomeProductCardDto>>> GetAroundMostLooked(double latitude, double longitude);
        public Task<IResponse<List<CompareProductDTO>>> CompareProducts(List<int> productIds);
        public Task<IResponse<List<BasicProductCardDTO>>> GetLatestSearchProducts(int userId);
        public Task<IResponse<List<LatestSearchWord>>> GetLastestSearchedWords(int userId);
        Task<IResponse<List<BasicProductCardDTO>>> GetMainHomeProducts();
        public Task<byte[]> GenerateExcel(int? subcategoryId, int? subSubcategoryId, Language language);
        public Task<List<Product>> ImportExcel(int? subcategoryId, int? subSubcategoryId, Language language, byte[] excelData);


        public Task<IResponse<List<SearchCategoryDTO>>> GetSearchCategoryList(string word);
        public Task<IResponse<List<SearchCategoryDTO>>> GetSearchSubCategoryList(string word);
        public Task<IResponse<List<SearchCategoryDTO>>> GetSearchSubSubCategoryList(string word);
        public Task<IResponse<List<SearchProductDTO>>> GetSearchProductList(string word);
        public Task<IResponse<List<SearchProductDTO>>> GetSearchProductResult(string word, int? catId, int? subCatId, int? subsubCatId);
        public Task<IResponse<List<BasicProductCardDTO>>> ChangeSearchProductResult(ChangeSearchListDTO dto);
        public Task<IResponse<List<SearchProductDTO>>> GetSearchProductVarietiesResult(SearchParamtersDTO dto,string word,int count,int page);

        public Task<IResponse<CSellerProductsDto>> GetSellerProductListsWithCount(int sellerId, int page, int quantity, string? word, int? catId, int? subCatId, int? subSubCatId);
        public Task<IResponse<ProductDetailDTO>> GetProductDetail(int productId);
        public Task<IResponse<List<BasicProductCardDTO>>> GetSameProducts(int productId);
        public Task<IResponse<List<BasicProductCardDTO>>> GetCategoriesProduducts(int? cateid, int? subcateid, int? subsubcateid);
        public Task<IResponse<List<ProductSellerDTO>>> GetOtherProductSellers(int? productId);
        public Task<IResponse<List<BasicProductCardDTO>>> GetSellerStockProducts(int? sellerId);
        public Task<IResponse<List<BasicProductCardDTO>>> GetSellerNotStockProducts(int? sellerId);
        public Task<IResponse<List<BasicProductCardDTO>>> GetSellerNotConfirmedProducts(int? sellerId);
        public Task<IResponse<BasicProductCardDTO>> GetProductWithBarcode(string StockCode);
        public Task<IResponse> ApplyDiscount(List<int> productIds, int percent, double price);
        public Task Free();
    }
}
