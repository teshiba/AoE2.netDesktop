namespace AoE2NetDesktop.Form
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Notify Property Settings class.
    /// </summary>
    public class PropertySettings : INotifyPropertyChanged
    {
        private static readonly Dictionary<string, PropertyChangedEventArgs> PropertyChangedEventArgs = new () {
            { nameof(ChromaKey), new PropertyChangedEventArgs(nameof(ChromaKey)) },
            { nameof(IsHideTitle), new PropertyChangedEventArgs(nameof(IsHideTitle)) },
            { nameof(IsAlwaysOnTop), new PropertyChangedEventArgs(nameof(IsAlwaysOnTop)) },
            { nameof(Opacity), new PropertyChangedEventArgs(nameof(Opacity)) },
            { nameof(IsTransparency), new PropertyChangedEventArgs(nameof(IsTransparency)) },
            { nameof(DrawHighQuality), new PropertyChangedEventArgs(nameof(DrawHighQuality)) },
        };

        private string chromaKey;
        private bool isHideTitle;
        private bool isAlwaysOnTop;
        private double opacity;
        private bool isTransparency;
        private bool drawHighQuality;

        /// <inheritdoc/>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets ChromaKey.
        /// </summary>
        public string ChromaKey
        {
            get => chromaKey;
            set => SetProperty(ref chromaKey, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether window title is hide.
        /// </summary>
        public bool IsHideTitle
        {
            get => isHideTitle;
            set => SetProperty(ref isHideTitle, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether window is AlwaysOnTop.
        /// </summary>
        public bool IsAlwaysOnTop
        {
            get => isAlwaysOnTop;
            set => SetProperty(ref isAlwaysOnTop, value);
        }

        /// <summary>
        /// Gets or sets main form opacity.
        /// </summary>
        public double Opacity
        {
            get => opacity;
            set => SetProperty(ref opacity, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether main form is transparency.
        /// </summary>
        public bool IsTransparency
        {
            get => isTransparency;
            set => SetProperty(ref isTransparency, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether high quality drawing.
        /// </summary>
        public bool DrawHighQuality
        {
            get => drawHighQuality;
            set => SetProperty(ref drawHighQuality, value);
        }

        /// <summary>
        /// Set and notify property if the value is changed.
        /// </summary>
        /// <typeparam name="T">property type.</typeparam>
        /// <param name="field">backing field.</param>
        /// <param name="value">set value.</param>
        /// <param name="propertyName">property name.</param>
        /// <returns>Returns whether the value has changed.</returns>
        protected virtual bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            var ret = false;

            if (!Equals(field, value)) {
                field = value;
                PropertyChanged?.Invoke(this, PropertyChangedEventArgs[propertyName]);
                ret = true;
            }

            return ret;
        }
    }
}
