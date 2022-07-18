namespace AoE2NetDesktop;

using AoE2NetDesktop.Form;
using AoE2NetDesktop.LibAoE2Net.Parameters;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;

/// <summary>
/// Main Program.
/// </summary>
internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    [ExcludeFromCodeCoverage(Justification = "Executing function does not need to test.")]
    private static void Main()
    {
        Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new FormMain(Language.en));
    }
}
