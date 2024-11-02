namespace Trendimaa.DTO.Listing
{
    public class ListingDTO<Entity>
    {
        public int Count { get; set; }
        public List<Entity> List { get; set; }
    }
}
