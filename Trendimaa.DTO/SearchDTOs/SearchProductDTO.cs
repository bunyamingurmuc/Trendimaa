using Trendeimaa.Entities.Interface;
using Trendimaa.DTO.Seller;

namespace Trendimaa.DTO.SearchDTOs
{
    public class SearchProductDTO:BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int? CategoryId { get; set; }
        public SearchCategoryDTO? Category { get; set; }
        public int? SubCategoryId { get; set; }
        public SearchCategoryDTO? SubCategory { get; set; }
        public int? SubSubCategoryId { get; set; }
        public SearchCategoryDTO? SubSubCategory { get; set; }
        public int? SellerId { get; set; }
        public SellerCardDTO? Seller{ get; set; }
        public List<ImageDTO>Images{ get; set; }

    }
}
