using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Dang.ModuleDi.PackageModules
{
    public static class RegisterModules
    {
        private static IList<Type> Modules { get; set; } = new List<Type>();

        public static void AddModule(Type packageModule)
        {
            if (packageModule == null)
            {
                throw new ArgumentNullException(nameof(packageModule));
            }

            ValidateModuleType(packageModule);

            Modules.Add(packageModule);
        }


        public static void AddModules(Type[] packageModules)
        {
            if (packageModules == null || !packageModules.Any())
            {
                throw new ArgumentNullException(nameof(packageModules));
            }

            foreach (var packageModule in packageModules)
            {
                ValidateModuleType(packageModule);

                Modules.Add(packageModule);
            }
        }

        public static void ClearModules()
        {
            Modules.Clear();
        }

        public static void RegisterTypes(IServiceCollection serviceCollection)
        {
            if (serviceCollection == null)
            {
                throw new ArgumentNullException(nameof(serviceCollection));
            }

            if (!Modules.Any())
            {
                throw new ArgumentNullException(nameof(serviceCollection), "You need add modules to the Type collection of Package Modules before calling RegisterTypes");
            }

            foreach (var module in Modules)
            {
                var instanceModule = Activator.CreateInstance(module) as BasePackageModule;
                instanceModule?.AddDependencies(serviceCollection);
            }
        }

        private static void ValidateModuleType(Type packageModule)
        {
            if (packageModule.BaseType != typeof(BasePackageModule))
            {
                throw new ArgumentNullException(nameof(packageModule),
                    "The Package Module supplied must implement the BasePackageModule.cs base class");
            }
        }
    }
}
