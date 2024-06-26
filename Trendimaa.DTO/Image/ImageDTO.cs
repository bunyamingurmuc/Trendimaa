using Trendeimaa.Entities;
using Trendeimaa.Entities.Interface;

namespace Trendimaa.DTO
{
    public class ImageDTO:BaseEntity
    {
        public string? Path { get; set; }
        public int? ProductId { get; set; }
        public int? CommentId { get; set; }
        public int? CampaignId { get; set; }
    }
}
