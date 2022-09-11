using EMY.Restaurant.Core.Application.Repositories.MailListRepositories;
using EMY.Restaurant.Core.Application.Repositories.MenuCategoryRepositories;
using EMY.Restaurant.Core.Application.Repositories.MenuRepositories;
using EMY.Restaurant.Core.Application.Repositories.OrderItemRepositories;
using EMY.Restaurant.Core.Application.Repositories.OrderRepositories;
using EMY.Restaurant.Core.Application.Repositories.PasswordHistoryRepositories;
using EMY.Restaurant.Core.Application.Repositories.PhotoRepositories;
using EMY.Restaurant.Core.Application.Repositories.ReservationRepositories;
using EMY.Restaurant.Core.Application.Repositories.SliderContentRepositories;
using EMY.Restaurant.Core.Application.Repositories.UserGroupRepositories;
using EMY.Restaurant.Core.Application.Repositories.UserGroupRoleRepositories;
using EMY.Restaurant.Core.Application.Repositories.UserRepositories;

namespace EMY.Restaurant.Infrastructure.Persistence
{
    public class DatabaseFactory : IDatabaseFactory
    {

        public DatabaseFactory(IMailListReadRepository mailListRead, IMailListWriteRepository mailListWrite, IMenuCategoryReadRepository menuCategoryRead, IMenuCategoryWriteRepository menuCategoryWrite, IMenuReadRepository menuRead, IMenuWriteRepository menuWrite, IOrderItemReadRepository orderItemRead, IOrderItemWriteRepository orderItemWrite, IOrderReadRepository orderRead, IOrderWriteRepository orderWrite, IPasswordHistoryReadRepository passwordHistory, IPasswordHistoryWriteRepository passwordHistoryWrite, IPhotoReadRepository photoRead, IPhotoWriteRepository photoWrite, IReservationReadRepository reservationRead, IReservationWriteRepository reservationWrite, IUserGroupReadRepository userGroupRead, IUserGroupWriteRepository userGroupWrite, IUserGroupRoleReadRepository userGroupRoleRead, IUserGroupRoleWriteRepository userGroupRoleWrite, IUserReadRepository userRead, IUserWriteRepository userWrite,ISliderContentReadRepository sliderContentRead, ISliderContentWriteRepository sliderContentWrite)
        {
            MailListRead = mailListRead;
            MailListWrite = mailListWrite;
            MenuCategoryRead = menuCategoryRead;
            MenuCategoryWrite = menuCategoryWrite;
            MenuRead = menuRead;
            MenuWrite = menuWrite;
            OrderItemRead = orderItemRead;
            OrderItemWrite = orderItemWrite;
            OrderRead = orderRead;
            OrderWrite = orderWrite;
            PasswordHistory = passwordHistory;
            PasswordHistoryWrite = passwordHistoryWrite;
            PhotoRead = photoRead;
            PhotoWrite = photoWrite;
            ReservationRead = reservationRead;
            ReservationWrite = reservationWrite;
            UserGroupRead = userGroupRead;
            UserGroupWrite = userGroupWrite;
            UserGroupRoleRead = userGroupRoleRead;
            UserGroupRoleWrite = userGroupRoleWrite;
            UserRead = userRead;
            UserWrite = userWrite;
            SliderContentRead = sliderContentRead;
            SliderContentWrite = sliderContentWrite;
        }
       

       public  IMailListReadRepository MailListRead { get; set; }
       public  IMailListWriteRepository MailListWrite { get; set; }
       public  IMenuCategoryReadRepository MenuCategoryRead { get; set; }
       public  IMenuCategoryWriteRepository MenuCategoryWrite { get; set; }
       public  IMenuReadRepository MenuRead { get; set; }
        public IMenuWriteRepository MenuWrite { get; set; }
       public  IOrderItemReadRepository OrderItemRead { get; set; }
       public  IOrderItemWriteRepository OrderItemWrite { get; set; }
       public  IOrderReadRepository OrderRead { get; set; }
       public  IOrderWriteRepository OrderWrite { get; set; }
       public  IPasswordHistoryReadRepository PasswordHistory { get; set; }
       public  IPasswordHistoryWriteRepository PasswordHistoryWrite { get; set; }
       public  IPhotoReadRepository PhotoRead { get; set; }
       public  IPhotoWriteRepository PhotoWrite { get; set; }
       public  IReservationReadRepository ReservationRead { get; set; }
       public  IReservationWriteRepository ReservationWrite { get; set; }
       public  IUserGroupReadRepository UserGroupRead { get; set; }
       public  IUserGroupWriteRepository UserGroupWrite { get; set; }
       public  IUserGroupRoleReadRepository UserGroupRoleRead { get; set; }
       public  IUserGroupRoleWriteRepository UserGroupRoleWrite { get; set; }
       public  IUserReadRepository UserRead { get; set; }
       public  IUserWriteRepository UserWrite { get; set; }
        public ISliderContentReadRepository SliderContentRead { get; set; }
        public ISliderContentWriteRepository SliderContentWrite { get; set; }
    }
}
