namespace AoE2NetDesktop.Form.Tests
{
    using AoE2NetDesktop.Tests;

    using System.ComponentModel;
    using System.Windows.Forms;

    public static class FormExt
    {
        public static T GetControl<T>(this Form form, string name)
            where T : Component => form.GetField<T>(name);
    }
}