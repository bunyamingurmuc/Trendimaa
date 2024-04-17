using Trendeimaa.Entities.Interface;
using Trendimaa.Common.Enum;

namespace Trendeimaa.Entities.CategoryFolder
{
    public class SubSubCategory : BaseEntity
    {
        public SubSubCategory()
        {
            Products = new List<Product>();
            SearchRelateds = new List<SearchRelated>();
            Specifications = new List<Specification>();
            Varieties = new List<Variety> ();
        }

        public int? SubCategoryId { get; set; }
        public SubCategory? SubCategory { get; set; }
        public string Name { get; set; }
        public Language Language { get; set; }
        public List<Product> Products { get; set; }
        public List<SearchRelated> SearchRelateds { get; set; }
        public List<Specification> Specifications { get; set; }
        public List<Variety> Varieties { get; set; }
    }
}
