using EMY.Restaurant.Core.Application.Repositories.ReservationRepositories;
using EMY.Restaurant.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EMY.Restaurant.Infrastructure.Persistence.Repositories.ReservationRepositories
{
    public class ReservationWriteRepository : WriteRepository<Reservation>, IReservationWriteRepository
    {
        public ReservationWriteRepository(DbContext context) : base(context)
        {
        }
    }
}
