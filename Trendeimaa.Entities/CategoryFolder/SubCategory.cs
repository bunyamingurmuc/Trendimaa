using Trendeimaa.Entities.Interface;
using Trendimaa.Common.Enum;

namespace Trendeimaa.Entities.CategoryFolder
{
    public class SubCategory : BaseEntity
    {

        public SubCategory()
        {
            Products = new List<Product>();
            SearchRelateds = new List<SearchRelated>();
            SubSubCategories = new List<SubSubCategory>();
            Specifications=  new List<Specification>();
            Varieties = new List<Variety>();
        }

        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
        public string Name { get; set; }
        public Language Language { get; set; }

        public List<Product> Products { get; set; }
        public List<SearchRelated> SearchRelateds { get; set; }
        public List<SubSubCategory> SubSubCategories { get; set; }
        public List<Specification> Specifications{ get; set; }
        public List<Variety> Varieties{ get; set; }
    }
}
