using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Dang.ModuleDi.PackageModules
{
    public abstract class BasePackageModule
    {
        public abstract Assembly CurrentAssembly { get; }

        public abstract void AddDependencies(IServiceCollection serviceCollection);
    }
}
