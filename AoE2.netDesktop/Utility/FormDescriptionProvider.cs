namespace AoE2NetDesktop.From
{
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;

    /// <summary>
    /// Abstract class description provider.
    /// </summary>
    public class FormDescriptionProvider : TypeDescriptionProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormDescriptionProvider"/> class.
        /// </summary>
        public FormDescriptionProvider()
            : base(TypeDescriptor.GetProvider(typeof(Form)))
        {
        }

        /// <inheritdoc/>
        public override Type GetReflectionType(Type objectType, object instance)
        {
            return typeof(Form);
        }

        /// <inheritdoc/>
        public override object CreateInstance(IServiceProvider provider, Type objectType, Type[] argTypes, object[] args)
        {
            return base.CreateInstance(provider, typeof(Form), argTypes, args);
        }
    }
}