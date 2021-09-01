using System;
using System.Linq;
using System.Reflection;

namespace AoE2NetDesktop.Tests
{
    public static class TestUtilityExt
    {

        public static T GetField<T>(this object obj, string name)
        {
            var bindingFlags = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
            var fieldInfo = obj.GetType().GetField(name, bindingFlags);
            return (T)fieldInfo.GetValue(obj);
        }

        public static T GetProperty<T>(this object obj, string name)
        {
            var bindingFlags = BindingFlags.NonPublic | BindingFlags.Instance;
            var propertyInfo = obj.GetType()
                                .GetProperties(bindingFlags)
                                .Where(prop => (prop.Name == name) && (prop.PropertyType == typeof(T)))
                                .First();
            return (T)propertyInfo.GetValue(obj);
        }

        public static void SetSettings<TType, TValue>(TType testTargetInstance, string assemblyName, string propertyName, TValue value)
        {
            if (assemblyName is null) {
                throw new ArgumentNullException(nameof(assemblyName));
            }

            if (propertyName is null) {
                throw new ArgumentNullException(nameof(propertyName));
            }

            var settings = Assembly.GetAssembly(testTargetInstance.GetType()).GetType($"{assemblyName}.Settings");
            var settingsDefault = settings.GetProperty("Default").GetValue(settings);
            settingsDefault.GetType().GetProperty(propertyName).SetValue(settingsDefault, value);
        }

    }
}