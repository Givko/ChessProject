using Chess.Core.DataAccess.Infrastructure.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Chess.GameEngine.DataAccess.Infrastructure.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddDataAccessDependencies(this IServiceCollection services, string connectionString)
        {
            return services.AddDataAccessDependencies<GameEngineDbContext>(connectionString);
        }
    }
}
