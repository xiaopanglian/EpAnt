using Microsoft.Extensions.DependencyInjection;

namespace EpAnt.DependencyInjection
{
    public class DefaultServiceDescriptor : Contracts.IServiceDescriptor
    {
        public IEnumerable<ServiceDescriptor> GetServiceDescriptors(IEnumerable<Type> types, Type type, ServiceLifetime serviceLifetime)
        {
            return types.Select(c => new ServiceDescriptor(type, c, serviceLifetime));
        }
    }
}