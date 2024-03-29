﻿namespace AoE2NetDesktopTests.TestUtility;

using System.ComponentModel;
using System.Windows.Forms;

public static class FormExt
{
    public static T GetControl<T>(this Form form, string name)
        where T : Component => form.GetField<T>(name);
}
