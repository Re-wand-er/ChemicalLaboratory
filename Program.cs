using ChemicalLaboratory.Domain.UserRepository;
using Domain;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
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
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddDbContext<DataBaseContext>(options => 
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IPeopleRepository, PeopleRepository>();
            builder.Services.AddScoped<IExperimentRepository, ExperimentRepository>();

            //builder.Services.AddScoped(typeof(BaseRepository<>));
            //builder.Services.AddScoped<BaseRepository, ReagentRepository>();
            //------------------------------------------------------------------------------------------------------------

            //builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //     .AddCookie(options =>
            //     {
            //         options.LoginPath = "/Authorisation/Index"; // Страница входа
            //         options.AccessDeniedPath = "/Home/Index"; // Страница доступа запрещена
            //         options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Срок действия cookie
            //     });

            /*
            builder.Services.AddRazorPages()
                 .AddRazorPagesOptions(options =>
                 {
                     // Укажите корневой маршрут
                     options.Conventions.AddPageRoute("/UI", "");
                 });

            builder.Services.AddRazorPages();
            */
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
