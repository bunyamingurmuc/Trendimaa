using Trendeimaa.Entities;

namespace Trendimaa.DTO.SearchDTOs
{
    public class ChangeSearchListDTO
    {
        public List<string>? Brands { get; set; }
        public int? MaxPrice { get; set; }
        public int? MinPrice { get; set; }
        public List<int>? CategoryIds{ get; set; }
        public List<int>? SubCategoryIds{ get; set; }
        public List<int>? SubSubCategoryIds{ get; set; }
        public List<int>? ProductIds { get; set; }
        public List<Specification>? Specifications { get; set; }
        public List<Variety>? Varieties { get; set; }

        //List<int> productIds, List<Specification>? specifications, List<Variety>? varieties
    }
}
