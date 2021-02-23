using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using DataMapping.DataContext;
using DataMapping.Models;
using Infrastruture.Repository.Classes;
using Infrastruture.Repository.Interfaces;
using Infrastruture.UnitOfWork.Classes;
using Infrastruture.UnitOfWork.Interfaces;
using WebAPI.AppIocDi;
using WebAPI.Swagger;

namespace WebAPI
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
            //Inversion Of Control (IOC) & Dependency Injection (DI)
            services.RegisterRepositories();

            //DataBase
            services.AddControllersWithViews();
            services.AddDbContext<DataDbContext>
            (o => o.UseSqlServer(Configuration.
                GetConnectionString("EmployeeAppCon")));

            //Enable CORS
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin",
                    options => 
                        options.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            //Enable JSON Serialization
            JSONSerializer(services);

            //Configure Swagger launch page
            services.AddSwaggerGen(x => { x.SwaggerDoc("v1",new OpenApiInfo{Title = "WebAPI",Version = "v1"});});
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataDbContext db)
        {
            //Configure Swagger
            ConfigSwagger(app);

            app.UseCors(options => options.AllowAnyOrigin()
                .AllowAnyMethod().AllowAnyHeader());
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Create DataBase first approche
            //db.Database.EnsureCreated();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //for photos
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(),"Photos")),
                RequestPath = "/Photos"
            });
        }


        private void JSONSerializer(IServiceCollection services)
        {
            //To enable JSON Serialization
            services.AddControllersWithViews().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft
                    .Json.ReferenceLoopHandling.Ignore).AddNewtonsoftJson(options =>
                options.SerializerSettings.ContractResolver = new DefaultContractResolver());
        }
        //This method is used to Configure Swagger
        private void ConfigSwagger(IApplicationBuilder app)
        {
            var swaggerOptions = new SwaggerOptions();
            Configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);
            app.UseSwagger(option => { option.RouteTemplate = swaggerOptions.JsonRoute; });
            app.UseSwaggerUI(option => { option.SwaggerEndpoint(swaggerOptions.UIEndpoint, swaggerOptions.Description); });
        }
    }
}
