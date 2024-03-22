using Trendimaa.Common.Enum;

namespace Trendimaa.DTO
{
    public class CSellerLoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Role { get; set; }
        public int? Id { get; set; }
        public Currency? Currency { get; set; }
        public Language? Language { get; set; }
    }
}
