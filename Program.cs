using ContosoUniversity_TARpe21.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.VisualStudio.Web.CodeGeneration.Design;

public class Programm
{ 
    private static void Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();
        builder.Services.AddDbContext<SchoolContext>(options => options.
        UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        var app = builder.Build();

        CreateDbIfNotExsits(app);

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
    private static void CreateDbIfNotExsits(IHost host)
    {
       using(var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<
                    SchoolContext>();
                DbInitializer.Initalize(context);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "an error BUZZ");
            }
        }
    }
 
}