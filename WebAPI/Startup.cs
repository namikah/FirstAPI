using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MyFirst.WebAPI.Data.Extensions;
using Repository.DataContext;
using Repository.Mapping;
using Repository.Repository;
using Repository.Repository.Contracts;
using Services.Contracts;
using Services.Services;
using System.Reflection;

namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionString, builder =>
                {
                    builder.MigrationsAssembly("MyFirst.Repository");
                });
            });
            services.AddAutoMapper(typeof(MapperProfile));


            //services.AddScoped(typeof(IRepository<>), typeof(EFCoreRepository<>));
            //services.AddScoped(typeof(IRepository<>), typeof(JsonRepository<>));
            //services.AddScoped<IStudentService, StudentService>();

            services.AddAllTypes<IStudentService>(new[] { typeof(StudentService).GetTypeInfo().Assembly });
            services.AddAllGenericTypes(typeof(IRepository<>), new[] { typeof(EFCoreRepository<>).GetTypeInfo().Assembly });


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPI", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
