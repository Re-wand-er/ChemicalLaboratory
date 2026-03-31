using ChemicalLaboratory.Domain.Interfaces;
using ChemicalLaboratory.Application.UseCases.Services;
using ChemicalLaboratory.Infrastructure.Persistence;
using ChemicalLaboratory.Infrastructure.Persistence.Repositories;
using ChemicalLaboratory.Application.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Serilog.Events;
using Serilog;
using Mapster;
using ChemicalLaboratory.Infrastructure.Email;

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

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var key = builder.Configuration["SecretKey"];
                    // Валидация токена при котором в загаловке было бы Authorisation ...; Barear ...
                    // Удобно например для мобильного или пк фронтенда
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey =
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!))
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {

                            if (context.Request.Cookies.ContainsKey("jwtToken"))
                            {
                                context.Token = context.Request.Cookies["jwtToken"];
                            }

                            return Task.CompletedTask;
                        }
                    };
                });
            
            builder.Services.AddAuthorization();

            // Для сохранения поля с кодом на время
            builder.Services.AddDistributedMemoryCache();

            //------------------------------------------------------------------------------------------------------------

            var frontendIP = builder.Configuration["FrontendIP"];
            // 1. Добавляем политику CORS
            // Для получения запросов от другого адреса
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins(frontendIP!) // Адрес вашего фронтенда
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                    // Для разработки можно разрешить всё:
                    // policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });

            //------------------------------------------------------------------------------------------------------------

            builder.Services.AddControllers();

            //------------------------------------------------------------------------------------------------------------

            builder.Services.AddDbContext<DataBaseContext>(options => 
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IReagentRepository, ReagentRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
            builder.Services.AddScoped<INotificationRepository, NotificationRepository>();

            builder.Services.AddScoped<IJwtService, JwtService>();
            builder.Services.AddScoped<IPasswordHasher, Argon2PasswordHasher>();
            builder.Services.AddScoped<IEmailSender, EmailSender>();

            builder.Services.AddScoped<ReagentService>();
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<SupplierService>();
            builder.Services.AddScoped<NotificationService>();

            //------------------------------------------------------------------------------------------------------------

            builder.Services.AddMapster();

            //------------------------------------------------------------------------------------------------------------

            var app = builder.Build();

            app.UseRouting();

            app.UseCors("AllowFrontend"); // для CORS

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHttpsRedirection();

            //app.UseStaticFiles();

            app.MapControllers();
            
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            //app.Use(async (context, next) =>
            //{
            //    if (context.Request.Path == "/")
            //    {
            //        context.Response.Redirect("/index.html");
            //        return;
            //    }
            //    await next();
            //});

            app.Run();
        }
    }
}
