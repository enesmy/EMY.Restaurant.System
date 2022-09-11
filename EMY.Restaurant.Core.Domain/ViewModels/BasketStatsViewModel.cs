namespace EMY.Restaurant.Core.Domain.ViewModels
{
    public class BasketStatsViewModel
    {
        public int TodayBaskets { get; set; }
        public decimal TodayBasketPrice { get; set; }

        public int YesterdayBaskets { get; set; }
        public decimal YesterdayBasketPrice { get; set; }
    }
}
