namespace AoE2NetDesktop.Form;

using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using ScottPlot;

/// <summary>
/// Extened BarPlot of ScottPlot.
/// </summary>
public class BarPlotEx
{
    private readonly FormsPlot formsPlot;
    private Orientation orientation;

    /// <summary>
    /// Initializes a new instance of the <see cref="BarPlotEx"/> class.
    /// </summary>
    /// <param name="formsPlot">Parent formsPlot.</param>
    public BarPlotEx(FormsPlot formsPlot)
    {
        this.formsPlot = formsPlot;
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
    /// Render graph.
    /// </summary>
    public void Render()
    {
        SetDataToFromsPlot();
        formsPlot.Render();
    }

    private void SetOrientation(Orientation orientation)
    {
        if (this.orientation != orientation) {
            this.orientation = orientation;
            SetDataToFromsPlot();
            formsPlot.Render();
        }
    }

    private void SetDataToFromsPlot()
    {
        formsPlot.Plot.Clear();
        formsPlot.Plot.XTicks();
        formsPlot.Plot.YTicks();

        if (Values.Count != 0) {
            var stackedLower = Values.Select(x => x.Value.Lower).ToArray();

            // if stacked graph data is avilable.
            if (Values.Where(x => x.Value.Upper != null).Any()) {
                var stackedUpper = new double[Values.Count];
                var upperData = Values.Select(x => (double)x.Value.Upper).ToArray();
                for (int i = 0; i < upperData.Length; i++) {
                    stackedUpper[i] = stackedLower[i] + upperData[i];
                }

                var barUpper = formsPlot.Plot.AddBar(stackedUpper);
                barUpper.Orientation = orientation;
                barUpper.ShowValuesAboveBars = ShowValuesAboveBars;
                barUpper.FillColor = Color.IndianRed;
            }

            var barLower = formsPlot.Plot.AddBar(stackedLower);
            barLower.Orientation = orientation;
            barLower.ShowValuesAboveBars = ShowValuesAboveBars;
            barLower.FillColor = Color.Green;

            formsPlot.Plot.SetAxisLimits(xMin: 0, yMin: -1);

            switch (orientation) {
            case Orientation.Horizontal:
                formsPlot.Plot.YTicks(Values.Keys.ToArray());
                formsPlot.Plot.SetAxisLimits(xMin: YMin, yMin: XMin);
                formsPlot.Plot.XLabel(ValueLabel);
                formsPlot.Plot.YLabel(ItemLabel);
                break;
            case Orientation.Vertical:
                formsPlot.Plot.XTicks(Values.Keys.ToArray());
                formsPlot.Plot.SetAxisLimits(xMin: XMin, yMin: YMin);
                formsPlot.Plot.XLabel(ItemLabel);
                formsPlot.Plot.YLabel(ValueLabel);
                formsPlot.Plot.XAxis.TickLabelStyle(rotation: ItemLabelRotation);
                break;
            }
        } else {
            formsPlot.Plot.YTicks(new string[] { "No Data" });
        }
    }
}
