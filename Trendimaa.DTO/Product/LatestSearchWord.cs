using Trendeimaa.Entities.Interface;

namespace Trendimaa.DTO.Product
{
    public class LatestSearchWord:BaseEntity
    {
        public string? Word { get; set; }
        public int? CategoryId { get; set; }
        public int? SubCategoryId { get; set; }
        public int? SubSubCategoryId { get; set; }
        public int? ProductId { get; set; }
        public int? AppUserId { get; set; }
    }
}
