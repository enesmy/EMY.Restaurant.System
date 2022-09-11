using EMY.Restaurant.Core.Application.Repositories.OrderRepositories;
using EMY.Restaurant.Core.Domain.Entities;
using EMY.Restaurant.Core.Domain.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace EMY.Restaurant.Infrastructure.Persistence.Repositories.BasketRepositories
{
    public class OrderReadRepository : ReadRepository<Order>, IOrderReadRepository
    {
        public OrderReadRepository(DbContext context) : base(context)
        {
        }

        public BasketStatsViewModel BasketStats()
        {

            var today = GetWhere(o =>
            o.CreatedAt.Date == DateTime.Today.Date &&
            o.OrderStatus != OrderStatus.Pending &&
            o.OrderStatus != OrderStatus.Canceled
            , false);
            var yesterday = GetWhere(o =>
        o.CreatedAt.Date == DateTime.Today.AddDays(-1) &&
        o.OrderStatus != OrderStatus.Pending &&
        o.OrderStatus != OrderStatus.Canceled
        , false);


            var TodayBaskets = today.Count();
            decimal TodayBasketPrice = today.Sum(o => o.AfterDiscountPrice);


            int YesterdayBaskets = yesterday.Count();
            decimal YesterdayBasketPrice = yesterday.Sum(o => o.AfterDiscountPrice);


            var basketStats = new BasketStatsViewModel()
            {
                TodayBaskets = TodayBaskets,
                TodayBasketPrice = TodayBasketPrice,
                YesterdayBaskets = YesterdayBaskets,
                YesterdayBasketPrice = YesterdayBasketPrice
            };
            return basketStats;
        }
    }
}
