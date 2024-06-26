using Trendeimaa.Entities.Interface;

namespace Trendeimaa.Entities
{
    public class CreditCard:BaseEntity
    {
        public string Title { get; set; }
        public int FirstDigits { get; set; }
        public int LastDigits { get; set; }
        public string Name { get; set; }
        public int? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }

    }
}
