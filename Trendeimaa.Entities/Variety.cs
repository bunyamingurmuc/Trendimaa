using Trendeimaa.Entities.CategoryFolder;
using Trendeimaa.Entities.Interface;
using Trendimaa.Common.Enum;

namespace Trendeimaa.Entities
{
    public class Variety:BaseEntity
    {
        public VarietyName VarietyName { get; set; }
        public string Key { get; set; }
        public string Description { get; set; }
        public Language Language{ get; set; }
        public bool IsDefault{ get; set; }
        public bool IsRequired{ get; set; }

      
        public Product? Product { get; set; }
        public int? ProductId { get; set; }
        public int? SubSubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; }
        public int? SubCategoryId { get; set; }
        public SubSubCategory SubSubCategory { get; set; }


    }
}
