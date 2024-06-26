using Trendeimaa.Entities.Interface;

namespace Trendimaa.DTO.WalletDTOs
{
    public class WalletDTO:BaseEntity
    {
        public double Amount { get; set; }
        public int? AppUserId { get; set; }
        public List<WalletItemDTO> WalletItems { get; set; }
    }
}
