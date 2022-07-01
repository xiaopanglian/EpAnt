using Microsoft.Extensions.DependencyInjection;

namespace EpAnt.DependencyInjection.Contracts
{
    public interface IServiceDescriptor
    {
        IEnumerable<ServiceDescriptor> GetServiceDescriptors(IEnumerable<Type> types, Type type, ServiceLifetime serviceLifetime);
    }
}