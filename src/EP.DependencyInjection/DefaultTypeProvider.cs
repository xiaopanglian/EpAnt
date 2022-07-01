using EP.DependencyInjection.Contracts;
using System.Reflection;

namespace EP.DependencyInjection
{
    public class DefaultTypeProvider : ITypeProvider
    {
        public IEnumerable<Type> GetTypes(IEnumerable<Assembly> assemblies)
        {
            return assemblies.SelectMany(c => c.GetTypes());
        }

        public IEnumerable<Type> GetImplementTypes(IEnumerable<Type> types, Type type)
        {
            return types.Where(c => type.IsAssignableFrom(c) && !c.IsAbstract && c.IsClass);
        }
    }
}