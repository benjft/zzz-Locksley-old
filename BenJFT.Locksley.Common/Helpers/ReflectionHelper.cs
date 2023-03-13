using System.Reflection;

namespace BenJFT.Locksley.Common.Helpers;

public static class ReflectionHelper {
    public static IEnumerable<Type> GetSubTypesOf<TBase>(this Assembly assembly) {
        return assembly.GetTypes().Where(type => typeof(TBase).IsAssignableFrom(type) && type != typeof(TBase));
    }

    public static IEnumerable<Type> GetSubClassesOf<TBase>(this Assembly assembly) {
        return assembly.GetSubTypesOf<TBase>().Where(type => type is {IsClass: true, IsAbstract: false});
    }

    public static IEnumerable<Type> BaseTypes(this Type type) {
        var curType = type;
        IEnumerable<Type> baseTypes = new List<Type>();

        while (curType != null) {
            yield return curType;

            var baseType = curType.BaseType;
            if (baseType is {IsInterface: false})
                baseTypes = baseTypes.Append(baseType);

            baseTypes = baseTypes.Concat(curType.GetInterfaces());

            (curType, baseTypes) = baseTypes.Pop();
        }
    }

    public static TAttribute? GetCustomAttributeFromAncestor<TAttribute>(this Type type) where TAttribute : Attribute {
        return type.BaseTypes()
            .Select(curType => curType.GetCustomAttribute<TAttribute>())
            .FirstOrDefault(attribute => attribute != null);
    }

    public static IEnumerable<Type> GetDecoratedTypes<TAttribute>(this Assembly assembly)
        where TAttribute : Attribute {
        return assembly.GetTypes()
            .Where(type => type.GetCustomAttributeFromAncestor<TAttribute>() != null);
    }
}