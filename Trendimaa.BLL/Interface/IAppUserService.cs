using Orde.BLL.Extension.Token;
using Trendeimaa.Entities;
using Trendimaa.Common;
using Trendimaa.DTO;

namespace Trendimaa.BLL.Interface
{
    public interface IAppUserService: IService<AppUser>
    {
        Task<IResponse<AppUser>> SignUp(AppUser appUser);
        Task<IResponse<JwtTokenResponse>> Login(CLoginDto dto);
    }
}
