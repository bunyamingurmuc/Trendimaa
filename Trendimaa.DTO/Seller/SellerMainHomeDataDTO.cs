namespace Trendimaa.DTO.Seller
{
    public class SellerMainHomeDataDTO
    {
        public int OutofStockCount{ get; set; }
        public int PendingReturns{ get; set; }
        public int DelayedOrders{ get; set; }
        public int PendingOrders{ get; set; }
        public double DailySalesAmount { get; set; }
        public double WeeklySalesAmount { get; set; }
        public double MountlySalesAmount { get; set; }
        public double RateWeeklySalesAmount { get; set; }
        public double RateMountlySalesAmount { get; set; }


    }
}
