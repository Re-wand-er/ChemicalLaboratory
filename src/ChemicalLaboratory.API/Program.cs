using ChemicalLaboratory.Application.UseCases.Services;
using ChemicalLaboratory.Infrastructure.Persistence;
using ChemicalLaboratory.Infrastructure.Email;
using ChemicalLaboratory.Infrastructure;
using ChemicalLaboratory.Application.Interfaces;
using ChemicalLaboratory.Application.Mapping;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog.Events;
using System.Text;
using Serilog;
using Mapster;

namespace ChemicalLaboratory
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Считать любое время как UTC - костыль
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            // Нужно убрать

            var builder = WebApplication.CreateBuilder(args);

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Error)
                .Enrich.FromLogContext()
                .WriteTo.Console(outputTemplate: " {Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{SourceContext}{Exception}")
                // .WriteTo.File
                // (
                //     path: "log/log.log",
                //     fileSizeLimitBytes: 5_000_000,
                //     rollOnFileSizeLimit: true,
                //     shared: true, 
                //     outputTemplate: " {Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{SourceContext}{Exception}"
                // )
                .CreateLogger();
            builder.Host.UseSerilog();

            //------------------------------------------------------------------------------------------------------------
            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;
            //------------------------------------------------------------------------------------------------------------

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var key = builder.Configuration["SecretKey"];
                    // ��������� ������ ��� ������� � ��������� ���� �� Authorisation ...; Barear ...
                    // ������ �������� ��� ���������� ��� �� ���������
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey =
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!))
                    };

                    // options.Events = new JwtBearerEvents
                    // {
                    //     OnMessageReceived = context =>
                    //     {
                    //         if (context.Request.Cookies.ContainsKey("jwtToken"))
                    //         {
                    //             context.Token = context.Request.Cookies["jwtToken"];
                    //         }

                    //         return Task.CompletedTask;
                    //     }
                    // };
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var token = context.Request.Cookies["jwtToken"];
                    
                            if (!string.IsNullOrEmpty(token))
                            {
                                context.Token = token;
                    
                                Console.WriteLine(
                                    $"JWT FOUND | {context.Request.Method} {context.Request.Path}"
                                );
                            }
                            else
                            {
                                Console.WriteLine(
                                    $"JWT NOT FOUND | {context.Request.Method} {context.Request.Path}"
                                );
                            }
                    
                            return Task.CompletedTask;
                        },
                    
                        OnAuthenticationFailed = context =>
                        {
                            Console.WriteLine(
                                $"JWT AUTH FAILED | {context.Request.Path} | " +
                                $"{context.Exception.GetType().Name} | " +
                                $"{context.Exception.Message}"
                            );
                    
                            return Task.CompletedTask;
                        },
                    
                        OnChallenge = context =>
                        {
                            Console.WriteLine(
                                $"JWT CHALLENGE | {context.Request.Path}"
                            );
                    
                            return Task.CompletedTask;
                        }
                    };
                });
            
            builder.Services.AddAuthorization();

            // ��� ���������� ���� � ����� �� �����
            builder.Services.AddDistributedMemoryCache();

            //------------------------------------------------------------------------------------------------------------

            var frontendIP = builder.Configuration["FrontendIP"];
            // 1. ��������� �������� CORS
            // ��� ��������� �������� �� ������� ������
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins(frontendIP!) // ����� ������ ���������
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                    // ��� ���������� ����� ��������� ��:
                    // policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });

            //------------------------------------------------------------------------------------------------------------

            builder.Services.AddControllers();
            builder.Services.AddHttpContextAccessor();

            //------------------------------------------------------------------------------------------------------------

            builder.Services.AddDbContext<DataBaseContext>(options => 
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
                       .UseSnakeCaseNamingConvention());

            builder.Services.AddScopedRepository();

            builder.Services.AddScoped<IJwtService, JwtService>();
            builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
            builder.Services.AddScoped<IPasswordHasher, Argon2PasswordHasher>();
            builder.Services.AddScoped<IEmailSender, EmailSender>();

            builder.Services.AddScoped<ReagentService>();
            builder.Services.AddScoped<ReagentForecastService>();
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<SupplierService>();
            builder.Services.AddScoped<NotificationService>();
            
            builder.Services.AddScoped<GeneratePDFService>();
            builder.Services.AddScoped<QrDecoderService>();

            //------------------------------------------------------------------------------------------------------------

            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(typeof(MappingRegister).Assembly);
            builder.Services.AddSingleton(config);
            builder.Services.AddMapster();

            //------------------------------------------------------------------------------------------------------------

            var app = builder.Build();

            app.UseRouting();

            app.UseCors("AllowFrontend"); // ��� CORS

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
