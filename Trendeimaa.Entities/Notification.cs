using Trendeimaa.Entities.Interface;

namespace Trendeimaa.Entities
{
    public class Notification:BaseEntity
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public int? ImageId { get; set; }
        public Image? Image { get; set; }
    }
}
