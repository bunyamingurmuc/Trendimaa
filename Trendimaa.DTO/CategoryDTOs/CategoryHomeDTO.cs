using Trendeimaa.Entities.Interface;
using Trendimaa.Common.Enum;

namespace Trendimaa.DTO.CategoryDTOs
{
    public class CategoryHomeDTO:BaseEntity
    {
        public CategoryHomeDTO()
        {
            Images = new List<ImageDTO>();
        }
        public string Name { get; set; }
        public List<ImageDTO>Images { get; set; }
        public Language Language { get; set; }
    }
}
