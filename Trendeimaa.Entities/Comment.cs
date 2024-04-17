using Trendeimaa.Entities.Interface;

namespace Trendeimaa.Entities
{
    public class Comment:BaseEntity
    {
        public Comment()
        {
            Images = new List<Image>();   
        }
        public int Rate{ get; set; }
        public string Description { get; set; }
        public string NameLatter { get; set; }
        public string SurnameLatter { get; set; }
        public int? LikeCount{ get; set; }
        public int? DisLikeCount{ get; set; }


        public int? ProductId { get; set; }
        public Product? Product { get; set; }
        public int? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }

       
      
        public List<Image> Images { get; set; }
    }
}
