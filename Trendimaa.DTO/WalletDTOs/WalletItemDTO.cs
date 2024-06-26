using Trendeimaa.Entities.Interface;

namespace Trendimaa.DTO.WalletDTOs
{
    public class WalletItemDTO:BaseEntity
    {
        public int? WalletId { get; set; }
        public WalletDTO? Wallet { get; set; }
        public double WalletItemAmount { get; set; }
        public bool IsPlus { get; set; }
        public int? OrderId { get; set; }
    }
}
