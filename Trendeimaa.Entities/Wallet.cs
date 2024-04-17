using Trendeimaa.Entities.Interface;

namespace Trendeimaa.Entities
{
    public class Wallet:BaseEntity
    {
        public Wallet()
        {
            WalletItems= new List<WalletItem>();
        }

        public double Amount { get; set; }
        public int? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public List<WalletItem> WalletItems{ get; set; }

    }
}
