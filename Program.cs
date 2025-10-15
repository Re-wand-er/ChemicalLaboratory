using EFCore;
using ChemicalLaboratory.Domain;
using ChemicalLaboratory.Domain.ORM;
using ChemicalLaboratory.Domain.UserServices;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using EFCore.Services;


namespace ChemicalLaboratory
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<IUserService, UserService>();

//------------------------------------------------------------------------------------------------------------
            //builder.Services.AddDbContext<ORMSQLCommand>(options =>
            //    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddDbContext<DataBaseContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IPeopleService, PeopleService>();

            //builder.Services.AddScoped(typeof(BaseRepository<>));
            //builder.Services.AddScoped<BaseRepository, ReagentRepository>();
//------------------------------------------------------------------------------------------------------------

            builder.Services.AddRazorPages(options =>
            {
                // отключаем глобально Antiforgery-токен
                options.Conventions.ConfigureFilter(new IgnoreAntiforgeryTokenAttribute());
            });

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                 .AddCookie(options =>
                 {
                     options.LoginPath = "/Home/Authorisation"; // Страница входа
                     options.AccessDeniedPath = "/Index"; // Страница доступа запрещена
                     options.Cookie.Name = "User"; //Название cookie
                     options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Срок действия cookie
                     options.SlidingExpiration = true; // Обновление срока действия при активности
                 });
            //builder.Services.AddRazorPages()
            //            .AddRazorPagesOptions(options =>
            //            {
            //                // Укажите корневой маршрут
            //                options.Conventions.AddPageRoute("/Home/Authorisation", "");
            //            });

            builder.Services.AddRazorPages();
           
           
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
