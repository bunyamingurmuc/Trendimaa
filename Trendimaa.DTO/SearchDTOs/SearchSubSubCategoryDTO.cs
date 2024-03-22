using Trendeimaa.Entities;

namespace Trendimaa.DTO.SearchDTOs
{
    public class SearchSubSubCategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? CategoryId { get; set; }
        public SearchSubCategoryDTO? Category { get; set; }

    }
}
