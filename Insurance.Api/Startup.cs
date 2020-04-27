using AutoMapper;
using Insurance.Data.EFCore;
using Insurance.Data.EFCore.Repositories;
using Insurance.Data.Interfaces;
using Insurance.Models.Content;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Insurance.Contents.Api
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
            services.AddMvc();
            services.AddControllers();
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.WithOrigins(Configuration["FrontEnd"]).AllowAnyHeader().AllowAnyMethod());
            });

            services.AddAutoMapper(typeof(Startup));
            services.AddDbContext<InsuranceDBContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("InsuranceDBContext")));

            services.AddScoped<IRepository<Item>, ItemRepository>();
            services.AddScoped<IRepository<Category>, CategoryRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<InsuranceDBContext>();
                context.SeedData();
            }

            app.UseCors(options => options.WithOrigins(Configuration["FrontEnd"]).AllowAnyHeader().AllowAnyMethod());
            app.UseRouting();
            //app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
