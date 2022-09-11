using EMY.Restaurant.Core.Domain.Common;
using EMY.Restaurant.Core.Domain.Entities;
using EMY.Restaurant.Core.Domain.ViewModels;
using System;
using System.Collections.Generic;

namespace EMY.Restaurant.Core.Application.Repositories.ReservationRepositories
{
    public interface IReservationReadRepository : IReadRepository<Reservation>
    {
        ReservationStatsViewModel GetReservationStats();
        List<Reservation> GetReservations();
        List<Reservation> GetAuthorizedReservations();
        List<Reservation> GetReservationsByDate(DateTime date);
        List<Reservation> GetReservationsByDate(DateTime date, ReservationConfirmationStatus status);
    }
}
