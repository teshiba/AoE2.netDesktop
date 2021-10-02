namespace AoE2NetDesktop.Form
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// A class that controls Windows Forms class.
    /// </summary>
    public abstract class FormControler
    {
        /// <summary>
        /// Gets or sets scheduler.
        /// </summary>
        public TaskScheduler Scheduler { get; set; }

        /// <summary>
        /// Invoke the specified function on the UI task.
        /// </summary>
        /// <param name="function">A function delegate that returns the future result to be available through the task.</param>
        public void Invoke(Func<Task> function)
        {
            Task.Factory.StartNew(
                function,
                CancellationToken.None,
                TaskCreationOptions.None,
                Scheduler);
        }
    }
}