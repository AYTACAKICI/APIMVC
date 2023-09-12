using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MVCAPİ.Tekrar.DAL.AuthDAL.Concrate;
using MVCAPİ.Tekrar.DAL.AuthDAL.InterFaces;
using MVCAPİ.Tekrar.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCAPİ.Tekrar
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MVCAPİ.Tekrar", Version = "v1" });
            });
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IAuthDAL, AuthDAL>();
            services.AddScoped<ISepetDAL, SepetDAL>();
            services.AddDbContext<MyContext>(x => x.UseSqlServer("server = AYTACAKICI\\SQLEXPRESS; database = NORTHWND; uid = sa; pwd = 123 "));
            services.AddDbContext<AuthContext>(x => x.UseSqlServer("server = AYTACAKICI\\SQLEXPRESS; database = NORTHWND; uid = sa; pwd = 123 "));
            services.AddDbContext<SepetContext>(x => x.UseSqlServer("server = AYTACAKICI\\SQLEXPRESS; database = NORTHWND; uid = sa; pwd = 123 "));           
            services.AddAuthorization();            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                { ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection
                ("AppSettings:Token").Value)),
                    ValidateIssuer = false

                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MVCAPİ.Tekrar v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(a => {
                a.SwaggerEndpoint("/swagger/v1/swagger.json", "MVCAPİ.Tekrar v1");
                a.DocumentTitle = "MVCAPİ.Tekrar v1";
            });
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}
