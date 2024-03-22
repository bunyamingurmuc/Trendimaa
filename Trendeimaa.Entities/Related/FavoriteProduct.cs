namespace Trendeimaa.Entities.Related
{
    public class FavoriteProduct
    {
        public int? ProductId { get; set; }
        public Product? Product { get; set; }
        public int? FavoriteId { get; set; }
        public Favorite? Favorite { get; set; }
    }
}
