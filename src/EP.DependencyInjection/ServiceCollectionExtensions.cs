
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDefaultDependencyInjection(this IServiceCollection services)
        {
            return services.AddDefaultDependencyInjection(AppDomain.CurrentDomain.GetAssemblies());
        }

        public static IServiceCollection AddDefaultDependencyInjection(this IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            services.AddSingleton<ITypeProvider, DefaultTypeProvider>();
            services.AddSingleton<IServiceDescriptor, DefaultServiceDescriptor>();

            var typeProvider = services.GetInstance<ITypeProvider>();
            var serviceDescriptor = services.GetInstance<IServiceDescriptor>();

            var types = typeProvider.GetTypes(assemblies);

            var implementTypes = typeProvider.GetImplementTypes(types, typeof(IScopedDependency))
                .Concat(typeProvider.GetImplementTypes(types, typeof(ISingletonDependency)))
                .Concat(typeProvider.GetImplementTypes(types, typeof(ITrasintDependency)));

            var serviceDescriptors = serviceDescriptor.GetServiceDescriptors(implementTypes, typeof(IScopedDependency), ServiceLifetime.Scoped)
                .Concat(serviceDescriptor.GetServiceDescriptors(implementTypes, typeof(ISingletonDependency), ServiceLifetime.Singleton))
                .Concat(serviceDescriptor.GetServiceDescriptors(implementTypes, typeof(ITrasintDependency), ServiceLifetime.Transient));

            foreach (var item in serviceDescriptors)
            {
                services.Add(item);
            }

            return services;
        }

        private static T GetInstance<T>(this IServiceCollection services) where T : class
        {
            return services.BuildServiceProvider().GetRequiredService<T>();
        }
    }
}