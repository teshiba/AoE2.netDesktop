namespace AoE2NetDesktop.Form
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// LastMatch loader class.
    /// </summary>
    public class LastMatchLoader
    {
        private readonly Timer timer = new () {
            Interval = 1000 * 60 * 5,
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="LastMatchLoader"/> class.
        /// </summary>
        /// <param name="action">Action.</param>
        public LastMatchLoader(EventHandler action)
        {
            timer.Tick += new EventHandler(action);
        }

        /// <summary>
        /// Start last match loader.
        /// </summary>
        public void Start() => timer.Start();

        /// <summary>
        /// Stop last match loader.
        /// </summary>
        public void Stop() => timer.Stop();
    }
}