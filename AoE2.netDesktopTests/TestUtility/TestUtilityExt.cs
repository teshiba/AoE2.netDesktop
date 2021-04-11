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
    }
}