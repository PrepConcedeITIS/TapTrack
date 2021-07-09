using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TapTrackAPI.Core.Base;
using TapTrackAPI.Core.Base.ValidationBase;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Features.Auth;
using TapTrackAPI.Core.Features.Auth.Services;
using TapTrackAPI.Core.Features.Issue;
using TapTrackAPI.Core.Interfaces;
using TapTrackAPI.Core.Services;
using TapTrackAPI.Data;
using TapTrackAPI.TelegramBot;
using TapTrackAPI.TelegramBot.Base;
using TapTrackAPI.TelegramBot.Commands.Start;
using TapTrackAPI.TelegramBot.Interfaces;
using TapTrackAPI.TelegramBot.Services;

namespace TapTrackAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            WebHostEnvironment = webHostEnvironment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment WebHostEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            AddDbContext(services);
            services.AddIdentityCore<User>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddSignInManager();
            services.AddControllers()
                .AddApplicationPart(typeof(AuthController).Assembly)
                .AddNewtonsoftJson();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "TapTrackAPI", Version = "v1"});
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Audience"],
                        IssuerSigningKey =
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SecretKey"])),
                        ClockSkew = TimeSpan.Zero
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(Policies.Admin, PoliciesExtensions.AdminPolicy());
                options.AddPolicy(Policies.User, PoliciesExtensions.UserPolicy());
            });
            services.AddHttpClient();
            services.AddAutoMapper(mc => { mc.AddMaps(typeof(AuthController).Assembly); });
            services.AddScoped<DbContext, AppDbContext>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<IImageUploadService, ImageUploadService>();
            services.AddScoped<IMailSender, MailSender>();

            services.AddIssueServices();

            RegisterMediaR(services);

            RegisterTelegramBot(services);

#if ANGULAR_WITH_API || RELEASE
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "taptrack/dist"; });
#endif
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseCors(builder =>
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TapTrackAPI v1"));
            }
            else
            {
                app.UseCors(builder =>
                    builder.WithOrigins("https://www.taptrack.tech").AllowAnyHeader().AllowAnyMethod());
            }

            app.UseMiddleware<ValidationExceptionMiddleware>();
            app.UseHttpsRedirection();

            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

#if ANGULAR_WITH_API || RELEASE
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "taptrack";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
#endif
        }

        private void AddDbContext(IServiceCollection services)
        {
            if (!WebHostEnvironment.IsDevelopment())
            {
                var connectionUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

                var databaseUri = new Uri(connectionUrl!);

                var db = databaseUri.LocalPath.TrimStart('/');
                var userInfo = databaseUri.UserInfo.Split(':', StringSplitOptions.RemoveEmptyEntries);

                var connection =
                    $"User ID={userInfo[0]};Password={userInfo[1]};Host={databaseUri.Host};Port={databaseUri.Port};Database={db};Pooling=true;SSL Mode=Require;Trust Server Certificate=True;";
                services.AddDbContext<AppDbContext>(builder => builder.UseNpgsql(connection));
            }
            else
            {
                services.AddDbContext<AppDbContext>(builder => builder
                    .UseNpgsql(Configuration.GetConnectionString("PostgresRemote")));
            }
        }

        private void RegisterMediaR(IServiceCollection services)
        {
            services.AddMediatR(typeof(AuthController).Assembly, typeof(BindUserAsyncHandler).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddValidatorsFromAssemblies(new[]
                {typeof(AuthController).Assembly, typeof(BindUserAsyncHandler).Assembly});
        }

        private static void RegisterTelegramBot(IServiceCollection services)
        {
            services.AddSingleton<IChatService, TelegramService>();
            services.AddBotCommands(typeof(IBotRequest).Assembly);
            services.AddHostedService<Bot>();
            services.AddScoped<INotificationService, NotificationService>();
        }
    }
}