using Trendeimaa.Entities.Interface;

namespace Trendeimaa.Entities
{
    public class UserKey:BaseEntity
    {
        public int? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public int? SellerId { get; set; }
        public Seller? Seller{ get; set; }
        public string Key { get; set; }
    }
}
