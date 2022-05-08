namespace AoE2NetDesktop.Form;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

using AoE2NetDesktop.LibAoE2Net.Functions;
using AoE2NetDesktop.LibAoE2Net.JsonFormat;
using AoE2NetDesktop.LibAoE2Net.Parameters;

using ScottPlot;
using ScottPlot.Plottable;

/// <summary>
/// Player rate graph.
/// </summary>
public class PlayerRatePlot
{
    private readonly FormsPlot formsPlot;
    private readonly Color plotLineColor;

    private ScatterPlot scatterPlot;
    private ScatterPlot scatterLines;
    private FinancePlot candlesticks;
    private PlotHighlight highlightPlot;

    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerRatePlot"/> class.
    /// </summary>
    /// <param name="formsPlot">Parent FormsPlot.</param>
    /// <param name="lineColor">draw line color.</param>
    public PlayerRatePlot(FormsPlot formsPlot, Color lineColor)
    {
        this.formsPlot = formsPlot;
        var initData = Array.Empty<double>();
        plotLineColor = lineColor;
        scatterPlot = new (initData, initData);
        scatterLines = new (initData, initData);
        candlesticks = new ();
        highlightPlot = new (new FormsPlot(), new ScatterPlot(initData, initData), string.Empty);
    }

    /// <summary>
    /// Gets minimum value of X.
    /// </summary>
    public double? MinX => scatterPlot.Xs.Length == 0 ? null : scatterPlot.Xs.Min();

    /// <summary>
    /// Gets minimum value of Y.
    /// </summary>
    public double? MinY => scatterPlot.Ys.Length == 0 ? null : scatterPlot.Ys.Min();

    /// <summary>
    /// Gets maximum value of X.
    /// </summary>
    public double? MaxX => scatterPlot.Xs.Length == 0 ? null : scatterPlot.Xs.Max();

    /// <summary>
    /// Gets maximum value of Y.
    /// </summary>
    public double? MaxY => scatterPlot.Ys.Length == 0 ? null : scatterPlot.Ys.Max();

    /// <summary>
    /// Gets or sets a value indicating whether the plot is visible.
    /// </summary>
    public bool IsVisible
    {
        get => scatterPlot.IsVisible;
        set {
            scatterPlot.IsVisible = value;
            scatterLines.IsVisible = value;
            candlesticks.IsVisible = value;
            highlightPlot.IsVisible = value;
            formsPlot.Render();
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the highlight is visible.
    /// </summary>
    public bool IsVisibleHighlight
    {
        get => highlightPlot.IsVisible;
        set {
            if (IsVisible) {
                highlightPlot.IsVisible = value;
            }
        }
    }

    /// <summary>
    /// Update Highlight.
    /// </summary>
    /// <returns>X-Y axis value.</returns>
    public (double pointX, double pointY) UpdateHighlight()
    {
        return highlightPlot.Update();
    }

    /// <summary>
    /// Plot player rate.
    /// </summary>
    /// <param name="playerMatchHistory">PlayerMatchHistory.</param>
    /// <param name="profileId">Profile ID.</param>
    /// <param name="leaderBoardId">LeaderBoard Type.</param>
    public void Plot(PlayerMatchHistory playerMatchHistory, int profileId, LeaderboardId leaderBoardId)
    {
        var dateList = new List<DateTime>();
        var rateList = new List<double>();

        foreach (var item in playerMatchHistory) {
            var player = item.GetPlayer(profileId);
            if (player.Rating != null) {
                if (item.LeaderboardId == leaderBoardId) {
                    rateList.Add((double)player.Rating);
                    dateList.Add(item.GetOpenedTime());
                }
            }
        }

        if (dateList.Count != 0) {
            formsPlot.Plot.Remove(scatterPlot);
            formsPlot.Plot.Remove(candlesticks);
            formsPlot.Plot.Remove(scatterLines);

            var xs = dateList.Select(x => x.ToOADate()).ToArray();

            var ohlc = new List<OHLC>();
            var oneDay = new TimeSpan(1, 0, 0, 0);

            ohlc.Add(new OHLC(rateList[0], rateList[0], rateList[0], rateList[0], dateList[0], oneDay));
            for (int i = 1; i < xs.Length; i++) {
                if (ohlc.Last().DateTime.Date != dateList[i].Date) {
                    ohlc.Add(new OHLC(rateList[i], rateList[i], rateList[i], rateList[i], dateList[i], oneDay));
                } else {
                    if (ohlc.Last().High < rateList[i]) {
                        ohlc.Last().High = rateList[i];
                    }

                    if (rateList[i] < ohlc.Last().Low) {
                        ohlc.Last().Low = rateList[i];
                    }

                    ohlc.Last().Close = rateList[i];
                }
            }

            scatterPlot = formsPlot.Plot.AddScatter(xs, rateList.ToArray(), color: plotLineColor, lineStyle: LineStyle.Dot);
            candlesticks = formsPlot.Plot.AddCandlesticks(ohlc.ToArray());
            candlesticks.ColorUp = Color.Red;
            candlesticks.ColorDown = Color.Green;

            try {
                var bol = candlesticks.GetBollingerBands(7);
                scatterLines = formsPlot.Plot.AddScatterLines(bol.xs, bol.sma, plotLineColor, 2, LineStyle.Solid, leaderBoardId.ToString());
            } catch (ArgumentException e) {
                Debug.Print($" Plot Rate ERROR. {e.Message} {e.StackTrace}");
            }

            highlightPlot = new PlotHighlight(formsPlot, scatterPlot, leaderBoardId.ToString());
            IsVisible = false;
        }
    }
}
