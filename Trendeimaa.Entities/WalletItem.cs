using Trendeimaa.Entities.Interface;

namespace Trendeimaa.Entities
{
    public class WalletItem:BaseEntity
    {
        public int? WalletId { get; set; }
        public Wallet? Wallet { get; set; }
        public double WalletItemAmount { get; set; }
        public bool IsPlus { get; set; }
        public int? OrderId{ get; set; }
        public Order? Order{ get; set; }
    }
}
