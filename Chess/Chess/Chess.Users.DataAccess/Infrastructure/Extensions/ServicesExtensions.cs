using Chess.Core.DataAccess.Infrastructure.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Chess.Users.DataAccess.Infrastructure.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddDataAccessDependencies(this IServiceCollection services, string connectionString)
        {
            return services.AddDataAccessDependencies<UsersDbContext>(connectionString);
        }
    }
}
