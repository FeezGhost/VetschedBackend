using System;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Vetsched.Data.DBContexts;

namespace Loader.infrastructure.Extensions
{

    public static class ServiceExtensions
    {
     
        public static void ConfigureDBContext(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<VetschedContext>(options => options.UseNpgsql(
                Configuration.GetConnectionString("DefaultConnection"),
                npgsqlOptionsAction: sqlOptions =>
                {
                    //sqlOptions.MigrationsAssembly("infrastructure");
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 10,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorCodesToAdd: null);
                        //sqlOptions.UseNetTopologySuite();
                }), ServiceLifetime.Scoped);
          

            // Add a DbContext to store your Database Keys
            //services.AddDbContext<DataProtectionKeyContext>(options => options.UseNpgsql(
            //    Configuration.GetConnectionString("DefaultConnection"),
            //    npgsqlOptionsAction: sqlOptions =>
            //    {
            //        sqlOptions.MigrationsAssembly("infrastructure");
            //        sqlOptions.EnableRetryOnFailure(
            //            maxRetryCount: 10,
            //            maxRetryDelay: TimeSpan.FromSeconds(30),
            //            errorCodesToAdd: null);
            //        sqlOptions.UseNetTopologySuite();
            //    }), ServiceLifetime.Scoped);

        }

        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            //Adding Jwt Bearer
           .AddJwtBearer(options =>
           {
               options.SaveToken = true;
               options.RequireHttpsMetadata = false;
               options.TokenValidationParameters = new TokenValidationParameters()
               {
                   ValidateIssuer = true,
                   ValidateAudience = false,
                   ValidAudience = configuration["JWT:ValidAudience"],
                   ValidIssuer = configuration["JWT:ValidIssuer"],
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
               };
           });
        }
    }
}
