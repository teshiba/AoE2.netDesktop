namespace AoE2NetDesktop.PlotEx;

using ScottPlot;

using System.Collections.Generic;
using System.Drawing;
using System.Linq;

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
    public Dictionary<string, StackedBarGraphData> Values { get; set; } = new();

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
        SetDataToFromsPlot(formsPlot.Plot);
        formsPlot.Render();
    }

    private void SetOrientation(Orientation orientation)
    {
        if(this.orientation != orientation) {
            this.orientation = orientation;
            SetDataToFromsPlot(formsPlot.Plot);
            formsPlot.Render();
        }
    }

    private void SetDataToFromsPlot(Plot plot)
    {
        plot.Clear();
        plot.XTicks();
        plot.YTicks();

        if(Values.Count != 0) {
            var stackedLower = Values.Select(x => x.Value.Lower).ToArray();

            // if stacked graph data is avilable.
            if(Values.Where(x => x.Value.Upper != null).Any()) {
                var stackedUpper = new double[Values.Count];
                var upperData = Values.Select(x => (double)x.Value.Upper).ToArray();
                for(int i = 0; i < upperData.Length; i++) {
                    stackedUpper[i] = stackedLower[i] + upperData[i];
                }

                var barUpper = plot.AddBar(stackedUpper);
                barUpper.Orientation = orientation;
                barUpper.ShowValuesAboveBars = ShowValuesAboveBars;
                barUpper.FillColor = Color.IndianRed;
            }

            var barLower = plot.AddBar(stackedLower);
            barLower.Orientation = orientation;
            barLower.ShowValuesAboveBars = ShowValuesAboveBars;
            barLower.FillColor = Color.Green;

            plot.SetAxisLimits(xMin: 0, yMin: -1);

            switch(orientation) {
            case Orientation.Horizontal:
                plot.YTicks(Values.Keys.ToArray());
                plot.SetAxisLimits(xMin: YMin, yMin: XMin);
                plot.XLabel(ValueLabel);
                plot.YLabel(ItemLabel);
                break;
            case Orientation.Vertical:
                plot.XTicks(Values.Keys.ToArray());
                plot.SetAxisLimits(xMin: XMin, yMin: YMin);
                plot.XLabel(ItemLabel);
                plot.YLabel(ValueLabel);
                plot.XAxis.TickLabelStyle(rotation: ItemLabelRotation);
                break;
            }
        } else {
            plot.YTicks(new string[] { "No Data" });
        }
    }
}
