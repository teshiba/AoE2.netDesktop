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

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerRateFormsPlot"/> class.
        /// </summary>
        /// <param name="formsPlot">Target formsplot.</param>
        public PlayerRateFormsPlot(FormsPlot formsPlot)
        {
            if (formsPlot is null) {
                throw new ArgumentNullException(nameof(formsPlot));
            }

            this.formsPlot = formsPlot;

            Plots = new Dictionary<LeaderboardId, PlayerRatePlot>() {
                { LeaderboardId.RM1v1, new PlayerRatePlot(formsPlot) },
                { LeaderboardId.RMTeam, new PlayerRatePlot(formsPlot) },
                { LeaderboardId.DM1v1, new PlayerRatePlot(formsPlot) },
                { LeaderboardId.DMTeam, new PlayerRatePlot(formsPlot) },
                { LeaderboardId.EW1v1, new PlayerRatePlot(formsPlot) },
                { LeaderboardId.EWTeam, new PlayerRatePlot(formsPlot) },
                { LeaderboardId.Unranked, new PlayerRatePlot(formsPlot) },
            };

            formsPlot.Plot.XAxis.TickLabelFormat("yyyy/MM/dd", dateTimeFormat: true);
            formsPlot.Plot.XAxis.ManualTickSpacing(1, ScottPlot.Ticks.DateTimeUnit.Month);
            formsPlot.Plot.XAxis.TickLabelStyle(rotation: 45);
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
        }

        /// <summary>
        /// Update Highlight.
        /// </summary>
        public void UpdateHighlight()
        {
            foreach (var plot in Plots) {
                plot.Value.UpdateHighlight();
            }
        }

        private static double GetMinX(Dictionary<LeaderboardId, PlayerRatePlot> plots)
            => plots.Select(x => x.Value.MinX).Min() ?? double.NegativeInfinity;

        private static double GetMinY(Dictionary<LeaderboardId, PlayerRatePlot> plots)
            => plots.Select(x => x.Value.MinY).Min() ?? double.NegativeInfinity;

        private static double GetMaxX(Dictionary<LeaderboardId, PlayerRatePlot> plots)
            => plots.Select(x => x.Value.MaxX).Max() ?? double.PositiveInfinity;

        private static double GetMaxY(Dictionary<LeaderboardId, PlayerRatePlot> plots)
            => plots.Select(x => x.Value.MaxY).Max() ?? double.PositiveInfinity;
    }
}