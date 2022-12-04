using System.Reflection;

namespace Locksley.App.Helpers; 

public static class ReflectionHelper {
    public static IEnumerable<Type> GetAllInterfaces() {
        return Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsInterface);
    }

    public static IEnumerable<Type> GetAllInterfacesInNamespace(string? @namespace) {
        return GetAllInterfaces().Where(t => t.Namespace == @namespace);
    }

    public static IEnumerable<Type> GetAllClasses() {
        return Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsClass);
    }

    public static IEnumerable<Type> GetAllClassesInNamespace(string? @namespace) {
        return GetAllClasses().Where(t => t.Namespace == @namespace);
    }
    
    public static IEnumerable<Type> GetAllSubclasses(Type type) {
        return GetAllClasses().Where(t => t.IsSubclassOf(type));
    }

    public static IEnumerable<Type> GetAllSubclasses<T>() => GetAllSubclasses(typeof(T));

    public static IEnumerable<Type> GetAllSubclassesInNamespace(Type type, string? @namespace) {
        return GetAllSubclasses(type).Where(t => t.Namespace == @namespace);
    }

    public static IEnumerable<Type> GetAllSubclassesInNamespace<T>(string? @namespace) =>
        GetAllSubclassesInNamespace(typeof(T), @namespace);
}