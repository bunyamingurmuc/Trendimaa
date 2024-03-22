using Trendeimaa.Entities.Interface;

namespace Trendeimaa.Entities.CategoryFolder
{
    public class SubSubCategory : BaseEntity
    {
        public SubSubCategory()
        {
            Products = new List<Product>();
            SearchRelateds = new List<SearchRelated>();
            Specifications = new List<Specification>();
        }

        public int? SubCategoryId { get; set; }
        public SubCategory? SubCategory { get; set; }
        public string Name { get; set; }
        public List<Product> Products { get; set; }
        public List<SearchRelated> SearchRelateds { get; set; }
        public List<Specification> Specifications { get; set; }
    }
}
