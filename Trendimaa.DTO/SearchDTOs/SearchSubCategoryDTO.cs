namespace Trendimaa.DTO.SearchDTOs
{
    public class SearchSubCategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? CategoryId { get; set; }
        public SearchCategoryDTO? Category { get; set; }
    }
}
