using DevLounge.Data;
using DevLounge.Data.Models;
using DevLounge.Data.Repositories;
using DevLounge.Service.Data.ForumCategories;
using DevLounge.Service.Data.ForumReplies;
using DevLounge.Service.Data.ForumSections;
using DevLounge.Service.Data.ForumThreads;
using DevLounge.Service.Utility;
using DevLounge.Web.Seed;
using DevLounge.Web.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DevLounge.Web
{
    public class Program
    {
        public static void ConfigureServices(WebApplicationBuilder builder)
        {
            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<DevLoungeDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Forum Repositories
            builder.Services.AddTransient<ForumSectionsRepository, ForumSectionsRepository>();
            builder.Services.AddTransient<ForumCategoriesRepository, ForumCategoriesRepository>();
            builder.Services.AddTransient<ForumThreadsRepository, ForumThreadsRepository>();
            builder.Services.AddTransient<ForumRepliesRepository, ForumRepliesRepository>();

            // Forum Services
            builder.Services.AddTransient<IForumSectionsService, ForumSectionsService>();
            builder.Services.AddTransient<IForumCategoriesService, ForumCategoriesService>();
            builder.Services.AddTransient<IForumThreadsService, ForumThreadsService>();
            builder.Services.AddTransient<IForumRepliesService, ForumRepliesService>();

            // Forum Utilities
            builder.Services.AddSingleton<IForumInternalRoutingUtility, ForumInternalRoutingUtility>();
            builder.Services.AddSingleton<ITimestampMappingUtility, TimestampMappingUtility>();
            //builder.Services.AddSingleton<ICloudinaryService>(instance => new CloudinaryService(
            //    builder.Configuration["Cloudinary:CloudName"],
            //    builder.Configuration["Cloudinary:ApiKey"],
            //    builder.Configuration["Cloudinary:ApiSecret"]));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services
                .AddDefaultIdentity<DevLoungeUser>(options => {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = 3;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<DevLoungeDbContext>();


            builder.Services.AddRazorPages();
            builder.Services.AddHttpContextAccessor();
        }

        private static void ConfigureAndRunApplication(WebApplicationBuilder builder)
        {
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.SeedRoles();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
            });

            app.Run();
        }

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder);
            ConfigureAndRunApplication(builder);
        }
    }
}