using EMY.Restaurant.Core.Application.Repositories.SliderContentRepositories;
using EMY.Restaurant.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EMY.Restaurant.Infrastructure.Persistence.Repositories.SliderContentRepositories
{
    public class SliderContentWriteRepository : WriteRepository<SliderContent>, ISliderContentWriteRepository
    {
        public SliderContentWriteRepository(DbContext context) : base(context)
        {
        }
    }
}
