namespace Trendimaa.DTO
{
    public class NotificationDTO
    {
       
        public string Title { get; set; }
        public string Message { get; set; }
        public int? ImageId { get; set; }
        public ImageDTO? Image { get; set; }
    }
}
