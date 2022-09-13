using EMY.Restaurant.Core.Application.Abstract;
using EMY.Restaurant.Core.Domain.Common;
using EMY.Restaurant.Core.Domain.Entities;
using EMY.Restaurant.Core.Domain.ViewModels;
using EMY.Restaurant.Infrastructure.Persistence;
using EMY.Restaurant.Presentation.Web.Statics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EMY.Restaurant.Presentation.Web.Controllers
{

    public class AdminController : Controller
    {

        private IWebHostEnvironment Environment;
        private readonly IDatabaseFactory Database;
        private readonly IEmailService _mailSystem;
        public AdminController(IDatabaseFactory database, IWebHostEnvironment environment, IEmailService mailSystem)
        {
            Database = database;
            Environment = environment;
            _mailSystem = mailSystem;
        }

        [EMY_ISINROLE(Forms.Admin, AuthType.Read)]
        public async Task<IActionResult> Index()
        {

            var reservationstats = Database.ReservationRead.GetReservationStats();
            ViewBag.ReservationStats = reservationstats;

            var basketstats = Database.OrderRead.BasketStats();
            ViewBag.BasketStats = basketstats;

            return View();
        }
        [HttpGet]
        [EMY_ISINROLE(Forms.OrderManagement, AuthType.Read)]
        public async Task<IActionResult> Order()
        {
            var orders = Database.OrderRead.Table.AsNoTracking().Include(o => o.OrderItems).Where(o => !o.IsDeleted &&
            (o.OrderStatus != OrderStatus.Finished && o.OrderStatus != OrderStatus.Canceled && o.OrderStatus != OrderStatus.Pending)
            && o.OrderItems.Count > 0).OrderByDescending(o => o.CreatedAt);
            return View(orders.ToList());
        }
        [HttpPost]
        [EMY_ISINROLE(Forms.OrderManagement, AuthType.Read)]
        public async Task<IActionResult> Order(OrderStatus status)
        {
            var orders = Database.OrderRead.Table.AsNoTracking().Include(o => o.OrderItems).Where(o => !o.IsDeleted && o.OrderStatus == status).OrderByDescending(o => o.CreatedAt);
            return View(orders.ToList());
        }
        [HttpGet]
        [EMY_ISINROLE(Forms.OrderManagement, AuthType.Read)]
        public async Task<IActionResult> OrderDetails(string id)
        {
            var order = await Database.OrderRead.Table.Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.OrderID == id.ToGuid());
            if (order == null) return NotFound();
            return View(order);
        }
        [EMY_ISINROLE(Forms.MenuDesign, AuthType.Read)]
        public async Task<IActionResult> MenuDesign(string menuCategoryID)
        {
            return View();
        }

        [Microsoft.AspNetCore.Authorization.Authorize(SystemMainStatics.DefaultScheme)]
        async Task<Photo> UploadImageold(IFormFile file, int maxWidth = 1700, int thumbWidth = 128, int thumbHeight = 128, bool generateThumb = true)
        {
            Log.Debug("Upload Started!");
            if (file.ContentType.StartsWith("image"))
            {
                Photo photo = new Photo();
                photo.PhotoID = Guid.NewGuid();
                photo.FileName = file.FileName;
                photo.Extention = ".jpg";
                await Database.PhotoWrite.AddAsync(photo, this.ActiveUserID());


                //MemoryStream memory = new MemoryStream();

                //await file.CopyToAsync(memory);

                string wwwPath = this.Environment.WebRootPath;
                string contentPath = this.Environment.ContentRootPath;
                string path = Path.Combine(this.Environment.WebRootPath, "Uploads/Photos/");
                string tempPath = Path.Combine(path, photo.PhotoID + "_org" + photo.Extention);
                string filePath = Path.Combine(path, photo.PhotoID + photo.Extention);
                string filePathThumb = Path.Combine(path, photo.PhotoID + "_thumb" + photo.Extention);

                using (var fileStream = new FileStream(tempPath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }


                Image image = Image.FromFile(tempPath);
                //Log.Debug("Image.FromStream(memory)");
                //image.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                //Log.Debug("image.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);");
                if (image.Width > maxWidth)
                {

                    int mustHeight = (int)(((decimal)maxWidth / (decimal)image.Width) * (decimal)image.Height);
                    var original = image.ResizeImage(new Size(maxWidth, mustHeight));
                    original.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                else
                {
                    image.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                }

                if (generateThumb)
                {
                    var thumb = image.ResizeImage(new Size(thumbWidth, thumbHeight));
                    thumb.Save(filePathThumb, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                image.Dispose();
                image = null;
                System.IO.File.Delete(tempPath);
                return photo;
            }
            return new Photo();
        }


        [Microsoft.AspNetCore.Authorization.Authorize(SystemMainStatics.DefaultScheme)]
        async Task<Photo> UploadImage(IFormFile file, int width, int height, int thumbWidth = 128, int thumbHeight = 128, bool generateThumb = true)
        {
            Log.Debug("Upload Started!");
            if (file.ContentType.StartsWith("image"))
            {
                Photo photo = new Photo();
                photo.PhotoID = Guid.NewGuid();
                photo.FileName = file.FileName;
                photo.Extention = System.IO.Path.GetExtension(file.FileName);
                await Database.PhotoWrite.AddAsync(photo, this.ActiveUserID());


                //MemoryStream memory = new MemoryStream();

                //await file.CopyToAsync(memory);

                string wwwPath = this.Environment.WebRootPath;
                string contentPath = this.Environment.ContentRootPath;
                string path = Path.Combine(this.Environment.WebRootPath, "Uploads/Photos/");
                string tempPath = Path.Combine(path, photo.PhotoID + "_org" + photo.Extention);
                string filePath = Path.Combine(path, photo.PhotoID + photo.Extention);
                string filePathThumb = Path.Combine(path, photo.PhotoID + "_thumb" + photo.Extention);

                using (var fileStream = new FileStream(tempPath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }


                Image image = Image.FromFile(tempPath);
                //Log.Debug("Image.FromStream(memory)");
                //image.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                //Log.Debug("image.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);");

                var original = image.ResizeImage(new Size(width, height));
                original.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);

                if (generateThumb)
                {
                    var thumb = image.ResizeImage(new Size(thumbWidth, thumbHeight));
                    thumb.Save(filePathThumb, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                image.Dispose();
                image = null;
                System.IO.File.Delete(tempPath);
                return photo;
            }
            return new Photo();
        }


        [HttpGet]
        [EMY_ISINROLE(Forms.MenuDesign, AuthType.Read)]
        public async Task<IActionResult> MenuCategory()
        {
            List<MenuCategory> menuCategory = await Database.MenuCategoryRead.GetAllMenuCategoryWithMenus();
            return View(menuCategory);
        }

        [HttpPost, ValidateAntiForgeryToken]
        [EMY_ISINROLE(Forms.MenuDesign, AuthType.Write)]
        public async Task<IActionResult> SaveCategory(string menuCategoryID, string name, string description, IFormFile headerPhoto, IFormFile logoPhoto)
        {
            string HeaderPhotoUrl = string.Empty;
            Guid HeaderPhotoID = Guid.Empty;

            string LogoPhotoUrl = string.Empty;
            Guid LogoPhotoID = Guid.Empty;

            if (headerPhoto != null)
            {
                var uploadedHeaderPhoto = new Photo();
                if (logoPhoto == null)
                {
                    uploadedHeaderPhoto = await UploadImage(headerPhoto, 1400, 780, 248, 248, true);
                    LogoPhotoID = uploadedHeaderPhoto.PhotoID;
                    LogoPhotoUrl = uploadedHeaderPhoto.PhotoID.ToString() + "_thumb" + uploadedHeaderPhoto.Extention;
                }
                else uploadedHeaderPhoto = await UploadImage(headerPhoto, width: 1400, height: 780, generateThumb: false);
                HeaderPhotoUrl = uploadedHeaderPhoto.PhotoID.ToString() + uploadedHeaderPhoto.Extention;
                HeaderPhotoID = uploadedHeaderPhoto.PhotoID;

            }


            if (logoPhoto != null)
            {
                var uploadedLogoPhoto = await UploadImage(logoPhoto, width: 248, height: 248, generateThumb: false);
                LogoPhotoUrl = uploadedLogoPhoto.PhotoID.ToString() + uploadedLogoPhoto.Extention; ;
                LogoPhotoID = uploadedLogoPhoto.PhotoID;
            }

            if (menuCategoryID.ToGuid() == Guid.Empty)
            {
                MenuCategory menuCategory = new MenuCategory()
                {
                    Description = description,
                    Name = name,
                    Active = true,
                    LogoPhotoID = LogoPhotoID,
                    HeaderPhotoID = HeaderPhotoID,
                    HeaderPhotoURL = HeaderPhotoUrl,
                    LogoPhotoURL = LogoPhotoUrl
                };
                await Database.MenuCategoryWrite.AddAsync(menuCategory, this.ActiveUserID());
                return Ok();
            }
            else
            {
                MenuCategory menuCategory = await Database.MenuCategoryRead.GetByIdAsync(menuCategoryID.ToGuid());
                if (menuCategory == null)
                {
                    return StatusCode(404, "Menu Category not found");// NotFound("Menu Category not found");
                }

                menuCategory.Description = description;
                menuCategory.Name = name;
                if (headerPhoto != null)
                {
                    menuCategory.HeaderPhotoID = HeaderPhotoID;
                    menuCategory.HeaderPhotoURL = HeaderPhotoUrl;
                }


                if (logoPhoto != null)
                {
                    menuCategory.LogoPhotoID = LogoPhotoID;
                    menuCategory.LogoPhotoURL = LogoPhotoUrl;
                }
                await Database.MenuCategoryWrite.UpdateAsync(menuCategory, this.ActiveUserID());
                return Ok();
            }
        }

        [HttpPost]
        [EMY_ISINROLE(Forms.MenuDesign, AuthType.Delete)]
        public async Task<IActionResult> DeleteCategory(string MenuCategoryID)
        {
            MenuCategory menuCategory = await Database.MenuCategoryRead.GetByIdAsync(MenuCategoryID.ToGuid());
            if (menuCategory == null)
            {
                return NotFound("Menu Category not found");
            }
            await Database.MenuCategoryWrite.RemoveAsync(menuCategory, this.ActiveUserID());
            return Ok($"{menuCategory.Name} has been removed!");
        }

        [HttpPost]
        [EMY_ISINROLE(Forms.MenuDesign, AuthType.Delete)]
        public async Task<IActionResult> DeleteMenu(string MenuID)
        {
            Menu menu = await Database.MenuRead.GetByIdAsync(MenuID.ToGuid());
            if (menu == null)
            {
                return NotFound("Menu not found");
            }
            await Database.MenuWrite.RemoveAsync(menu, this.ActiveUserID());
            return Ok($"{menu.Name} has been removed!");
        }

        [HttpGet]
        [EMY_ISINROLE(Forms.MenuDesign, AuthType.Write)]
        public async Task<IActionResult> CreateOrEditCategory(string categoryid)
        {
            if (!string.IsNullOrEmpty(categoryid))
            {
                MenuCategory menuCategory = await Database.MenuCategoryRead.GetByIdAsync(categoryid.ToGuid()) ?? new MenuCategory();
                return PartialView(menuCategory);
            }
            return PartialView(new MenuCategory());
        }

        [HttpGet]
        [EMY_ISINROLE(Forms.MenuDesign, AuthType.Read)]
        public async Task<IActionResult> GetMenu(string MenuCategoryID)
        {
            IList<Menu> menuItems = Database.MenuRead.GetMenuFromCategory(Guid.Parse(MenuCategoryID));
            return View(menuItems);
        }

        [HttpPost]
        [EMY_ISINROLE(Forms.MenuDesign, AuthType.Write)]
        public async Task<IActionResult> SaveMenu(Menu menu, IFormFile photoContent)
        {
            int result = 0;
            if (photoContent != null)
            {
                var photo = await UploadImage(photoContent, 800, 600, 400, 400, true);
                menu.PhotoID = photo.PhotoID;
                menu.PhotoFileName = photo.PhotoID.ToString() + photo.Extention;
                menu.PhotoThumbFileName = photo.PhotoID.ToString() + "_thumb" + photo.Extention;
            }

            if (menu.MenuID == Guid.Empty)
                result = await Database.MenuWrite.AddAsync(menu, this.ActiveUserID());
            else
            {
                var currentMenu = await Database.MenuRead.GetByIdAsync(menu.MenuID);
                if (currentMenu == null)
                    return NotFound("Menu not found!");


                currentMenu.Code = menu.Code;
                currentMenu.Name = menu.Name;
                currentMenu.Description = menu.Description;
                currentMenu.Price = menu.Price;
                currentMenu.DayOfWeek = menu.DayOfWeek;
                currentMenu.DayOfWeekPrice = menu.DayOfWeekPrice;
                currentMenu.MenuCategoryID = menu.MenuCategoryID;
                if (photoContent != null)
                {
                    currentMenu.PhotoID = menu.PhotoID;
                    currentMenu.PhotoFileName = menu.PhotoFileName;
                    currentMenu.PhotoThumbFileName = menu.PhotoThumbFileName;
                }

                result = await Database.MenuWrite.UpdateAsync(currentMenu, this.ActiveUserID());
            }



            if (result > 0)
            {
                return Ok(menu);
            }
            else
            {
                return BadRequest("Samething went wrong!");
            }
        }

        [HttpGet]
        [EMY_ISINROLE(Forms.MenuDesign, AuthType.Write)]
        public async Task<IActionResult> CreateOrEditMenu(string MenuCategoryID, string MenuID = "")
        {
            var menu = new Menu();
            menu.MenuCategoryID = Guid.Parse(MenuCategoryID);
            if (!string.IsNullOrEmpty(MenuID))
            {
                menu = await Database.MenuRead.GetByIdAsync(Guid.Parse(MenuID));
            }
            return PartialView("CreateOrEditMenu", menu);
        }
        [EMY_ISINROLE(Forms.MenuDesign, AuthType.Read)]
        public async Task<IActionResult> GetMenuDetailsAsync(string MenuID)
        {
            var menu = await Database.MenuRead.GetByIdAsync(Guid.Parse(MenuID));
            return View("CreateOrEdit", menu);
        }

        [EMY_ISINROLE(Forms.Reservation_Management, AuthType.Read)]
        public async Task<IActionResult> Reservations()
        {
            DateTime
                Begindt = DateTime.Today.AddDays(-1),
                Enddt = DateTime.Today.AddDays(1);

            List<Reservation> reservations = await Database.ReservationRead.Table.AsNoTracking().Where(o => !o.IsDeleted && o.Date >= Begindt && o.Date <= Enddt).OrderByDescending(o => o.CreatedAt).ToListAsync();
            ReservationPageResultViewModel result = new ReservationPageResultViewModel();
            result.AuthorizedReservations = reservations.Where(o => o.ConfirmationStatus == ReservationConfirmationStatus.Confirmed).ToList();
            result.UnAuthorizedReservations = reservations.Where(o => o.ConfirmationStatus == ReservationConfirmationStatus.Rejected).ToList();
            result.Pendings = await Database.ReservationRead.GetWhere(o => !o.IsDeleted && o.ConfirmationStatus == ReservationConfirmationStatus.Authorized).ToListAsync();
            result.Begin = Begindt;
            result.End = Enddt;
            return View(result);
        }
        [HttpPost]
        [EMY_ISINROLE(Forms.Reservation_Management, AuthType.Read)]
        public async Task<IActionResult> Reservations(DateTime Begin, DateTime End)
        {
            List<Reservation> reservations = await Database.ReservationRead.Table.AsNoTracking().Where(o => !o.IsDeleted && o.Date >= Begin && o.Date <= End).OrderByDescending(o => o.CreatedAt).ToListAsync();
            ReservationPageResultViewModel result = new ReservationPageResultViewModel();
            result.AuthorizedReservations = reservations.Where(o => o.ConfirmationStatus == ReservationConfirmationStatus.Confirmed).ToList();
            result.UnAuthorizedReservations = reservations.Where(o => o.ConfirmationStatus == ReservationConfirmationStatus.Rejected).ToList();
            result.Pendings = await Database.ReservationRead.GetWhere(o => !o.IsDeleted && o.ConfirmationStatus == ReservationConfirmationStatus.Authorized).ToListAsync();
            result.Begin = Begin;
            result.End = End;
            return View(result);
        }

        [HttpPost]
        [EMY_ISINROLE(Forms.Reservation_Management, AuthType.Write)]
        public async Task<IActionResult> SetReservationStatusSettings(string reservationid, ReservationConfirmationStatus status)
        {
            var reservation = await Database.ReservationRead.GetByIdAsync(reservationid.ToGuid());
            if (reservation == null)
            {
                return NotFound("Reservation not found!");
            }
            if (reservation.ConfirmationStatus == status && status == ReservationConfirmationStatus.Confirmed)
            {
                return BadRequest("Reservation already confirmed!");
            }
            if (reservation.ConfirmationStatus == status && status == ReservationConfirmationStatus.Rejected)
            {
                return BadRequest("Reservation already rejected!");
            }

            if (reservation.ConfirmationStatus == ReservationConfirmationStatus.Pending && status != ReservationConfirmationStatus.Authorized)
            {
                return BadRequest("Reservation is pending, you can only change to authorized!");
            }




            reservation.ConfirmationStatus = status;
            await Database.ReservationWrite.UpdateAsync(reservation, this.ActiveUserID());
            string confirmationMessage = "";
            switch (status)
            {
                case ReservationConfirmationStatus.Confirmed:
                    confirmationMessage = "confirmed!";
                    break;
                case ReservationConfirmationStatus.Rejected:
                    confirmationMessage = "rejected!";
                    break;
                case ReservationConfirmationStatus.Authorized:
                    confirmationMessage = "authorized!";
                    break;
            }

            await _mailSystem.SendEmail(reservation.Email, $"{Configuration.SystemName} Reservation Confirmation", $"Your reservation has been {confirmationMessage}", System.Net.Mail.MailPriority.High);
            return Ok(reservation);
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMailList(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Email is required!");
            }
            var mail = Database.MailListRead.Get(o => o.Email == email && !o.IsDeleted);
            if (mail == null)
            {
                await Database.MailListWrite.AddAsync(new MailList() { Email = email }, Guid.Empty);

                return Ok();
            }
            else
                return BadRequest("Email already exists!");

        }

        [HttpPost]
        [EMY_ISINROLE(Forms.OrderManagement, AuthType.Write)]
        public async Task<IActionResult> SuccessOrderItem(string id)
        {
            var orderItem = await Database.OrderItemRead.GetByIdAsync(id.ToGuid());
            if (orderItem == null)
            {
                return NotFound("Order item not found!");
            }
            if (orderItem.IsSuccess)
            {
                return BadRequest("Order item already success!");
            }

            var order = await Database.OrderRead.GetByIdAsync(orderItem.OrderID);
            if (order == null)
            {
                return NotFound("Order not found!");
            }

            orderItem.IsSuccess = true;
            await Database.OrderItemWrite.UpdateAsync(orderItem, this.ActiveUserID());
            var totalcount = await Database.OrderItemRead.Table.CountAsync(o => o.OrderID == orderItem.OrderID && !o.IsDeleted && !o.IsSuccess);

            if (totalcount == 0)
            {
                order.OrderStatus = OrderStatus.Ready;
                await Database.OrderWrite.UpdateAsync(order, this.ActiveUserID());
                CultureInfo culture = new CultureInfo("de-DE");
                await _mailSystem.SendEmail(order.EmailAdress, $"{Configuration.SystemName} Package Ready", $"Your order is ready Now {DateTime.Now.ToString("HH:mm dd MMM yyyy", culture)}", System.Net.Mail.MailPriority.High);
            }
            return Ok("Success");
        }

        [HttpPost]
        [EMY_ISINROLE(Forms.OrderManagement, AuthType.Write)]
        public async Task<IActionResult> DeliverPackage(string id)
        {
            var order = await Database.OrderRead.GetByIdAsync(id.ToGuid());
            if (order == null)
            {
                return NotFound("Order not found!");
            }

            order.IsSent = true;
            order.OrderStatus = OrderStatus.Finished;
            await Database.OrderWrite.UpdateAsync(order, this.ActiveUserID());
            return Ok("Success");
        }
        [HttpPost]
        [EMY_ISINROLE(Forms.OrderManagement, AuthType.Update)]
        public async Task<IActionResult> SetTimeToOrder(string orderid, DateTime time)
        {
            var order = await Database.OrderRead.GetByIdAsync(orderid.ToGuid());
            if (order == null)
            {
                return NotFound("Order not found!");
            }
            order.DestinationTime = time;
            await Database.OrderWrite.UpdateAsync(order, this.ActiveUserID());
            await _mailSystem.SendEmail(order.EmailAdress, $"{Configuration.SystemName} Order Time", $"Your order will be ready at {time}", System.Net.Mail.MailPriority.High);
            return Ok("Success");
        }

        [HttpGet]
        [EMY_ISINROLE(Forms.HomePageContentEditor, AuthType.Read)]
        public async Task<IActionResult> HomePageContents()
        {
            return View();
        }

        [HttpPost]
        [EMY_ISINROLE(Forms.HomePageContentEditor, AuthType.Update)]
        public async Task<IActionResult> SaveHomePageContent(string key, string value)
        {
            var result = HomePageConfiguration.SetValue(key, value.Replace("\"", "'"));
            return Ok(result.Message);
        }

        [HttpPost]
        [EMY_ISINROLE(Forms.HomePageContentEditor, AuthType.Update)]
        public async Task<IActionResult> UploadHomePageContent(string key, IFormFile photoContent)
        {

            if (photoContent == null) return NoContent();
            try
            {
                var photo = await UploadImageold(photoContent, generateThumb: false);
                var result = HomePageConfiguration.SetValue(key, photo.PhotoID.ToString() + photo.Extention);
                return Ok(result.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [EMY_ISINROLE(Forms.HomePageContentEditor, AuthType.Read)]
        public async Task<IActionResult> HomePageSlider()
        {
            return View();
        }

        [HttpPost]
        [EMY_ISINROLE(Forms.HomePageContentEditor, AuthType.Write)]
        public async Task<IActionResult> SaveNewSliderContent(int newelementIndex, IFormFile newelementPhoto, string newelementHeader,
            string newelementContent, int newelementAlign, string newelementLink)
        {
            if (newelementPhoto == null) return BadRequest("Photo was empty!");
            var photo = await UploadImage(newelementPhoto, width: 1450, height: 750, generateThumb: false);
            if (photo.PhotoID == Guid.Empty)
                return UnprocessableEntity("Picture is empty!");

            if (Database.SliderContentRead.GetWhere(o => !o.IsDeleted && o.SliderIndex == newelementIndex).Count() > 0)
            {
                var contents = Database.SliderContentRead.GetWhere(o => !o.IsDeleted && o.SliderIndex >= newelementIndex).OrderBy(o => o.SliderIndex).ToList();
                for (int i = 0; i < contents.Count; i++)
                {
                    if (contents.Count == i + 1) contents[i].SliderIndex++;
                    else
                    {
                        if (contents[i].SliderIndex + 1 < contents[i + 1].SliderIndex)
                        {
                            contents[i].SliderIndex++;
                            break;
                        }
                        else contents[i].SliderIndex++;
                    }

                    await Database.SliderContentWrite.UpdateAsync(contents[i], this.ActiveUserID());
                }
            }

            var side = (ContentSide)newelementAlign;
            var result = await Database.SliderContentWrite.AddAsync(new SliderContent
            {
                Content = newelementContent,
                Header = newelementHeader,
                ImgUrl = photo.PhotoID.ToString() + photo.Extention,
                Side = side,
                SliderIndex = newelementIndex,
                Link = newelementLink
            }, this.ActiveUserID());

            return Ok();
        }

        [HttpPost]
        [EMY_ISINROLE(Forms.HomePageContentEditor, AuthType.Write)]
        public async Task<IActionResult> DeleteSlide(string slideId)
        {
            SliderContent slide = await Database.SliderContentRead.GetByIdAsync(slideId.ToGuid());
            if (slide == null)
            {
                return NotFound("Slide not found");
            }
            await Database.SliderContentWrite.RemoveAsync(slide, this.ActiveUserID());
            return Ok($"'{slide.Header}' has been removed!");
        }

        [HttpGet]
        [EMY_ISINROLE(Forms.MenuDesign, AuthType.Write)]
        public async Task<IActionResult> CreateOrEditSubCategory(string menuCategoryID, string masterMenuCategoryID)
        {
            var menuCategory = new MenuCategory() { MasterMenuCategoryID = masterMenuCategoryID.ToGuid() };
            if (menuCategoryID.ToGuid() != null)
            {
                menuCategory = await Database.MenuCategoryRead.GetByIdAsync(menuCategoryID.ToGuid());
            }
            return PartialView(menuCategory);

        }

        [HttpPost]
        [EMY_ISINROLE(Forms.MenuDesign, AuthType.Write)]
        public async Task<IActionResult> SaveSubCategory(string menuCategoryID, string masterMenuCategoryID, string name, string description)
        {
            if (menuCategoryID.ToGuid() != Guid.Empty)
            {
                var currentCategory = await Database.MenuCategoryRead.GetByIdAsync(menuCategoryID.ToGuid());
                if (currentCategory == null)
                    return NotFound("Menu category is not found!");

                currentCategory.Name = name;
                currentCategory.Description = description;
                await Database.MenuCategoryWrite.UpdateAsync(currentCategory, this.ActiveUserID());
                return Ok(currentCategory);
            }
            else
            {
                var menuCategory = new MenuCategory()
                {
                    MasterMenuCategoryID = masterMenuCategoryID.ToGuid(),
                    MenuCategoryID = menuCategoryID.ToGuid(),
                    Active = true,
                    Description = description,
                    Name = name,
                    IsDeleted = false
                };

                await Database.MenuCategoryWrite.AddAsync(menuCategory, this.ActiveUserID());
                return Ok("Ready!");
            }

        }

    }
}

