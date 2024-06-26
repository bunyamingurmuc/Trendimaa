using Trendeimaa.Entities.Interface;

namespace Trendimaa.DTO
{
    public class BasicVarietyDTO:BaseEntity
    {
        public string Title { get; set; }
        public string? Key { get; set; }
        public string Description { get; set; }
        public bool? IsRequired { get; set; }

    }
}
