namespace AoE2NetDesktop.From
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    /// <summary>
    /// A class that controls Windows Forms class.
    /// </summary>
    /// <typeparam name="T">Target form class.</typeparam>
    public abstract class FormControler<T>
        where T : Form
    {
        private readonly TaskScheduler scheduler;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormControler{T}"/> class.
        /// </summary>
        public FormControler()
        {
            scheduler = TaskScheduler.FromCurrentSynchronizationContext();
        }

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
                scheduler);
        }
    }
}