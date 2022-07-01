using System.Reflection;

namespace EP.DependencyInjection.Contracts
{
    public interface ITypeProvider
    {
        IEnumerable<Type> GetTypes(IEnumerable<Assembly> assemblies);

        IEnumerable<Type> GetImplementTypes(IEnumerable<Type> types, Type type);
    }
}