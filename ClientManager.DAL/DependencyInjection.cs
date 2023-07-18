using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using ClientManager.DAL.Repository.Interfaces;
using ClientManager.DAL.Repository;

namespace ClientManager.DAL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection
            services, IConfiguration configuration)
        {

            var connectionString = configuration["DbConnection"];
            services.AddDbContext<ClientManagerDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IShareholderRepository, ShareholderRepository>();

            return services;
        }
    }
}
