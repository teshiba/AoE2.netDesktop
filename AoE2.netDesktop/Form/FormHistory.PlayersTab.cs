namespace AoE2NetDesktop.Form
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using LibAoE2net;

    /// <summary>
    /// Players Tab of FormHistory class.
    /// </summary>
    public partial class FormHistory : ControllableForm
    {

        /// <summary>
        /// Gets or sets player country plot object.
        /// </summary>
        public PlayerCountryPlot PlayerCountryStat { get; set; }

        private void InitPlayersTab()
        {
            SetPlotStyleFormsPlotCountry();
            InitListViewMatchedPlayersSorter();
            PlayerCountryStat = new PlayerCountryPlot(formsPlotCountry);
        }

        private void UpdatePlayersTabGraph()
        {
            PlayerCountryStat.Plot(Controler.PlayerMatchHistory, Controler.ProfileId);
        }

        private void SetPlotStyleFormsPlotCountry()
        {
            formsPlotCountry.Plot.YAxis.TickLabelStyle(fontSize: FontSize);
            formsPlotCountry.Plot.XAxis.TickLabelStyle(fontSize: FontSize);
            formsPlotCountry.Plot.XAxis.LabelStyle(fontSize: FontSize + 3);
            formsPlotCountry.Plot.YAxis.LabelStyle(fontSize: FontSize + 3);
        }

        private void InitListViewMatchedPlayersSorter()
        {
            var sorterMatchedPlayers = new ListViewItemComparer {
                Column = 8,
                ColumnModes = new ComparerMode[]
                {
                    ComparerMode.String,
                    ComparerMode.String,
                    ComparerMode.String,
                    ComparerMode.Integer,
                    ComparerMode.Integer,
                    ComparerMode.Integer,
                    ComparerMode.Integer,
                    ComparerMode.Integer,
                    ComparerMode.DateTime,
                },
            };

            listViewMatchedPlayers.ListViewItemSorter = sorterMatchedPlayers;
        }

        private void UpdateListViewPlayers()
        {
            var listViewItems = new List<ListViewItem>();

            listViewMatchedPlayers.BeginUpdate();
            listViewMatchedPlayers.Items.Clear();
            foreach (var player in Controler.MatchedPlayerInfos) {
                var listviewItem = new ListViewItem(player.Key);
                listviewItem.SubItems.Add(player.Value.Country);
                listviewItem.SubItems.Add(player.Value.RateRM1v1.ToString());
                listviewItem.SubItems.Add(player.Value.RateRMTeam.ToString());
                listviewItem.SubItems.Add(player.Value.GamesTeam.ToString());
                listviewItem.SubItems.Add(player.Value.GamesAlly.ToString());
                listviewItem.SubItems.Add(player.Value.GamesEnemy.ToString());
                listviewItem.SubItems.Add(player.Value.Games1v1.ToString());
                listviewItem.SubItems.Add(player.Value.LastDate.ToString());
                listViewItems.Add(listviewItem);
            }

            // When calling Add of ListViewItemCollection frequently in foreach etc.,
            // it takes too much time in the ListViewItemSorte, so AddRange is called once instead.
            listViewMatchedPlayers.Items.AddRange(listViewItems.ToArray());

            listViewMatchedPlayers.EndUpdate();
        }

        private void OpenSelectedPlayerProfile()
        {
            var selectedItems = listViewMatchedPlayers.SelectedItems;

            if (selectedItems.Count != 0) {
                Controler.OpenProfile(selectedItems[0].Text);
            }
        }

        private void OpenSelectedPlayerHistory()
        {
            var selectedItems = listViewMatchedPlayers.SelectedItems;

            if (selectedItems.Count != 0) {
                var formHistory = Controler.GenerateFormHistory(selectedItems[0].Text);
                formHistory.Show();
            }
        }

        ///////////////////////////////////////////////////////////////////////
        // event handlers
        ///////////////////////////////////////////////////////////////////////

        private void ListViewMatchedPlayers_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OpenSelectedPlayerHistory();
        }

        private void ListViewMatchedPlayers_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            SortByColumn((ListView)sender, e);
        }

        private void ContextMenuStripMatchedPlayers_Opening(object sender, CancelEventArgs e)
        {
            var location = new Point(contextMenuStripMatchedPlayers.Left, contextMenuStripMatchedPlayers.Top);
            var point = listViewMatchedPlayers.PointToClient(location);
            var item = listViewMatchedPlayers.HitTest(point).Item;
            if (item?.Bounds.Contains(point) ?? false) {
                openAoE2NetProfileToolStripMenuItem.Visible = true;
            } else {
                e.Cancel = true;
            }
        }

        private void SplitContainerPlayers_DoubleClick(object sender, EventArgs e)
        {
            switch (splitContainerPlayers.Orientation) {
            case Orientation.Horizontal:
                splitContainerPlayers.Orientation = Orientation.Vertical;
                PlayerCountryStat.Orientation = ScottPlot.Orientation.Horizontal;
                break;
            case Orientation.Vertical:
                splitContainerPlayers.Orientation = Orientation.Horizontal;
                PlayerCountryStat.Orientation = ScottPlot.Orientation.Vertical;
                break;
            }
        }

        private void OpenHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenSelectedPlayerHistory();
        }

        private void OpenAoE2NetProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenSelectedPlayerProfile();
        }
    }
}
