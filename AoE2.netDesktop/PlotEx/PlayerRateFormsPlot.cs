namespace AoE2NetDesktop.Form
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using LibAoE2net;
    using ScottPlot;

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
        /// <param name="color">Leaderboard color.</param>
        public PlayerRateFormsPlot(FormsPlot formsPlot, LeaderboardColor color)
        {
            if (formsPlot is null) {
                throw new ArgumentNullException(nameof(formsPlot));
            }

            this.formsPlot = formsPlot;

            Plots = new Dictionary<LeaderboardId, PlayerRatePlot>() {
                { LeaderboardId.RM1v1, new PlayerRatePlot(formsPlot, color.RM1v1) },
                { LeaderboardId.RMTeam, new PlayerRatePlot(formsPlot, color.RMTeam) },
                { LeaderboardId.DM1v1, new PlayerRatePlot(formsPlot, color.DM1v1) },
                { LeaderboardId.DMTeam, new PlayerRatePlot(formsPlot, color.DMTeam) },
                { LeaderboardId.EW1v1, new PlayerRatePlot(formsPlot, color.EW1v1) },
                { LeaderboardId.EWTeam, new PlayerRatePlot(formsPlot, color.EWTeam) },
                { LeaderboardId.Unranked, new PlayerRatePlot(formsPlot, color.Unranked) },
            };

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
        /// <param name="playerMatchHistory">PlayerMatchHistory.</param>
        /// <param name="profileId">Profile ID.</param>
        public void Plot(PlayerMatchHistory playerMatchHistory, int profileId)
        {
            formsPlot.Plot.Clear();
            foreach (var plot in Plots) {
                plot.Value.Plot(playerMatchHistory, profileId, plot.Key);
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
            var highlightPlot = lastHighlightPlot;
            var mindistanceSquared = double.MaxValue;
            (double mouseX, double mouseY) = formsPlot.GetMouseCoordinates();

            foreach (var plot in Plots) {
                if (plot.Value.IsVisible) {
                    (double pointX, double pointY) = plot.Value.UpdateHighlight();
                    var distanceSquared = ((pointX - mouseX) * (pointX - mouseX))
                                        + ((pointY - mouseY) * (pointY - mouseY));
                    if (distanceSquared < mindistanceSquared) {
                        highlightPlot = plot.Value;
                        mindistanceSquared = distanceSquared;
                    }
                }
            }

            foreach (var plot in Plots) {
                if (highlightPlot == plot.Value) {
                    if (highlightPlot != lastHighlightPlot) {
                        plot.Value.IsVisibleHighlight = true;
                        lastHighlightPlot = highlightPlot;
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
    }
}