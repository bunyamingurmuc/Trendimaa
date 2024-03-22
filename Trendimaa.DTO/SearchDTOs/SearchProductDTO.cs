using Trendimaa.DTO.Seller;

namespace Trendimaa.DTO.SearchDTOs
{
    public class SearchProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int? CategoryId { get; set; }
        public SearchCategoryDTO? Category { get; set; }
        public int? SubCategoryId { get; set; }
        public SearchSubCategoryDTO? SubCategory { get; set; }
        public int? SubSubCategoryId { get; set; }
        public SearchSubSubCategoryDTO? SubSubCategory { get; set; }
        public int? SellerId { get; set; }
        public SellerCardDTO? Seller{ get; set; }

    }
}
