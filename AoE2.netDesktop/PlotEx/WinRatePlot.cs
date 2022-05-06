namespace AoE2NetDesktop.Form
{
    using System;
    using System.Collections.Generic;
    using LibAoE2net;
    using ScottPlot;

    /// <summary>
    /// Win rate graph.
    /// </summary>
    public class WinRatePlot : BarPlotEx
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WinRatePlot"/> class.
        /// </summary>
        /// <param name="formsPlot">Parent FormsPlot.</param>
        /// <param name="fontSize">Font size.</param>
        public WinRatePlot(FormsPlot formsPlot, float fontSize)
            : base(formsPlot)
        {
            formsPlot.Configuration.LockHorizontalAxis = true;
            formsPlot.Configuration.LockVerticalAxis = true;
            formsPlot.Plot.Title("Game and win count");
            formsPlot.Plot.Layout(top: 40); // for Data source comboBox
            ShowValuesAboveBars = true;
            XMin = -1;
            ValueLabel = "Win / Total Game count";
            ItemLabelRotation = 45;
            Orientation = Orientation.Horizontal;

            formsPlot.Plot.YAxis.TickLabelStyle(fontSize: fontSize);
            formsPlot.Plot.XAxis.TickLabelStyle(fontSize: fontSize);
            formsPlot.Plot.XAxis.LabelStyle(fontSize: fontSize + 3);
            formsPlot.Plot.YAxis.LabelStyle(fontSize: fontSize + 3);

            formsPlot.Render();
        }

        /// <summary>
        /// Plot win rate.
        /// </summary>
        /// <param name="playerMatchHistory">PlayerMatchHistory.</param>
        /// <param name="profileId">Profile ID.</param>
        /// <param name="leaderBoardId">target leader board.</param>
        /// <param name="dataSource">target data source.</param>
        public void Plot(PlayerMatchHistory playerMatchHistory, int profileId, LeaderboardId leaderBoardId, DataSource dataSource)
        {
            if (playerMatchHistory is null) {
                throw new ArgumentNullException(nameof(playerMatchHistory));
            }

            Values.Clear();
            ItemLabel = dataSource.ToString();

            foreach (var item in playerMatchHistory) {
                if (item.LeaderboardId == leaderBoardId) {
                    var player = item.GetPlayer(profileId);
                    switch (dataSource) {
                    case DataSource.Map:
                        AddWonRate(Values, player.Won, item.GetMapName());
                        break;
                    case DataSource.Civilization:
                        AddWonRate(Values, player.Won, player.GetCivName());
                        break;
                    }
                }
            }

            Render();
        }

        private static void AddWonRate(Dictionary<string, StackedBarGraphData> data, bool? won, string key)
        {
            if (won != null) {
                if (!data.ContainsKey(key)) {
                    data.Add(key, new StackedBarGraphData(0, 0));
                }

                if ((bool)won) {
                    data[key] = new (data[key].Lower + 1, (double)data[key].Upper);
                } else {
                    data[key] = new (data[key].Lower, (double)data[key].Upper + 1);
                }
            }
        }
    }
}