using Trendeimaa.Entities.Interface;
using Trendimaa.Common.Enum;

namespace Trendimaa.DTO.CategoryDTOs
{
    public class CategoryHomeDTO:BaseEntity
    {
        public string Name { get; set; }
        public Language Language { get; set; }
    }
}
