using Trendeimaa.Entities.Interface;

namespace Trendimaa.DTO
{
    public class CommentDTO:BaseEntity
    {
        public int Rate { get; set; }
        public string Description { get; set; }
        public string NameLatter { get; set; }
        public string SurnameLatter { get; set; }
        public int? LikeCount { get; set; }
        public int? DisLikeCount { get; set; }


        public int? ProductId { get; set; }    
        public int? AppUserId { get; set; }
        public List<ImageDTO> Images { get; set; }
    }
}
