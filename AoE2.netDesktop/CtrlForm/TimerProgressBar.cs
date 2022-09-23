namespace AoE2NetDesktop.CtrlForm;

using System;
using System.Timers;
using System.Windows.Forms;

/// <summary>
/// ProgressBar with Timer.
/// </summary>
public class TimerProgressBar
{
    private readonly ProgressBar progressBar;
    private readonly System.Timers.Timer timer;

    /// <summary>
    /// Initializes a new instance of the <see cref="TimerProgressBar"/> class.
    /// </summary>
    /// <param name="progressBar">ProgressBar control.</param>
    public TimerProgressBar(ProgressBar progressBar)
    {
        this.progressBar = progressBar ?? throw new ArgumentNullException(nameof(progressBar));
        timer = new System.Timers.Timer();
        timer.Elapsed += Timer_Elapsed;
        timer.Interval = 1000;
    }

    /// <summary>
    /// Gets a value indicating whether the timer is running.
    /// </summary>
    public bool Started => timer.Enabled;

    /// <summary>
    /// Gets the current position of the progress bar.
    /// </summary>
    public int Value => progressBar.Value;

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

    private void Timer_Elapsed(object sender, ElapsedEventArgs e)
    {
        progressBar.Invoke(() =>
        {
            progressBar.Visible = true;
            if(progressBar.Value < progressBar.Maximum) {
                progressBar.Value++;
            }
        });
    }
}
