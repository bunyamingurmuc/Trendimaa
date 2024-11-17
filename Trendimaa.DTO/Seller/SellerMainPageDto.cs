namespace Trendimaa.DTO.Seller
{
    public class SellerMainPageDto
    {
        public int TotalQuestionCount { get; set; }
        public int NonAnsweredQuestionCount { get; set; }
        public int TotalProductCount{ get; set; }
        public int TotalOrderCount{ get; set; }
        public int DelayedOrderCount{ get; set; }
        public int ReturnedOrderCount { get; set; }
        public int SendedOrderCount{ get; set; }
        public int TotalPaymentCount{ get; set; }
        public int WillPaymentCount{ get; set; }
        public int MontlyOrderCount{ get; set; }
        public int MontlyCanceledOrderCount{ get; set; }
        public int SettedAlarmProductCount{ get; set; }
    }
}
