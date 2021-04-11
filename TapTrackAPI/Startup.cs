using System;
using System.Text;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
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
using TapTrackAPI.Core.Features.Project;
using TapTrackAPI.Core.Interfaces;
using TapTrackAPI.Core.Services;
using TapTrackAPI.Data;

namespace TapTrackAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        { 
            //services.AddCors();
            services.AddDbContext<AppDbContext>(builder => builder
                .UseNpgsql(Configuration.GetConnectionString("PostgresRemote")));
            services.AddIdentityCore<User>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddSignInManager();

            services.AddControllers()
                .AddApplicationPart(typeof(AuthController).Assembly)
                .AddNewtonsoftJson();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "TapTrackAPI", Version = "v1"});
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

            services.AddAutoMapper(mc =>
            {
                mc.AddMaps(typeof(AuthController).Assembly);
            });
            
            RegisterMediaR(services);
            
            services.AddScoped<DbContext, AppDbContext>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<IImageUploadService, ImageUploadService>();
            services.AddScoped<IMailSender, MailSender>();

            services.RegisterProject();
            services.RegisterIssue();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            #warning
            //app.UseCors(builder => builder.WithOrigins("paste url from prod front").AllowAnyHeader().AllowAnyMethod());
            
            if (env.IsDevelopment())
            {
                app.UseCors(builder =>
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TapTrackAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        private void RegisterMediaR(IServiceCollection services)
        {
            services.AddMediatR(typeof(AuthController).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddValidatorsFromAssemblies(new []{typeof(AuthController).Assembly});
        }
    }
}