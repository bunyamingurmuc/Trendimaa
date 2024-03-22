using Trendeimaa.Entities.Interface;

namespace Trendeimaa.Entities.CategoryFolder
{
    public class SubCategory : BaseEntity
    {

        public SubCategory()
        {
            Products = new List<Product>();
            SearchRelateds = new List<SearchRelated>();
            SubSubCategories = new List<SubSubCategory>();
        }

        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
        public string Name { get; set; }
        public List<Product> Products { get; set; }
        public List<SearchRelated> SearchRelateds { get; set; }
        public List<SubSubCategory> SubSubCategories { get; set; }
    }
}
