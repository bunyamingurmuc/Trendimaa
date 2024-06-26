using FirebaseAdmin.Messaging;
using Trendimaa.Common;
using Trendimaa.DTO;

namespace Trendimaa.BLL.Interface
{
    public interface INotificationService:IService<Trendeimaa.Entities.Notification>
    {
        public Task<IResponse<List<NotificationDTO>>> GetSellerNotification(int sellerId);
        public Task<IResponse<List<NotificationDTO>>> GetUserNotification(int appuserId);
        public Task<BatchResponse> SendNotification(List<string> registrationTokens, string title, string body);
        public Task<string> SendNotificationSingle(string registrationTokens, string title, string body);
        public Task<BatchResponse> SendNotificationOrderReadyToUser(int orderId);
        public Task<BatchResponse> SendNotificationEmployee(int orderId);
        public Task<BatchResponse> SendNotificationTestSeller(int sellerId, string title, string body);
        public Task<BatchResponse> SendNotificationSeller(int orderId);
    }
}
