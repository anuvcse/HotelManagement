using HotelManagementRepository;
using HotelManagementRepository.Implementation;
using HotelManagementRepository.Interface;
using HotelManagementService.Implementation;
using HotelManagementService.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementAPI
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
            services.AddTransient<IBookingDetailsRepository, BookingDetailsRepository>();
            services.AddTransient<IRoomsRepository, RoomsRepository>();
            services.AddTransient<ICustomerDetailsRepository, CustomerDetailsRepository>();
            services.AddTransient<IOwnerService, OwnerService>();
            services.AddTransient<IUserService, UserService>();
            services.AddDbContext<ApplicationDbContext>(
             options => options.UseSqlServer(
                        Configuration["ConnectionStrings:DefaultConnection"]));
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(options =>
           {
               
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["Secret"].ToString())),
                   ValidateIssuer = false,
                   ValidateAudience = false,
                   // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                   ClockSkew = TimeSpan.Zero
               };

               options.Events = new JwtBearerEvents
               {
                   OnTokenValidated = context =>
                   {
                       var accessToken = (JwtSecurityToken)context.SecurityToken;
                       if (accessToken == null)
                       {
                           return Task.CompletedTask;
                       }

                       //Debug what this access token has got
                       //See the possibility of validating token here as well.
                       var identity = (ClaimsIdentity)context.Principal.Identity;
                       identity?.AddClaim(new Claim("access_token", accessToken.RawData));

                       return Task.CompletedTask;
                   },
                   OnAuthenticationFailed = context =>
                   {
                       context.Response.Clear();
                       context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                       context.Response.ContentType = "application/json; charset=utf-8";
                       const string message = "Authentication Failed";
                       context.Response.WriteAsync(message);
                       throw new SecurityTokenValidationException(
                           "Invalid security token received from identity provider");
                   }
               };
           });
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

            app.UseCors(x => x
               .AllowAnyMethod()
               .AllowAnyHeader()
               .SetIsOriginAllowed(origin => true)
               .AllowCredentials());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                context.Database.Migrate();
            }
        }
    }
}
