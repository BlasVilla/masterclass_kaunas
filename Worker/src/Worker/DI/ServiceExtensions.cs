using Microsoft.Extensions.DependencyInjection;
using Worker.Calculations;
using Worker.Messaging;

namespace Worker.DI
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddAllDependencies(this IServiceCollection services)
        {
            return services
                .AddSingleton<IMessageService, MessageService>()
                .AddSingleton<ICalculationService, SquareRootCalculationService>();
        }
    }
}