﻿namespace AoE2NetDesktop.From
{
    using System.ComponentModel;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    /// <summary>
    /// ControllableForm class.
    /// </summary>
    [TypeDescriptionProvider(typeof(FormDescriptionProvider))]
    public abstract class ControllableForm : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ControllableForm"/> class.
        /// </summary>
        /// <param name="formControler">formControler.</param>
        public ControllableForm(FormControler formControler)
        {
            formControler.Scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            Controler = formControler;
        }

        /// <summary>
        /// Gets async method awaiter.
        /// </summary>
        public AsyncMethodAwaiter Awaiter { get; } = new ();

        /// <summary>
        /// Gets formControler.
        /// </summary>
        protected virtual FormControler Controler { get; }
    }
}