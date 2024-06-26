
using Trendeimaa.Entities;
using Trendimaa.Common;
using Trendimaa.DTO.WalletDTOs;

namespace Trendimaa.BLL.Interface
{
    public interface IWalletService:IService<Wallet>
    {
        Task<Response<WalletDTO>> GeyUserWallet(int userId);
    }
}
