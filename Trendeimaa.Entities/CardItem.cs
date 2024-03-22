using Trendeimaa.Entities.Interface;

namespace Trendeimaa.Entities
{
    public class CardItem:BaseEntity
    {
        public int Quantity { get; set; }
        public double? Amount { get; set; }
        public int? CardId { get; set; }
        public Card? Card { get; set; }
        public int? ProductId { get; set; }
        public Product? Product { get; set; }

    }
}
