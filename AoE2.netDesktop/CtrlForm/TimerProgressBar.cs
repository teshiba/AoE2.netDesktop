namespace AoE2NetDesktop.CtrlForm;

using System;
using System.Windows.Forms;

/// <summary>
/// ProgressBar with Timer.
/// </summary>
public class TimerProgressBar
{
    private readonly ProgressBar progressBar;
    private readonly Timer timer;

    /// <summary>
    /// Initializes a new instance of the <see cref="TimerProgressBar"/> class.
    /// </summary>
    /// <param name="progressBar">ProgressBar control.</param>
    /// <param name="timer">Timer control.</param>
    public TimerProgressBar(ProgressBar progressBar, Timer timer)
    {
        this.progressBar = progressBar;
        this.timer = timer;
        this.timer.Tick += Timer_Tick;
    }

    /// <summary>
    /// Stop timer.
    /// </summary>
    public void Stop()
    {
        timer.Stop();
        progressBar.Value = progressBar.Maximum;
        progressBar.Refresh();
        progressBar.Visible = false;
    }

    /// <summary>
    /// Start timer.
    /// </summary>
    /// <returns>true: start the timer, false: timer already started.</returns>
    public bool Start()
    {
        var ret = false;
        if(!timer.Enabled) {
            Restart();
            ret = true;
        }

        return ret;
    }

    /// <summary>
    /// Restart timer.
    /// </summary>
    public void Restart()
    {
        progressBar.Value = 0;
        progressBar.Visible = true;
        timer.Start();
    }

    private void Timer_Tick(object sender, EventArgs e)
    {
        progressBar.Visible = true;
        if(progressBar.Value < progressBar.Maximum) {
            progressBar.Value++;
        }
    }
}
