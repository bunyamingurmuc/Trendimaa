using Trendeimaa.Entities;

namespace Trendimaa.DTO.Product
{
    public class SearchParamtersDTO
    {
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }
        public double? MinRate { get; set; }
        public List<string>? Brands{ get; set; }
        public int? SubSubCategoryId { get; set; }
        public int? SubCategoryId { get; set; }
        public int? CategoryId { get; set; }
        public List<Specification>? Specifications { get; set; }
        public List<Variety>? Varieties { get; set; }

    }
}
