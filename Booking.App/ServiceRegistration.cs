using Booking.App.Command.Booking.Interface;
using Booking.App.Command.Booking.Repo;
using System.Text;
using Booking.App.Command.User.Interface;
using Booking.App.Command.User.Repo;
using Booking.BAL.Helper;
using Booking.BAL.Service.Interface;
using Booking.BAL.Service.Repo;
using Booking.BAL.Translator.Interface;
using Booking.BAL.Translator.Repo;
using Booking.DAL.Context;
using Booking.DAL.Repository.Interface;
using Booking.DAL.Repository.Repo;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Booking.App
{
    public static class ServiceRegistration
    {
        public static IServiceCollection ApplicationServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
            // Register DbContext
            if (!env.IsEnvironment("Testing"))
            {
                string connectionString = configuration.GetConnectionString("DefaultConnection");
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(connectionString));
            }

            // JWT Authentication Configuration
            //var secretKey = Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]);
            //services.AddAuthentication()
            //.AddJwtBearer(options =>
            //{
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = new SymmetricSecurityKey(secretKey),
            //        ValidateIssuer = true,
            //        ValidIssuer = configuration["Jwt:Issuer"],
            //        ValidateAudience = true,
            //        ValidAudience = configuration["Jwt:Audience"],
            //        ValidateLifetime = true,
            //        ClockSkew = TimeSpan.Zero
            //    };
            //});

            services.AddSingleton<JwtHelper>();
            services.AddMemoryCache();
            //services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
            //services.AddAuthorization();

            services.AddScoped<IPasswordHasher<string>, PasswordHasher<string>>();

            services.AddScoped<IUserProfileService, UserProfileService>();
            services.AddScoped<IBookingService, BookingService>();

            services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();

            services.AddScoped<IUserTranslator, UserTranslator>();
            services.AddScoped<IBookingTranslator, BookingTranslator>();

            services.AddScoped<ICreateUserCommand, CreateUserCommand>();
            services.AddScoped<IUserLoginCommand, UserLoginCommand>();

            services.AddScoped<ICreateBookingCommand, CreateBookingCommand>();
            services.AddScoped<IGetBookingCommand, GetBookingCommand>();
            services.AddScoped<IGetRoomAvailabilityCommand, GetRoomAvailabilityCommand>();
            services.AddScoped<ICancelBookingCommand, CancelBookingCommand>();

            services.AddScoped<ClaimsHelper>();

            return services;
        }    
    }
}
