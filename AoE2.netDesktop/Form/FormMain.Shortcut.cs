namespace AoE2NetDesktop.Form;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

using AoE2NetDesktop.CtrlForm;
using AoE2NetDesktop.Utility;
using AoE2NetDesktop.Utility.Forms;

/// <summary>
/// App main form shortcut.
/// </summary>
public partial class FormMain : ControllableForm
{
    private Dictionary<string, Func<Task>> shortcutActions;

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
        shortcutActions = new Dictionary<string, Func<Task>>() {
            { "F5", UpdateLastMatchAsync },
            { "ShiftAltUp", DecreaseHeight1pxAsync },
            { "ShiftAltDown", IncreaseHeight1pxAsync },
            { "ShiftAltLeft", DecreaseWidth1pxAsync },
            { "ShiftAltRight", IncreaseWidth1pxAsync },
            { "AltUp", DecreaseHeight10pxAsync },
            { "AltDown", IncreaseHeight10pxAsync },
            { "AltLeft", DecreaseWidth10pxAsync },
            { "AltRight", IncreaseWidth10pxAsync },
            { "ShiftSpace", SwitchHideTitleAsync },
            { "AltSpace", ShowWindowTitleAsync },
            { "Left", PrevMatchResultAsync },
            { "Right", NextMatchResultAsync },
        };
    }

    // ///////////////////////////////////////////////////////////////////////
    // shortcut actions
    // ///////////////////////////////////////////////////////////////////////
    private async Task NextMatchResultAsync()
    {
        if(displayStatus is DisplayStatus.Shown or DisplayStatus.RedrawingPrevMatch) {
            if(Controler.RequestMatchView > 0) {
                Controler.RequestMatchView--;
                if(progressBar.Start()) {
                    await UpdateRequestedMatchAsync();
                    progressBar.Stop();
                }
            }
        }
    }

    private async Task PrevMatchResultAsync()
    {
        if(displayStatus is DisplayStatus.Shown or DisplayStatus.RedrawingPrevMatch) {
            Controler.RequestMatchView++;

            if(progressBar.Start()) {
                await UpdateRequestedMatchAsync();
                progressBar.Stop();
            }
        }
    }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    private async Task DecreaseHeight10pxAsync() => Size += new Size(0, -10);

    private async Task IncreaseHeight10pxAsync() => Size += new Size(0, 10);

    private async Task DecreaseWidth10pxAsync() => Size += new Size(-10, 0);

    private async Task IncreaseWidth10pxAsync() => Size += new Size(10, 0);

    private async Task DecreaseHeight1pxAsync() => Size += new Size(0, -1);

    private async Task IncreaseHeight1pxAsync() => Size += new Size(0, 1);

    private async Task DecreaseWidth1pxAsync() => Size += new Size(-1, 0);

    private async Task IncreaseWidth1pxAsync() => Size += new Size(1, 0);

    private async Task SwitchHideTitleAsync()
        => Settings.Default.MainFormIsHideTitle = !Settings.Default.MainFormIsHideTitle;

    // show the title bar and popup the window menu.
    private async Task ShowWindowTitleAsync() =>
        Settings.Default.MainFormIsHideTitle = false;

    private async Task UpdateLastMatchAsync()
    {
        // F5 is called by shortcut key settings of ToolStripMenuItem;
    }
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously

    // ///////////////////////////////////////////////////////////////////////
    // Get shortcut function API
    // ///////////////////////////////////////////////////////////////////////
    private Func<Task> GetShortcutFunction(Keys keyCode, bool shift, bool alt)
    {
        var key = string.Empty;

        if(shift) {
            key += "Shift";
        }

        if(alt) {
            key += "Alt";
        }

        var keyString = key + keyCode.ToString();
        var result = shortcutActions.TryGetValue(keyString, out var action);

        if(!result) {
            action = () => Task.Run(() =>
            {
                // Undefined shortcut key
            });
        }

        Log.Debug($"Press key {keyString}");

        return action;
    }
}
