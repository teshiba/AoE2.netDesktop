namespace AoE2NetDesktopTests.TestUtility;
using System;
using System.Collections.Generic;
using System.Reflection;

public static class PrivateRefs
{
    public static T GetField<T>(this object obj, string name)
    {
        Type type;
        var bindingFlags = BindingFlags.NonPublic | BindingFlags.Static;

        if (obj.GetType().FullName == "System.RuntimeType") {
            type = (Type)obj;
        } else {
            bindingFlags |= BindingFlags.Instance;
            type = obj.GetType();
        }

        var fieldInfo = type.GetField(name, bindingFlags);

        if (fieldInfo?.GetValue(obj) == null) {
            fieldInfo = type.BaseType.GetField(name, bindingFlags);
        }

        return (T)fieldInfo.GetValue(obj);
    }

    public static void SetField<T>(this object obj, string name, T value)
    {
        var bindingFlags = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
        var fieldInfo = obj.GetType().GetField(name, bindingFlags);

        if (fieldInfo?.GetValue(obj) == null) {
            fieldInfo = obj.GetType().BaseType.GetField(name, bindingFlags);
        }

        fieldInfo.SetValue(obj, value);
    }

    public static T GetProperty<T>(this object obj, string name)
    {
        var bindingFlags = BindingFlags.NonPublic | BindingFlags.Instance;
        var propertyInfo = obj.GetType().GetProperty(name, bindingFlags);

        return (T)propertyInfo.GetValue(obj);
    }

    public static T Invoke<T>(this object obj, string name, params object[] arg)
        => (T)GetMethodInfo(obj, name, arg).Invoke(obj, arg);

    public static void Invoke(this object obj, string name, params object[] arg)
        => GetMethodInfo(obj, name, arg).Invoke(obj, arg);

    private static MethodInfo GetMethodInfo(object obj, string name, object[] arg)
    {
        var bindingFlags = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod;
        var argTypes = new List<Type>();

        foreach (var item in arg) {
            argTypes.Add(item.GetType());
        }

        var methodInfo = obj.GetType().GetMethod(name, bindingFlags, null, argTypes.ToArray(), null)
            ?? obj.GetType().BaseType.GetMethod(name, bindingFlags, null, argTypes.ToArray(), null);

        return methodInfo;
    }
}