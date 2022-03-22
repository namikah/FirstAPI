using BackendProject.Data;
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
using MyFirst.AuthenticationService;
using MyFirst.AuthenticationService.Contracts;
using MyFirst.AuthenticationService.Models;
using MyFirst.Infrastructure.Middlewares;
using MyFirst.Models.Entities;
using MyFirst.Repository.DataContext;
using MyFirst.Repository.Repository;
using MyFirst.Repository.Repository.Contracts;
using MyFirst.Services.Mapping;
using MyFirst.Services.Services;
using MyFirst.Services.Services.Contracts;
using MyFirst.WebAPI.Data.Extensions;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace MyFirst.WebAPI
{
    public class Startup
    {
        private readonly IWebHostEnvironment _environment;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            _environment = environment;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<JwtSetting>(Configuration.GetSection("JWT"));

            var connectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionString, builder =>
                {
                    builder.MigrationsAssembly("MyFirst.Repository");
                });
            });
            services.AddAutoMapper(typeof(MapperProfile));

            services.AddScoped(typeof(IRepository<>), typeof(EFCoreRepository<>));
            //services.AddScoped(typeof(IRepository<>), typeof(JsonRepository<>));
            services.AddScoped<IStudentService, StudentService>();

            //services.AddScoped(typeof(IUserRepository<>), typeof(UserRepository<>));
            services.AddScoped(typeof(IUserService), typeof(UserService));
            services.AddScoped<IAuthService, AuthService>();

            //services.AddAllTypes<IStudentService>(new[] { typeof(StudentService).GetTypeInfo().Assembly });
            //services.AddAllGenericTypes(typeof(IRepository<>), new[] { typeof(EFCoreRepository<>).GetTypeInfo().Assembly });
            //services.AddAllTypes<IUserService>(new[] { typeof(UserService).GetTypeInfo().Assembly });
            //services.AddAllGenericTypes(typeof(IUserRepository<>), new[] { typeof(UserRepository<>).GetTypeInfo().Assembly });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(o =>
           {
               o.RequireHttpsMetadata = false;
               o.SaveToken = false;
               o.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidateLifetime = true,
                   ClockSkew = TimeSpan.Zero,
                   ValidIssuer = Configuration["JWT:Issuer"],
                   ValidAudience = Configuration["JWT:Audience"],
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Key"]))
               };
           });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPI", Version = "v1" });
            });

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;

                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);

                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;

            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            Constants.SeedDataPath = Path.Combine(_environment.ContentRootPath,"Data", "SeedData");
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1"));
            }

            app.ConfigureExceptionHandler();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
