using EMY.Restaurant.Core.Application.Abstract;
using EMY.Restaurant.Core.Application.Repositories.MailListRepositories;
using EMY.Restaurant.Core.Application.Repositories.MenuCategoryRepositories;
using EMY.Restaurant.Core.Application.Repositories.MenuRepositories;
using EMY.Restaurant.Core.Application.Repositories.OrderItemRepositories;
using EMY.Restaurant.Core.Application.Repositories.OrderRepositories;
using EMY.Restaurant.Core.Application.Repositories.PasswordHistoryRepositories;
using EMY.Restaurant.Core.Application.Repositories.PhotoRepositories;
using EMY.Restaurant.Core.Application.Repositories.PostRepositories;
using EMY.Restaurant.Core.Application.Repositories.ReservationRepositories;
using EMY.Restaurant.Core.Application.Repositories.SliderContentRepositories;
using EMY.Restaurant.Core.Application.Repositories.UserGroupRepositories;
using EMY.Restaurant.Core.Application.Repositories.UserGroupRoleRepositories;
using EMY.Restaurant.Core.Application.Repositories.UserRepositories;
using EMY.Restaurant.Infrastructure.Persistence.Concrete;
using EMY.Restaurant.Infrastructure.Persistence.Contexts;
using EMY.Restaurant.Infrastructure.Persistence.Repositories.BasketItemRepositories;
using EMY.Restaurant.Infrastructure.Persistence.Repositories.BasketRepositories;
using EMY.Restaurant.Infrastructure.Persistence.Repositories.MailListRepositories;
using EMY.Restaurant.Infrastructure.Persistence.Repositories.MenuCategoryRepositories;
using EMY.Restaurant.Infrastructure.Persistence.Repositories.MenuRepositories;
using EMY.Restaurant.Infrastructure.Persistence.Repositories.PasswordHistoryRepositories;
using EMY.Restaurant.Infrastructure.Persistence.Repositories.PhotoRepositories;
using EMY.Restaurant.Infrastructure.Persistence.Repositories.PostRepositories;
using EMY.Restaurant.Infrastructure.Persistence.Repositories.ReservationRepositories;
using EMY.Restaurant.Infrastructure.Persistence.Repositories.SliderContentRepositories;
using EMY.Restaurant.Infrastructure.Persistence.Repositories.UserGroupRepositories;
using EMY.Restaurant.Infrastructure.Persistence.Repositories.UserGroupRoleRepositories;
using EMY.Restaurant.Infrastructure.Persistence.Repositories.UserRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EMY.Restaurant.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistanceServices(this IServiceCollection services)
        {
            services.AddDbContext<EMYRestaurantDbContext>(options => options.UseSqlServer(Configuration.ConnectionString), ServiceLifetime.Transient);
            services.AddScoped<DbContext, EMYRestaurantDbContext>();
            services.AddPersistanceServicesReads();
            services.AddPersistanceServicesWrites();
            services.AddScoped<IEmailService, EmailManager>();
            services.AddScoped<IDatabaseFactory, DatabaseFactory>();
        }

        public static void AddPersistanceServicesReads(this IServiceCollection services)
        {
            services.AddScoped<IOrderItemReadRepository, OrderItemReadRepository>();
            services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            services.AddScoped<IMailListReadRepository, MailListReadRepository>();
            services.AddScoped<IMenuCategoryReadRepository, MenuCategoryReadRepository>();
            services.AddScoped<IMenuReadRepository, MenuReadRepository>();
            services.AddScoped<IPhotoReadRepository, PhotoReadRepository>();
            services.AddScoped<IReservationReadRepository, ReservationReadRepository>();
            services.AddScoped<IUserGroupReadRepository, UserGroupReadRepository>();
            services.AddScoped<IUserGroupRoleReadRepository, UserGroupRoleReadRepository>();
            services.AddScoped<IUserReadRepository, UserReadRepository>();
            services.AddScoped<IPasswordHistoryReadRepository, PasswordHistoryReadRepository>();
            services.AddScoped<ISliderContentReadRepository, SliderContentReadRepository>();
            services.AddScoped<IPostReadRepository, PostReadRepository>();
        }

        public static void AddPersistanceServicesWrites(this IServiceCollection services)
        {
            services.AddScoped<IOrderItemWriteRepository, OrderItemWriteRepository>();
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
            services.AddScoped<IMailListWriteRepository, MailListWriteRepository>();
            services.AddScoped<IMenuCategoryWriteRepository, MenuCategoryWriteRepository>();
            services.AddScoped<IMenuWriteRepository, MenuWriteRepository>();
            services.AddScoped<IPhotoWriteRepository, PhotoWriteRepository>();
            services.AddScoped<IReservationWriteRepository, ReservationWriteRepository>();
            services.AddScoped<IUserGroupWriteRepository, UserGroupWriteRepository>();
            services.AddScoped<IUserGroupRoleWriteRepository, UserGroupRoleWriteRepository>();
            services.AddScoped<IUserWriteRepository, UserWriteRepository>();
            services.AddScoped<IPasswordHistoryWriteRepository, PasswordHistoryWriteRepository>();
            services.AddScoped<ISliderContentWriteRepository, SliderContentWriteRepository>();
            services.AddScoped<IPostWriteRepository, PostWriteRepository>();
        }
    }
}
