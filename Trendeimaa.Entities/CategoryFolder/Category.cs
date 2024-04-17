using Trendeimaa.Entities.CategoryFolder;
using Trendeimaa.Entities.Interface;
using Trendimaa.Common.Enum;

namespace Trendeimaa.Entities
{
    public class Category:BaseEntity
    {
        public Category()
        {
            Products= new List<Product>();
            SearchRelateds = new List<SearchRelated>();
            SubCategories = new List<SubCategory>();
            Images = new List<Image>();
        }
        public string Name { get; set; }
        public Language Language{ get; set; }

        public List<Product>Products{ get; set; }
        public List<SearchRelated> SearchRelateds { get; set; }
        public List<SubCategory> SubCategories{ get; set; }
        public List<Image> Images{ get; set; }

    }
}
