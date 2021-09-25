namespace AoE2NetDesktop.Form
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    using ScottPlot;

    /// <summary>
    /// Extened BarPlot of ScottPlot.
    /// </summary>
    public class BarPlotEx
    {
        private Orientation orientation;

        /// <summary>
        /// Initializes a new instance of the <see cref="BarPlotEx"/> class.
        /// </summary>
        /// <param name="formsPlot">Parent formsPlot.</param>
        public BarPlotEx(FormsPlot formsPlot)
        {
            FormsPlot = formsPlot;
        }

        /// <summary>
        /// Gets or sets X-Axis min limit.
        /// </summary>
        public double XMin { get; set; }

        /// <summary>
        /// Gets or sets Y-Axis min limit.
        /// </summary>
        public double YMin { get; set; }

        /// <summary>
        /// Gets or sets data item label.
        /// </summary>
        public string ItemLabel { get; set; }

        /// <summary>
        /// Gets or sets value label.
        /// </summary>
        public string ValueLabel { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether ShowValuesAboveBars.
        /// </summary>
        public bool ShowValuesAboveBars { get; set; }

        /// <summary>
        /// Gets or sets rotation of X-axis Tick Labels.
        /// </summary>
        public float? ItemLabelRotation { get; set; }

        /// <summary>
        /// Gets or sets stacked value list.
        /// </summary>
        public Dictionary<string, StackedBarGraphData> Values { get; set; } = new ();

        /// <summary>
        /// Gets or sets orientation of the bars.
        /// Default behavior is vertical.
        /// So values are on the Y-axis and positions are on the X-axis.
        /// </summary>
        public Orientation Orientation
        {
            get => orientation;
            set => SetOrientation(value);
        }

        /// <summary>
        /// Gets parent formsPlot.
        /// </summary>
        protected FormsPlot FormsPlot { get; }

        /// <summary>
        /// Render graph.
        /// </summary>
        public void Render()
        {
            SetDataToFromsPlot();
            FormsPlot.Render();
        }

        private void SetOrientation(Orientation orientation)
        {
            if (this.orientation != orientation) {
                this.orientation = orientation;
                SetDataToFromsPlot();
                FormsPlot.Render();
            }
        }

        private void SetDataToFromsPlot()
        {
            FormsPlot.Plot.Clear();
            FormsPlot.Plot.XTicks();
            FormsPlot.Plot.YTicks();

            if (Values.Count != 0) {
                var stackedLower = Values.Select(x => x.Value.Lower).ToArray();

                // if stacked graph data is avilable.
                if (Values.Where(x => x.Value.Upper != null).Any()) {
                    var stackedUpper = new double[Values.Count];
                    var upperData = Values.Select(x => (double)x.Value.Upper).ToArray();
                    for (int i = 0; i < upperData.Length; i++) {
                        stackedUpper[i] = stackedLower[i] + upperData[i];
                    }

                    var barUpper = FormsPlot.Plot.AddBar(stackedUpper);
                    barUpper.Orientation = orientation;
                    barUpper.ShowValuesAboveBars = ShowValuesAboveBars;
                    barUpper.FillColor = Color.IndianRed;
                }

                var barLower = FormsPlot.Plot.AddBar(stackedLower);
                barLower.Orientation = orientation;
                barLower.ShowValuesAboveBars = ShowValuesAboveBars;
                barLower.FillColor = Color.Green;

                FormsPlot.Plot.SetAxisLimits(xMin: 0, yMin: -1);

                switch (orientation) {
                case Orientation.Horizontal:
                    FormsPlot.Plot.YTicks(Values.Keys.ToArray());
                    FormsPlot.Plot.SetAxisLimits(xMin: YMin, yMin: XMin);
                    FormsPlot.Plot.XLabel(ValueLabel);
                    FormsPlot.Plot.YLabel(ItemLabel);
                    break;
                case Orientation.Vertical:
                    FormsPlot.Plot.XTicks(Values.Keys.ToArray());
                    FormsPlot.Plot.SetAxisLimits(xMin: XMin, yMin: YMin);
                    FormsPlot.Plot.XLabel(ItemLabel);
                    FormsPlot.Plot.YLabel(ValueLabel);
                    FormsPlot.Plot.XAxis.TickLabelStyle(rotation: ItemLabelRotation);
                    break;
                }
            } else {
                FormsPlot.Plot.YTicks(new string[] { "No Data" });
            }
        }
    }
}