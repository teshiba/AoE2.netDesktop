namespace AoE2NetDesktop.PlotEx;

using AoE2NetDesktop.Form;
using AoE2NetDesktop.LibAoE2Net.JsonFormat;
using AoE2NetDesktop.LibAoE2Net.Parameters;

using ScottPlot;

using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Manage each leaderBoard Player rate Graphs.
/// </summary>
public class PlayerRateFormsPlot
{
    private readonly FormsPlot formsPlot;
    private PlayerRatePlot lastHighlightPlot;

    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerRateFormsPlot"/> class.
    /// </summary>
    /// <param name="formsPlot">Target formsplot.</param>
    /// <param name="colorList">Leaderboard color.</param>
    /// <param name="fontSize">Font size.</param>
    public PlayerRateFormsPlot(FormsPlot formsPlot, List<LeaderboardView> colorList, float fontSize)
    {
        if(formsPlot is null) {
            throw new ArgumentNullException(nameof(formsPlot));
        }

        this.formsPlot = formsPlot;

        formsPlot.Plot.Title("Player Rate");
        formsPlot.Plot.YLabel("Rate");
        formsPlot.Plot.XLabel("Date");
        formsPlot.Plot.YAxis.TickLabelStyle(fontSize: fontSize);
        formsPlot.Plot.XAxis.TickLabelStyle(fontSize: fontSize);
        formsPlot.Plot.XAxis.LabelStyle(fontSize: fontSize + 3);
        formsPlot.Plot.YAxis.LabelStyle(fontSize: fontSize + 3);
        formsPlot.Render();

        Plots = new Dictionary<LeaderboardId, PlayerRatePlot>();
        foreach(var item in colorList) {
            Plots.Add(item.LeaderboardId, new PlayerRatePlot(formsPlot, item.Color));
        }

        formsPlot.Plot.XAxis.TickLabelFormat("yyyy/MM/dd", dateTimeFormat: true);
        formsPlot.Plot.XAxis.ManualTickSpacing(1, ScottPlot.Ticks.DateTimeUnit.Month);
        formsPlot.Plot.XAxis.TickLabelStyle(rotation: 45);
        formsPlot.Plot.Legend(true, Alignment.UpperLeft);
    }

    /// <summary>
    /// Gets or sets playerRatePlot list.
    /// </summary>
    public Dictionary<LeaderboardId, PlayerRatePlot> Plots { get; set; }

    /// <summary>
    /// Plot player rate.
    /// </summary>
    /// <param name="playerRatingHistory">PlayerRatingHistory.</param>
    public void Plot(PlayerRatingHistories playerRatingHistory)
    {
        formsPlot.Plot.Clear();
        foreach(var plot in Plots) {
            plot.Value.Plot(playerRatingHistory[plot.Key], plot.Key);
        }

        formsPlot.Plot.SetOuterViewLimits(
            GetMinX(Plots) - 10,
            GetMaxX(Plots) + 10,
            GetMinY(Plots) - 10,
            GetMaxY(Plots) + 10);
        formsPlot.Plot.SetAxisLimits(
            GetMinX(Plots) - 10,
            GetMaxX(Plots) + 10,
            GetMinY(Plots) - 10,
            GetMaxY(Plots) + 10);
        formsPlot.Plot.Render();
    }

    /// <summary>
    /// Update Highlight.
    /// </summary>
    public void UpdateHighlight()
    {
        var highlightPlot = GetHighlightPlot();

        foreach(var plot in Plots) {
            if(plot.Value == highlightPlot) {
                if(plot.Value != lastHighlightPlot) {
                    plot.Value.IsVisibleHighlight = true;
                    lastHighlightPlot = plot.Value;
                    formsPlot.Render();
                }
            } else {
                plot.Value.IsVisibleHighlight = false;
            }
        }
    }

    private static double GetMinX(Dictionary<LeaderboardId, PlayerRatePlot> plots)
        => plots.Select(x => x.Value.MinX).Min() ?? double.NaN;

    private static double GetMinY(Dictionary<LeaderboardId, PlayerRatePlot> plots)
        => plots.Select(x => x.Value.MinY).Min() ?? double.NaN;

    private static double GetMaxX(Dictionary<LeaderboardId, PlayerRatePlot> plots)
        => plots.Select(x => x.Value.MaxX).Max() ?? double.NaN;

    private static double GetMaxY(Dictionary<LeaderboardId, PlayerRatePlot> plots)
        => plots.Select(x => x.Value.MaxY).Max() ?? double.NaN;

    private PlayerRatePlot GetHighlightPlot()
    {
        var ret = lastHighlightPlot;
        var mindistanceSquared = double.MaxValue;
        (double mouseX, double mouseY) = formsPlot.GetMouseCoordinates();

        foreach(var plot in Plots.Where(plot => plot.Value.IsVisible)) {
            (double pointX, double pointY) = plot.Value.UpdateHighlight();
            var distanceSquared = ((pointX - mouseX) * (pointX - mouseX))
                                + ((pointY - mouseY) * (pointY - mouseY));
            if(distanceSquared < mindistanceSquared) {
                ret = plot.Value;
                mindistanceSquared = distanceSquared;
            }
        }

        return ret;
    }
}
