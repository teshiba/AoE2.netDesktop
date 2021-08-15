namespace AoE2NetDesktop.Form
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Windows.Forms;
    using AoE2NetDesktop.From;
    using LibAoE2net;

    using Microsoft.CodeAnalysis.CSharp.Syntax;

    /// <summary>
    /// FormHistory class.
    /// </summary>
    public partial class FormHistory : ControllableForm
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormHistory"/> class.
        /// </summary>
        /// <param name="ctrlHistory">A class that inherits FormControler.</param>
        public FormHistory(CtrlHistory ctrlHistory)
            : base(ctrlHistory)
        {
            InitializeComponent();
        }

        /// <inheritdoc/>
        protected override CtrlHistory Controler { get => (CtrlHistory)base.Controler; }

        private async void FormHistory_ShownAsync(object sender, System.EventArgs e)
        {
            bool ret = await Controler.ReadPlayerMatchHistoryAsync();
            var dateList1v1 = new List<DateTime>();
            var rateList1v1 = new List<double>();
            var dateListTeam = new List<DateTime>();
            var rateListTeam = new List<double>();
            var rateListUnranked = new List<double>();

            if (ret) {
                foreach (var item in Controler.PlayerMatchHistory) {
                    var player = Controler.GetSelectedPlayer(item);
                    var listViewItem = new ListViewItem(item.GetMapName());

                    listViewItem.SubItems.Add((player.Rating?.ToString() ?? "----")
                                            + (player.RatingChange?.Contains('-') ?? true ? string.Empty : "+")
                                            + player.RatingChange?.ToString());

                    if (player.Won == null) {
                        listViewItem.SubItems.Add("---");
                    } else {
                        var won = (bool)player.Won;
                        listViewItem.SubItems.Add(won ? "o" : string.Empty);
                    }

                    listViewItem.SubItems.Add(player.GetCivName());
                    listViewItem.SubItems.Add(player.Color.ToString() ?? "-");
                    listViewItem.SubItems.Add(item.GetOpenedTime().ToString());
                    listViewItem.SubItems.Add(item.Version);

                    if (player.Rating != null) {
                        int rate = (int)player.Rating;
                        switch (item.LeaderboardId) {
                        case LeaderBoardId.OneVOneRandomMap:
                            rateList1v1.Add(rate);
                            listViewHistory1v1.Items.Add(listViewItem);
                            dateList1v1.Add(item.GetOpenedTime());
                            break;
                        case LeaderBoardId.TeamRandomMap:
                            rateListTeam.Add(rate);
                            listViewHistoryTeam.Items.Add(listViewItem);
                            dateListTeam.Add(item.GetOpenedTime());
                            break;
                        default:
                            break;
                        }
                    }
                }

                var xs1v1 = dateList1v1.Select(x => x.ToOADate()).ToArray();
                var xsTeam = dateListTeam.Select(x => x.ToOADate()).ToArray();
                formsPlotRate.Plot.XAxis.DateTimeFormat(true);
                formsPlotRate.Plot.AddScatter(xs1v1, rateList1v1.ToArray());
                formsPlotRate.Plot.AddScatter(xsTeam, rateListTeam.ToArray());
            } else {
                Debug.Print("ReadPlayerMatchHistoryAsync ERROR.");
            }
        }
    }
}
