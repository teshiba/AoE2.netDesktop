using System.Windows.Forms;
using AoE2NetDesktop.Tests;

namespace AoE2NetDesktop.From.Tests
{
    public static class FormExt
    {
        public static T GetControl<T>(this Form form, string name)
            where T : Control => form.GetField<T>(name);
    }
}