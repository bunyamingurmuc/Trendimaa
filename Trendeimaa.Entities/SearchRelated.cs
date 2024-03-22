using Trendeimaa.Entities.CategoryFolder;
using Trendeimaa.Entities.Interface;

namespace Trendeimaa.Entities
{
    public class SearchRelated:BaseEntity
    {
        public string Word { get; set; }
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
        public int? SubCategoryId { get; set; }
        public SubCategory? SubCategory { get; set; }
        public int? SubSubCategoryId { get; set; }
        public SubSubCategory? SubSubCategory { get; set; }
        public int? ProductId { get; set; }
        public Product? Product { get; set; }
        public int? AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public double Latitude{ get; set; }
        public double Longitude { get; set; }
    }
}
