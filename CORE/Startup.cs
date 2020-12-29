using AutoMapper;
using BUSINESS_LOGIC;
using BUSINESS_LOGIC.Interfaces;
using BUSINESS_LOGIC.Services;
using DAL;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Z.EntityFramework.Extensions;

namespace CORE
{
    public class Startup
    {  
        
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
      

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Register DbContext
            services.AddDbContext<ExcelImportDbContext>(options => options.UseSqlServer(
                Configuration.GetConnectionString("Default"), x =>
                {
                    x.MigrationsAssembly("DAL");
                }));
       

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient(typeof(IBase<>), typeof(BaseService<>));
            services.AddTransient<IProduct, ProductService>();
            services.AddTransient<IColor, ColorService>();
            services.AddTransient<IArticle, ArticleService>();
            services.AddTransient<IJson, JsonImportService>(); 
            services.AddTransient<IReadData, DataService>();

            services.AddSingleton(_ => Configuration);


            //Register Swagger
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "File Upload API", Version = "v1" });
            });

            services.AddControllers();

            EntityFrameworkManager.ContextFactory = context =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<ExcelImportDbContext>();
                optionsBuilder.UseSqlServer(Configuration.GetConnectionString("Default"));
                return new ExcelImportDbContext(optionsBuilder.Options);
            };

            services.AddAutoMapper(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ExcelImport API V1");
            });
        }
    }
}
