using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

/*CUSTOM*/
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;




namespace shipping_instruction_management
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
            services.AddControllers().AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null);


            services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin()
                                                            .AllowAnyMethod()
                                                             .AllowAnyHeader()));

            //* DBContext
            services.AddDbContext<WebAPI.Models.APIContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            );

            //* Services
            services.AddScoped<WebAPI.Services.UsuarioService, WebAPI.Services.UsuarioService>();
            services.AddScoped<WebAPI.Services.ClienteService, WebAPI.Services.ClienteService>();
            services.AddScoped<WebAPI.Services.AgenteService, WebAPI.Services.AgenteService>();
            services.AddScoped<WebAPI.Services.ConsignatarioService, WebAPI.Services.ConsignatarioService>();
            services.AddScoped<WebAPI.Services.ShipperService, WebAPI.Services.ShipperService>();
            services.AddScoped<WebAPI.Services.CarrierService, WebAPI.Services.CarrierService>();
            services.AddScoped<WebAPI.Services.ServicioService, WebAPI.Services.ServicioService>();
            services.AddScoped<WebAPI.Services.ShippingInstructionService, WebAPI.Services.ShippingInstructionService>();
            services.AddScoped<WebAPI.Services.SearcherService, WebAPI.Services.SearcherService>();





            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.SaveToken = true;
                x.RequireHttpsMetadata = false;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = "http://dotnetdetail.net",
                    ValidIssuer = "http://dotnetdetail.net",
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes("GeorgeSun2020*BlaBla#BlaBla@2020")),
                };
            });

            //POLICY APPLIED TO ALL CONTROLLERS
            services.AddMvc(options =>
                    {
                        var policy = new AuthorizationPolicyBuilder()
                            .RequireAuthenticatedUser()
                            .Build();
                        options.Filters.Add(new AuthorizeFilter(policy));
                    });




        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseHttpsRedirection();
            app.UseCors("AllowAll");

            app.UseRouting();
            app.UseAuthentication(); // *


            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
