namespace EMY.Restaurant.Core.Domain.ViewModels
{
    public class ReservationStatsViewModel
    {
        public int TodayReservations { get; set; }
        public int TodayReservationPeople { get; set; }
        public int YesterdayReservations { get; set; }
        public int YesterdayReservationPeople { get; set; }
    }
}
