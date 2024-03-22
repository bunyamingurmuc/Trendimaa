using Trendeimaa.Entities.Interface;
using Trendimaa.Common.Enum;

namespace Trendeimaa.Entities
{
    public class Variety:BaseEntity
    {
        public VarietyName VarietyName { get; set; }
        public string Key { get; set; }
        public string Description { get; set; }
        public Language Language{ get; set; }

      
        public Product? Product { get; set; }
        public int? ProductId { get; set; }
        
    }
}
