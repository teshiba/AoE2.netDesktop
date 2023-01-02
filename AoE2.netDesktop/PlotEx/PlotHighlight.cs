﻿namespace AoE2NetDesktop.PlotEx;

using System;
using System.Drawing;

using ScottPlot;
using ScottPlot.Plottable;

/// <summary>
/// ScottPlot ScatterPlot Highlight.
/// </summary>
public class PlotHighlight
{
    private readonly ScatterPlot targetPlot;
    private readonly string title;
    private readonly MarkerPlot highlightPlot;
    private readonly Tooltip tooltip;
    private readonly FormsPlot formsPlot;
    private int lastIndex = -1;

    /// <summary>
    /// Initializes a new instance of the <see cref="PlotHighlight"/> class.
    /// </summary>
    /// <param name="formsPlot">target plot area.</param>
    /// <param name="scatterPlot">target scatter plot.</param>
    /// <param name="title">Tooltip title.</param>
    public PlotHighlight(FormsPlot formsPlot, ScatterPlot scatterPlot, string title)
    {
        targetPlot = scatterPlot ?? throw new ArgumentNullException(nameof(scatterPlot));
        this.title = title;
        this.formsPlot = formsPlot ?? throw new ArgumentNullException(nameof(formsPlot));

        highlightPlot = formsPlot.Plot.AddPoint(0, 0);
        highlightPlot.Color = Color.Red;
        highlightPlot.MarkerSize = 10;
        highlightPlot.MarkerShape = MarkerShape.openCircle;

        tooltip = formsPlot.Plot.AddTooltip(label: "Rate", x: -1000, y: -1000);
        tooltip.LabelPadding = 0;
        tooltip.FillColor = Color.White;
        tooltip.Font.Size = 16;
        tooltip.Font.Bold = true;
    }

    /// <summary>
    /// Gets or sets a value indicating whether plot is visible.
    /// </summary>
    public bool IsVisible
    {
        get => highlightPlot.IsVisible;
        set
        {
            highlightPlot.IsVisible = value;
            tooltip.IsVisible = value;
        }
    }

    /// <summary>
    /// Update highlight.
    /// </summary>
    /// <returns>X-Y axis value.</returns>
    public (double pointX, double pointY) Update()
    {
        // determine point nearest the cursor
        (double mouseCoordX, double mouseCoordY) = formsPlot.GetMouseCoordinates();
        (double pointX, double pointY) ret = default;

        if(!double.IsNaN(formsPlot.Plot.YAxis.Dims.PxPerUnit)) {
            double xyRatio = formsPlot.Plot.XAxis.Dims.PxPerUnit / formsPlot.Plot.YAxis.Dims.PxPerUnit;
            (double pointX, double pointY, int pointIndex) = targetPlot.GetPointNearest(mouseCoordX, mouseCoordY, xyRatio);

            // place the highlight over the point of interest
            highlightPlot.X = pointX;
            highlightPlot.Y = pointY;
            tooltip.Label = $"{title}:{pointY}\n{DateTime.FromOADate(pointX)}";
            tooltip.X = pointX;
            tooltip.Y = pointY;

            // render if the highlighted point chnaged
            if(lastIndex != pointIndex) {
                lastIndex = pointIndex;
                formsPlot.Render();
            }

            ret = (pointX, pointY);
        }

        return ret;
    }
}
