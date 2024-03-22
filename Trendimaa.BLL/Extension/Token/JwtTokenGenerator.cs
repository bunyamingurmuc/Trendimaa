using Microsoft.IdentityModel.Tokens;
using Orde.BLL.Extension.Token;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Trendimaa.Common;
using Trendimaa.DTO;

namespace Trendimaa.BLL.Extension.Token
{
    public class JwtTokenGenerator
    {
        public static JwtTokenResponse GenerateToken(CLoginDto dto)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, dto.Email));
            claims.Add(new Claim(ClaimTypes.Role, dto.Role));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, dto.Id.Value.ToString()));
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenSettings.Key));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = new JwtSecurityToken(
                claims: claims,
                issuer: JwtTokenSettings.Issuer,
                audience: JwtTokenSettings.Audience,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddDays(JwtTokenSettings.Expire),
                signingCredentials: credentials
                );
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            return new JwtTokenResponse(handler.WriteToken(token));
        }
        public static JwtTokenResponse GenerateTokenDev(CDeveloperLoginDto dto)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, dto.Username));
            claims.Add(new Claim(ClaimTypes.Role, dto.Role));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, dto.Id.Value.ToString()));
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenSettings.Key));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = new JwtSecurityToken(
                claims: claims,
                issuer: JwtTokenSettings.Issuer,
                audience: JwtTokenSettings.Audience,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: credentials
            );
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            return new JwtTokenResponse(handler.WriteToken(token));
        }
        public static JwtTokenResponse GenerateTokenAdmin(AdminDTO dto)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Email, dto.Email));
            claims.Add(new Claim(ClaimTypes.Role, dto.Role));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, dto.Id.Value.ToString()));
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenSettings.Key));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = new JwtSecurityToken(
                claims: claims,
                issuer: JwtTokenSettings.Issuer,
                audience: JwtTokenSettings.Audience,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddDays(JwtTokenSettings.Expire),
                signingCredentials: credentials
                );
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            return new JwtTokenResponse(handler.WriteToken(token));
        }

        public static JwtTokenResponse GenerateTokenSeller(CSellerLoginDto dto)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, dto.Email));
            claims.Add(new Claim(ClaimTypes.Role, dto.Role));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, dto.Id.Value.ToString()));
            claims.Add(new Claim("Language", dto.Language.ToString()));
            claims.Add(new Claim("Currency", dto.Currency.ToString()));

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenSettings.Key));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = new JwtSecurityToken(
                claims: claims,
                issuer: JwtTokenSettings.Issuer,
                audience: JwtTokenSettings.Audience,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddDays(JwtTokenSettings.Expire),
                signingCredentials: credentials
            );
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            return new JwtTokenResponse(handler.WriteToken(token));
        }

        public static JwtTokenResponse GenerateTokenEmployee(CEmployeeLoginDto dto)

        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, dto.Email));
            claims.Add(new Claim(ClaimTypes.Role, dto.Role));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, dto.Id.Value.ToString()));
            claims.Add(new Claim("sellerId", dto.SellerId.ToString()));
            claims.Add(new Claim("isMarket", dto.IsMarket.ToString()));

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenSettings.Key));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = new JwtSecurityToken(
                claims: claims,
                issuer: JwtTokenSettings.Issuer,
                audience: JwtTokenSettings.Audience,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddDays(JwtTokenSettings.Expire),
                signingCredentials: credentials
            );
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            return new JwtTokenResponse(handler.WriteToken(token));
        }
    }
}
