namespace AoE2NetDesktop.PlotEx;

using AoE2NetDesktop.LibAoE2Net.Functions;
using AoE2NetDesktop.LibAoE2Net.JsonFormat;
using AoE2NetDesktop.LibAoE2Net.Parameters;

using ScottPlot;

using System;

/// <summary>
/// Player country graph.
/// </summary>
public class PlayerCountryPlot : BarPlotEx
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerCountryPlot"/> class.
    /// </summary>
    /// <param name="formsPlot">Parent FormsPlot.</param>
    /// <param name="fontSize">Font size.</param>
    public PlayerCountryPlot(FormsPlot formsPlot, float fontSize)
        : base(formsPlot)
    {
        formsPlot.Configuration.LockHorizontalAxis = true;
        formsPlot.Configuration.LockVerticalAxis = true;
        formsPlot.Plot.Title("Player's country");
        ShowValuesAboveBars = true;
        XMin = -1;
        ItemLabel = "Country";
        ValueLabel = "Game count";
        ItemLabelRotation = 45;
        Orientation = Orientation.Horizontal;

        formsPlot.Plot.YAxis.TickLabelStyle(fontSize: fontSize);
        formsPlot.Plot.XAxis.TickLabelStyle(fontSize: fontSize);
        formsPlot.Plot.XAxis.LabelStyle(fontSize: fontSize + 3);
        formsPlot.Plot.YAxis.LabelStyle(fontSize: fontSize + 3);

        formsPlot.Render();
    }

    /// <summary>
    /// Plot played player country.
    /// </summary>
    /// <param name="playerMatchHistory">PlayerMatchHistory.</param>
    /// <param name="profileId">profile ID.</param>
    public void Plot(PlayerMatchHistory playerMatchHistory, int profileId)
    {
        if(playerMatchHistory is null) {
            throw new ArgumentNullException(nameof(playerMatchHistory));
        }

        Values.Clear();

        foreach(var match in playerMatchHistory) {
            foreach(var player in match.Players) {
                var selectedPlayer = match.GetPlayer(profileId);
                if(player != selectedPlayer) {
                    var country = CountryCode.ConvertToFullName(player.Country);
                    if(!Values.ContainsKey(country)) {
                        var stackedData = new StackedBarGraphData(0, 0);
                        Values.Add(country, stackedData);
                    }

                    Values[country].Lower++;
                }
            }
        }

        if(Values.Count != 0) {
            Render();
        }
    }
}
