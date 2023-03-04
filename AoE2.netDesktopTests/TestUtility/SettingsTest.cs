namespace AoE2NetDesktopTests.TestUtility;
using System;
using System.Reflection;

public static class SettingsRefs
{
    private static Assembly assemblyInstance;

    public static string AssemblyName { get; set; }

    public static TValue Get<TValue>(string propertyName)
    {
        var settingsDefault = InitAssemblyInstance(propertyName);

        return (TValue)settingsDefault.GetType().GetProperty(propertyName).GetValue(settingsDefault);
    }

    public static void Set<TValue>(string propertyName, TValue value)
    {
        var settingsDefault = InitAssemblyInstance(propertyName);

        try {
            settingsDefault.GetType().GetProperty(propertyName).SetValue(settingsDefault, value);
        } catch (Exception e) {
            throw new Exception($"propertyName={propertyName}, value={value}\ntrace:{e.StackTrace}");
        }
    }

    private static object InitAssemblyInstance(string propertyName)
    {
        if (AssemblyName is null) {
            throw new InvalidOperationException($"{nameof(AssemblyName)} is not set.");
        }

        if (propertyName is null) {
            throw new ArgumentNullException(nameof(propertyName));
        }

        assemblyInstance ??= Assembly.LoadFrom(AssemblyName);

        var settings = assemblyInstance.GetType($"{AssemblyName}.Settings");
        var settingsDefault = settings.GetProperty("Default").GetValue(settings);
        return settingsDefault;
    }
}