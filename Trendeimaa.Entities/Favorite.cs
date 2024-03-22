using Trendeimaa.Entities.Interface;
using Trendeimaa.Entities.Related;

namespace Trendeimaa.Entities
{
    public class Favorite:BaseEntity
    {
        public int? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public Favorite()
        {
            favoriteProducts = new List<FavoriteProduct>();
        }
        public List<FavoriteProduct> favoriteProducts{ get; set; }
    }
}
