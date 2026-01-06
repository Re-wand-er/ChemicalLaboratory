using ChemicalLaboratory.Domain.Interfaces;
using ChemicalLaboratory.Infrastructure.Persistence;
using ChemicalLaboratory.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;

namespace ChemicalLaboratory
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //------------------------------------------------------------------------------------------------------------

            builder.Services.AddControllersWithViews()
                .AddRazorOptions(options =>
                {
                    options.ViewLocationFormats.Clear();
                    options.ViewLocationFormats.Add("/WebUI/Views/{1}/{0}.cshtml");
                    options.ViewLocationFormats.Add("/WebUI/Views/Home/{0}.cshtml");
                    options.ViewLocationFormats.Add("/WebUI/Views/Shared/{0}.cshtml");
                });

            //------------------------------------------------------------------------------------------------------------
            builder.Services.AddDbContext<DataBaseContext>(options => 
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IReagentRepository, ReagentRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
            builder.Services.AddScoped<INotificationRepository, NotificationRepository>();

            //builder.Services.AddScoped<IUserService, UserService>();

            //------------------------------------------------------------------------------------------------------------

            //builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //     .AddCookie(options =>
            //     {
            //         options.LoginPath = "/Authorisation/Index"; // Страница входа
            //         options.AccessDeniedPath = "/Home/Index"; // Страница доступа запрещена
            //         options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Срок действия cookie
            //     });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //app.UseAuthentication();
            //app.UseAuthorization();
            
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"
                );

            app.Run();
        }
    }
}
