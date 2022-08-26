using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookly_Back_End.DAL;
using Bookly_Back_End.Data;
using Bookly_Back_End.Interfaces;
using Bookly_Back_End.Models;
using Bookly_Back_End.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Stripe;

namespace Bookly_Back_End
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;

        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<LayoutService>();
            services.AddScoped<IBookOperation, BookOperation>();
            services.AddScoped<IOrderConfirmation, OrderConfirmation>();
            services.AddScoped<IQuery, Query>();
            services.AddControllersWithViews();
            services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(_configuration.GetConnectionString("Default"));
            });
            services.AddIdentity<AppUser, IdentityRole>(opt =>
            {

                opt.User.RequireUniqueEmail = true;

                opt.Password.RequireDigit = true;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequiredLength = 8;

                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                opt.Lockout.MaxFailedAccessAttempts = 3;
                opt.SignIn.RequireConfirmedEmail = true;
                opt.User.AllowedUserNameCharacters = "qwertyuiopasdfghjklzxcvbnm_1234567890";
                
            }).AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();
            services.AddHttpContextAccessor();
            services.Configure<StripeSettings>(_configuration.GetSection("Stripe"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            StripeConfiguration.ApiKey = "sk_test_51LYFKMEmelNugo2zhXQYyBbrt3VXwX4s8twljHcftTn7fmWQbVktG1gJEy4ZakXTVvZRx9CMmrNtJbMx2feNI1TY00TwsyYqtC";
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                   name: "areas",
                   pattern: "{area:exists}/{controller=dashboard}/{action=Index}/{id?}");


                endpoints.MapControllerRoute(
                    name: "Default",
                    pattern: "{controller=home}/{action=Index}/{id?}");
            });
        }
    }
}
