namespace AoE2NetDesktop.Form
{
    using System.Threading.Tasks;
    using System.Windows.Forms;

    /// <summary>
    /// ControllableForm class.
    /// </summary>
    public class ControllableForm : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ControllableForm"/> class.
        /// </summary>
        public ControllableForm()
        {
        }

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