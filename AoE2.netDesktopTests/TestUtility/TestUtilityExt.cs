namespace AoE2NetDesktop.Tests;

using System;
using System.Collections.Generic;
using System.Reflection;

public static class TestUtilityExt
{
    private static Assembly assemblyInstance;

    public static string AssemblyName { get; set; }

    public static T GetField<T>(this object obj, string name)
    {
        Type type;
        var bindingFlags = BindingFlags.NonPublic | BindingFlags.Static;

        if(obj.GetType().FullName == "System.RuntimeType") {
            type = (Type)obj;
        } else {
            bindingFlags |= BindingFlags.Instance;
            type = obj.GetType();
        }

        var fieldInfo = type.GetField(name, bindingFlags);
        if(fieldInfo == null) {
            fieldInfo = type.BaseType.GetField(name, bindingFlags);
        }

        return (T)fieldInfo.GetValue(obj);
    }

    public static void SetField<T>(this object obj, string name, T value)
    {
        var bindingFlags = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
        var fieldInfo = obj.GetType().GetField(name, bindingFlags);
        if(fieldInfo == null) {
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
    {
        var bindingFlags = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod;

        var argTypes = new List<Type>();

        foreach(var item in arg) {
            argTypes.Add(item.GetType());
        }

        var methodInfo = obj.GetType().GetMethod(name, bindingFlags, null, argTypes.ToArray(), null);
        if(methodInfo == null) {
            methodInfo = obj.GetType().BaseType.GetMethod(name, bindingFlags, null, argTypes.ToArray(), null);
        }

        return (T)methodInfo.Invoke(obj, arg);
    }

    public static void Invoke(this object obj, string name, params object[] arg)
    {
        var bindingFlags = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod;

        var argTypes = new List<Type>();

        foreach(var item in arg) {
            argTypes.Add(item.GetType());
        }

        var methodInfo = obj.GetType().GetMethod(name, bindingFlags, null, argTypes.ToArray(), null);
        if(methodInfo == null) {
            methodInfo = obj.GetType().BaseType.GetMethod(name, bindingFlags, null, argTypes.ToArray(), null);
        }

        methodInfo.Invoke(obj, arg);
    }

    public static void SetSettings<TValue>(string propertyName, TValue value)
    {
        if(AssemblyName is null) {
            throw new InvalidOperationException($"{nameof(AssemblyName)} is not set.");
        }

        if(propertyName is null) {
            throw new ArgumentNullException(nameof(propertyName));
        }

        if(assemblyInstance is null) {
            assemblyInstance = Assembly.LoadFrom(AssemblyName);
        }

        var settings = assemblyInstance.GetType($"{AssemblyName}.Settings");
        var settingsDefault = settings.GetProperty("Default").GetValue(settings);
        try {
            settingsDefault.GetType().GetProperty(propertyName).SetValue(settingsDefault, value);
        } catch(Exception e) {
            throw new Exception($"propertyName={propertyName}, value={value}\ntrace:{e.StackTrace}");
        }
    }

    public static TValue GetSettings<TValue>(string propertyName)
    {
        if(AssemblyName is null) {
            throw new InvalidOperationException($"{nameof(AssemblyName)} is not set.");
        }

        if(propertyName is null) {
            throw new ArgumentNullException(nameof(propertyName));
        }

        if(assemblyInstance is null) {
            assemblyInstance = Assembly.LoadFrom(AssemblyName);
        }

        var settings = assemblyInstance.GetType($"{AssemblyName}.Settings");
        var settingsDefault = settings.GetProperty("Default").GetValue(settings);

        return (TValue)settingsDefault.GetType().GetProperty(propertyName).GetValue(settingsDefault);
    }
}
