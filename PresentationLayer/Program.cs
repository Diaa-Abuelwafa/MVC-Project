namespace PresentationLayer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            // Specific Map Route
            app.MapControllerRoute(
                name: "MyRoute02",
                pattern: "emp/{id:int?}",
                new
                {
                    controller = "Department",
                    action = "Index"
                });

            // General Map Route
            app.MapControllerRoute(
                name: "MyRoute01",
                pattern: "{controller=Department}/{action=Index}/{id?}/{name:alpha?}"
                );

            // General Map Route
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Department}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
