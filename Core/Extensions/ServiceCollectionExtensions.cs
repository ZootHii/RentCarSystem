using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDependencyResolvers(this IServiceCollection serviceCollection, params ICoreModule[] coreModules)
        {
            foreach (var coreModule in coreModules)
            {
                coreModule.Load(serviceCollection);
            }
            
            ServiceTool.CreateServiceProvider(serviceCollection);
        }
    }
}