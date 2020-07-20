using Chess.Core.DataAccess.Wrappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Chess.Core.DataAccess.Infrastructure.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddDataAccessDependencies<TDbContext>(this IServiceCollection services, string connectionString)
            where TDbContext : DbContext
        {
            #region Scoped registrations
            services
                .AddScoped<IActivatorWrapper, ActivatorWrapper>()
                .AddScoped<IUnitOfWork, UnitOfWork<TDbContext>>();
            #endregion

            services.AddDbContext<TDbContext>(options
                => options.UseSqlServer(connectionString));

            return services;
        }
    }
}
