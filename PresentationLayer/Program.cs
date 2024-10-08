using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Repositories;
using DataAccessLayer.Data.Contexts;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PresentationLayer.Controllers.Helpers;
using PresentationLayer.Models.Mapping;

namespace PresentationLayer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Add Custom Services To The Container
            //builder.Services.AddScoped<IDepartmentRepository,DepartmentRepository>();

            // Add Custom Service To The Container
            //builder.Services.AddScoped<IEmployeeRepository,EmployeeRepository>();

            // Add Custom Service To The Container
            builder.Services.AddScoped<IUnitOfWorkRepository, UnitOfWorkRepository>();

            // Add Custom Service To The Container
            builder.Services.AddScoped<FileHelper>();

            // Add Identity Package Service To The Container
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                            .AddEntityFrameworkStores<AppDbContext>()
                            .AddDefaultTokenProviders();

            // Add DbContext Service To The Container
            builder.Services.AddDbContext<AppDbContext>(optionBuilder =>
            {
                // Read ConnectionString From AppSettings.json
                optionBuilder.UseSqlServer(builder.Configuration.GetConnectionString("cs"));
            });

            // Registe Built-In Service
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            // General Map Route
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Department}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
