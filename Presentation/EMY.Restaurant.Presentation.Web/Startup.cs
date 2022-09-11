using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using Serilog;
using EMY.Restaurant.Infrastructure.Persistence;
using EMY.Restaurant.Presentation.Web.Statics;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

namespace EMY.Restaurant.Presentation.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddPersistanceServices();
            Log.Debug("Servicess Added");
            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(15);

                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/Login";
                options.SlidingExpiration = true;
            });
            Log.Information("Login page settings Ok");
            services.AddAuthentication(SystemMainStatics.DefaultScheme)
             .AddCookie(SystemMainStatics.DefaultScheme, options =>
             {
                 options.LoginPath = new PathString("/Account/Login");
                 options.AccessDeniedPath = "/Account/Login";
             });
            Log.Information("AddAuthentication Ok");

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Debug()
               .WriteTo.File(Environment.CurrentDirectory + "/logs/myapp.txt", rollingInterval: RollingInterval.Day)
               .CreateLogger();

            if (env.IsDevelopment())
            {
                Log.Information("Start IsDevelopment");
                app.UseDeveloperExceptionPage();
                Log.Information("IsDevelopment ok");
            }
            else
            {
                Log.Information("Start Release and hsts");
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                Log.Information("End Release and hsts");
            }

            var defaultCulture = new CultureInfo("de-DE");
            var localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(defaultCulture),
                SupportedCultures = new List<CultureInfo> { defaultCulture },
                SupportedUICultures = new List<CultureInfo> { defaultCulture }
            };
            app.UseRequestLocalization(localizationOptions);

            Log.Information("Culture ok");

            app.UseHttpsRedirection();
            Log.Information("app usehttpsredirections ok");
            app.UseStaticFiles();
            Log.Information("Static files");
            app.UseRouting();
            Log.Information("use rooting");
            app.UseAuthorization();
            Log.Information("useauthorization");
            app.UseAuthentication();
            Log.Information("useauthentication");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            Log.Information("map controller ok");
        }
    }
}
