using Trendeimaa.Entities.Interface;

namespace Trendeimaa.Entities
{
    public class AlarmItem:BaseEntity
    {
        public double AlarmPrice { get; set; }
        public int? ProductId{ get; set; }
        public Product? Product{ get; set; }
        public int? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }

    }
}
