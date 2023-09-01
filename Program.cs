using ContosoUniversity_TARpe21.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

public class Programm
{ 
    private static void Main(string[] args)
    {

        var host = CreateHostBuilder(args).Build();
        createDbIfNotExcists(host);
        host.Run();

    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddControllersWithViews();
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
    builder.Services.AddDbContext<SchoolContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();
    }
    private static void createDbIfNotExcists(IHost host)
    {
       using(var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<SchoolContext>();
                DbInitializer.Initalize(context);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Programm>>();
                logger.LogError(ex, "an error has occured creating the database");
            }
        }
    }
    public static IHostBuilder CreateHostBuilder(String[] args) =>
     Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Programm>();
        });
}