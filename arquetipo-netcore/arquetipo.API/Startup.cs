using arquetipo.Domain.Interfaces;
using arquetipo.Infrastructure.Services;
using arquetipo.Repository.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
//using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace arquetipo.API
{
    public static class Startup
    {
        public static WebApplication InizializarApp(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder);
            var app = builder.Build();
            Configure(app);
            return app;
        }

        private static void ConfigureServices(WebApplicationBuilder builder)
        {
            //var key = Encoding.ASCII.GetBytes(Configuration.GetValue<string>("SecretKey"));
            var key = Encoding.ASCII.GetBytes("asdfghjkl@.9133alñjas.dsf.r.33");

            builder.Services.AddAuthentication(x => {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x => {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });


            builder.Services.AddDbContext<BlogContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IPost, PostImplementacion>();
            builder.Services.AddScoped<ICliente, ClienteImplementacion>();
            builder.Services.AddScoped<IPatioAuto, PatioAutoImplementacion>();
            builder.Services.AddScoped<IEjecutivo, EjecutivoImplementacion>();
            builder.Services.AddScoped<IMarca, MarcaImplementacion>();
            builder.Services.AddScoped<IAsignaCliente, AsignaClienteImplementacion>();
            builder.Services.AddScoped<IAutomovil, AutomovilImplementacion>();
            builder.Services.AddScoped<ISolicitudCredito, SolicitudCreditoImplementacion>();

            
                        
        }
        private static void Configure(WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();
            

            app.MapControllers();

           // app.Run();
        }
    }
}
