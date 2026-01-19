using Serilog;
using Serilog.Events;
using ChemicalLaboratory.Domain.Interfaces;
using ChemicalLaboratory.Application.UseCases.Services;
using ChemicalLaboratory.Infrastructure.Persistence;
using ChemicalLaboratory.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ChemicalLaboratory
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Error)
                .Enrich.FromLogContext()
                .WriteTo.File
                (
                    path: "log/log.log",
                    fileSizeLimitBytes: 5_000_000,
                    rollOnFileSizeLimit: true,
                    shared: true, 
                    outputTemplate: " {Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{SourceContext}{Exception}"
                )
                .CreateLogger();
            builder.Host.UseSerilog();

            //------------------------------------------------------------------------------------------------------------
            builder.Services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey =
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SUPER_SECRET_UNBEATABLE_KEY"))
                    };
                });

            builder.Services.AddAuthorization();


            //------------------------------------------------------------------------------------------------------------
            //builder.Services.AddControllersWithViews()
            //    .AddRazorOptions(options =>
            //    {
            //        options.ViewLocationFormats.Clear();
            //        options.ViewLocationFormats.Add("/WebUI/Views/{1}/{0}.cshtml");
            //        options.ViewLocationFormats.Add("/WebUI/Views/Home/{0}.cshtml");
            //        options.ViewLocationFormats.Add("/WebUI/Views/Shared/{0}.cshtml");
            //    });
            builder.Services.AddControllers();
            //------------------------------------------------------------------------------------------------------------
            builder.Services.AddDbContext<DataBaseContext>(options => 
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IReagentRepository, ReagentRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
            builder.Services.AddScoped<INotificationRepository, NotificationRepository>();

            builder.Services.AddScoped<ReagentService>();
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<SupplierService>();
            builder.Services.AddScoped<NotificationService>();

            //------------------------------------------------------------------------------------------------------------
            //builder.Services.AddHttpClient("ApiClient", client =>
            //{
            //    client.BaseAddress = new Uri("https://localhost:7248/");
            //});

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

            app.Use(async (context, next) =>
            {
                if (context.Request.Path == "/")
                {
                    context.Response.Redirect("/index.html");
                    return;
                }
                await next();
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            //app.MapControllerRoute(
            //    name: "default",
            //    pattern: "{controller=Home}/{action=Index}/{id?}"
            //    );

            app.Run();
        }
    }
}
