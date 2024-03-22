using Trendeimaa.Entities.CategoryFolder;
using Trendeimaa.Entities.Interface;

namespace Trendeimaa.Entities
{
    public class Specification:BaseEntity
    {
        public string Title { get; set; }
        public string Key { get; set; }
        public string Description { get; set; }
        public bool? IsDefault { get; set; } = false;
        public Product? Product { get; set; }
        public int? ProductId { get; set; }
        public int? SubSubCategoryId { get; set; }
        public SubSubCategory SubSubCategory { get; set; }

    }
}
