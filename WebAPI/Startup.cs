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
using System.Text;
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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using WebAPI.AppIocDi;
using WebAPI.Jwt;
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

            //Jwt
            JwtSettings jwtSettings = new JwtSettings();
            Configuration.Bind(nameof(jwtSettings), jwtSettings);
            services.AddSingleton(jwtSettings);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                
            }).AddJwtBearer(x =>
            {
                x.SaveToken = true;
                x.TokenValidationParameters=new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = false,
                    ValidateLifetime = true

                };
            });

            //Configure Swagger launch page
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1",new OpenApiInfo{Title = "WebAPI",Version = "v1"});
                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[0] }
                };

                x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "JWT Authorization header using the bearer scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                x.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Id = "Bearer",
                                    Type = ReferenceType.SecurityScheme
                                }
                            }, 
                        new List<string>()
                    }
                });
            });

            //Use Identity Server with separate project
            //IdentityServer(services);

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

            //Use Identity Server with separate project
            app.UseAuthentication();

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

        #region Private method
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

        //Use Identity Server with separate project
        private void IdentityServer(IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.Authority = "https://localhost:44308";
                o.Audience = "myresourceapi";
                o.RequireHttpsMetadata = false;
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("PublicSecure", policy => policy.RequireClaim("client_id", "secret_client_id"));
            });
        }
        #endregion

    }
}
