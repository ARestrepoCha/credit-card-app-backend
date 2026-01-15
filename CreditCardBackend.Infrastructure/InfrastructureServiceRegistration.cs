using CreditCardBackend.Application.Common.Interfaces.Authentication;
using CreditCardBackend.Application.Common.Interfaces.Security;
using CreditCardBackend.Domain.Entities;
using CreditCardBackend.Domain.Interfaces.IGeneric;
using CreditCardBackend.Domain.Interfaces.Repositories;
using CreditCardBackend.Infrastructure.Authentication;
using CreditCardBackend.Infrastructure.Persistence;
using CreditCardBackend.Infrastructure.Repositories;
using CreditCardBackend.Infrastructure.Repositories.Generic;
using CreditCardBackend.Infrastructure.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CreditCardBackend.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddHttpContextAccessor();

            services.AddIdentityCore<User>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 8;
            })
             .AddEntityFrameworkStores<AppDbContext>()
             .AddDefaultTokenProviders();

            // Services
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Register infrastructure services here
            services.AddSingleton<IEncryptionService, EncryptionService>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

            // Repositories
            services.AddScoped<ICreditCardRepository, CreditCardRepository>();

            return services;
        }
    }
}
