
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Dang.ModuleDi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterAllTypesEndingWithInAssembly(this IServiceCollection services, string typeNameEndsWith, Assembly assemblies,
            ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            var typesFromAssemblies = assemblies.DefinedTypes.Where(t => t.Name.EndsWith(typeNameEndsWith) && t.ImplementedInterfaces.Any()).ToList();

            foreach (var type in typesFromAssemblies)
            {
                services.Add(new ServiceDescriptor(type.ImplementedInterfaces.FirstOrDefault(), type, lifetime));
            }
        }
    }
}