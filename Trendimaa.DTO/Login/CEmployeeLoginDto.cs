namespace Trendimaa.DTO
{
    public class CEmployeeLoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Role { get; set; }
        public int? Id { get; set; }
        public bool? IsMarket { get; set; }
        public int? SellerId { get; set; }
    }
}
