namespace AoE2NetDesktop.Form;

using AoE2NetDesktop.CtrlForm;
using AoE2NetDesktop.Utility.Forms;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

/// <summary>
/// App main form shortcut.
/// </summary>
public partial class FormMain : ControllableForm
{
    private Dictionary<string, Action> shortcutActions;

    /// <summary>
    /// Initialize shortcut key functions.
    /// Dictionary.Key string are below.
    /// $"{<see cref="Keys"/>}",
    /// $"Alt{<see cref="Keys"/>}",
    /// $"Shift{<see cref="Keys"/>}",
    /// $"ShiftAlt{<see cref="Keys"/>}".
    /// </summary>
    public void InitShortcut()
    {
        shortcutActions = new Dictionary<string, Action>() {
            { "F5", UpdateLastMatch },
            { "ShiftAltUp", DecreaseHeight1px },
            { "ShiftAltDown", IncreaseHeight1px },
            { "ShiftAltLeft", DecreaseWidth1px },
            { "ShiftAltRight", IncreaseWidth1px },
            { "AltUp", DecreaseHeight10px },
            { "AltDown", IncreaseHeight10px },
            { "AltLeft", DecreaseWidth10px },
            { "AltRight", IncreaseWidth10px },
            { "ShiftSpace", SwitchHideTitle },
            { "AltSpace", ShowWindowTitle },
            { "Left", PrevMatchResult },
            { "Right", NextMatchResult },
        };
    }

    // ///////////////////////////////////////////////////////////////////////
    // shortcut actions
    // ///////////////////////////////////////////////////////////////////////
#pragma warning disable VSTHRD100 // Avoid async void methods
    private async void NextMatchResult()
    {
        if(requestMatchView > 0) {
            requestMatchView--;
            CtrlMain.DisplayedMatch = await DrawMatchAsync(requestMatchView);
        }
    }

    private async void PrevMatchResult()
    {
        requestMatchView++;
        CtrlMain.DisplayedMatch = await DrawMatchAsync(requestMatchView);
    }
#pragma warning restore VSTHRD100 // Avoid async void methods

    private void DecreaseHeight10px() => Size += new Size(0, -10);

    private void IncreaseHeight10px() => Size += new Size(0, 10);

    private void DecreaseWidth10px() => Size += new Size(-10, 0);

    private void IncreaseWidth10px() => Size += new Size(10, 0);

    private void DecreaseHeight1px() => Size += new Size(0, -1);

    private void IncreaseHeight1px() => Size += new Size(0, 1);

    private void DecreaseWidth1px() => Size += new Size(-1, 0);

    private void IncreaseWidth1px() => Size += new Size(1, 0);

    private void SwitchHideTitle()
        => Settings.Default.MainFormIsHideTitle = !Settings.Default.MainFormIsHideTitle;

    // show the title bar and popup the window menu.
    private void ShowWindowTitle() =>
        Settings.Default.MainFormIsHideTitle = false;

    private void UpdateLastMatch()
    {
        // F5 is called by shortcut key settings of ToolStripMenuItem;
    }

    // ///////////////////////////////////////////////////////////////////////
    // Get shortcut function API
    // ///////////////////////////////////////////////////////////////////////
    private Action GetFunction(Keys keyCode, bool shift, bool alt)
    {
        var key = string.Empty;

        if(shift) {
            key += "Shift";
        }

        if(alt) {
            key += "Alt";
        }

        var result = shortcutActions.TryGetValue(
            key + keyCode.ToString(), out Action action);
        if(!result) {
            action = () =>
            {
                // Undefined shortcut key
            };
        }

        return action;
    }
}
