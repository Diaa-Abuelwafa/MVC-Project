using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Repositories;
using DataAccessLayer.Data.Contexts;
using Microsoft.EntityFrameworkCore;

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
            builder.Services.AddScoped<IDepartmentRepository,DepartmentRepository>();

            // Add Custom Service To The Container
            builder.Services.AddScoped<IEmployeeRepository,EmployeeRepository>();

            // Add DbContext Service To The Container
            builder.Services.AddDbContext<AppDbContext>(optionBuilder =>
            {
                // Read ConnectionString From AppSettings.json
                optionBuilder.UseSqlServer(builder.Configuration.GetConnectionString("cs"));
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            // General Map Route
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Department}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
