using Microsoft.Extensions.DependencyInjection;

namespace Core.Utilities.IoC
{
    public interface ICoreModule
    {
        public void Load(IServiceCollection serviceCollection);
    }
}