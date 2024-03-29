﻿namespace AoE2NetDesktop.Form;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using AoE2NetDesktop.CtrlForm;
using AoE2NetDesktop.PlotEx;
using AoE2NetDesktop.Utility.Forms;

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
        InitListViewMatchedPlayersSorter();
        PlayerCountryStat = new PlayerCountryPlot(formsPlotCountry, FontSize);
    }

    private void UpdatePlayersTabGraph()
        => PlayerCountryStat.Plot(Controler.PlayerMatchHistory, Controler.ProfileId);

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

    private void UpdatePlayersTabListViewFilterCountory()
    {
        listViewFilterCountry.BeginUpdate();
        listViewFilterCountry.Items.Clear();

        var listviewItems = new List<ListViewItem>();

        foreach(var country in Controler.MatchedPlayerInfos.Select(x => x.Value.Country)) {
            if(!listviewItems.Exists((item) => item.Text == country)) {
                listviewItems.Add(new ListViewItem(country));
            }
        }

        listviewItems.Sort(new Comparison<ListViewItem>((item1, item2)
                                => string.Compare(item1.Text, item2.Text)));
        foreach(var item in listviewItems) {
            listViewFilterCountry.Items.Add(item.Text, item.Text, null);
        }

        listViewFilterCountry.EndUpdate();
    }

    private void UpdateListViewMatchedPlayers()
    {
        var listview = listViewMatchedPlayers;
        var findPlayerName = textBoxFindName.Text;
        var enable = checkBoxEnableCountryFilter.Checked;
        var ignoreCase = checkBoxIgnoreCase.Checked;

        var listViewItems = new List<ListViewItem>();
        var countries = GetCountryFilterList();

        listview.BeginUpdate();
        listview.Items.Clear();

        StringComparison stringComparison;
        if(ignoreCase) {
            stringComparison = StringComparison.CurrentCultureIgnoreCase;
        } else {
            stringComparison = StringComparison.CurrentCulture;
        }

        foreach(var playerInfo in Controler.MatchedPlayerInfos.Where(Predicate)) {
            var listviewItem = new ListViewItem(playerInfo.Value.Name);
            listviewItem.SubItems.Add(playerInfo.Value.Country);
            listviewItem.SubItems.Add(playerInfo.Value.RateRM1v1.ToString());
            listviewItem.SubItems.Add(playerInfo.Value.RateRMTeam.ToString());
            listviewItem.SubItems.Add(playerInfo.Value.GamesTeam.ToString());
            listviewItem.SubItems.Add(playerInfo.Value.GamesAlly.ToString());
            listviewItem.SubItems.Add(playerInfo.Value.GamesEnemy.ToString());
            listviewItem.SubItems.Add(playerInfo.Value.Games1v1.ToString());
            listviewItem.SubItems.Add(playerInfo.Value.LastDate.ToString());
            listviewItem.Tag = playerInfo.Value;
            listViewItems.Add(listviewItem);
        }

        // When calling Add of ListViewItemCollection frequently in foreach etc.,
        // it takes too much time in the ListViewItemSorte, so AddRange is called once instead.
        listview.Items.AddRange(listViewItems.ToArray());

        listview.EndUpdate();

        // local function
        bool Predicate(KeyValuePair<int?, PlayerInfo> x)
        {
            var ret = false;

            if(x.Value.Name.Contains(findPlayerName, stringComparison)) {
                if(enable == false
                    || countries.Count == 0
                    || countries.Contains(x.Value.Country)) {
                    ret = true;
                }
            }

            return ret;
        }
    }

    private void OpenSelectedPlayerProfile()
    {
        var selectedItems = listViewMatchedPlayers.SelectedItems;

        if(selectedItems.Count != 0) {
            var playerInfo = (PlayerInfo)selectedItems[0].Tag;
            Controler.OpenProfile(playerInfo.ProfileId);
        }
    }

    private void OpenSelectedPlayerHistory()
    {
        var selectedItems = listViewMatchedPlayers.SelectedItems;
        if(selectedItems.Count != 0) {
            var playerInfo = (PlayerInfo)selectedItems[0].Tag;
            var formHistory = CtrlHistory.GenerateFormHistory(playerInfo.Name, playerInfo.ProfileId);
            formHistory.Show();
        }
    }

    ///////////////////////////////////////////////////////////////////////
    // event handlers
    ///////////////////////////////////////////////////////////////////////

    private void ListViewMatchedPlayers_MouseDoubleClick(object sender, MouseEventArgs e)
        => OpenSelectedPlayerHistory();

    private void ListViewMatchedPlayers_ColumnClick(object sender, ColumnClickEventArgs e)
        => SortByColumn((ListView)sender, e);

    private void ContextMenuStripMatchedPlayers_Opening(object sender, CancelEventArgs e)
    {
        var location = new Point(contextMenuStripMatchedPlayers.Left, contextMenuStripMatchedPlayers.Top);
        var point = listViewMatchedPlayers.PointToClient(location);
        var item = listViewMatchedPlayers.HitTest(point).Item;

        if(item?.Bounds.Contains(point) ?? false) {
            openAoE2NetProfileToolStripMenuItem.Visible = true;
        } else {
            e.Cancel = true;
        }
    }

    private void ShowListviewFilterCountry(Point point)
    {
        listViewFilterCountry.Location = new Point(point.X - 5, point.Y - 5);
        listViewFilterCountry.Width = (int)((listViewMatchedPlayers.Width - point.X) * 0.9);
        listViewFilterCountry.Height = (int)(listViewFilterCountry.Width / 1.618);
        listViewFilterCountry.Visible = true;
    }

    private void TextBoxFindName_TextChanged(object sender, EventArgs e)
        => UpdateListViewMatchedPlayers();

    private List<string> GetCountryFilterList()
    {
        var list = new List<string>();
        foreach(ListViewItem item in listViewFilterCountry.CheckedItems) {
            list.Add(item.Text);
        }

        return list;
    }

    private void ListViewFilterCountory_ItemChecked(object sender, ItemCheckedEventArgs e)
    {
        // When listViewFilterCountory shows first,
        // each item's checkboxes are initialized, and this handler is called.
        // But the player list does not need updating.
        if(e.Item.Focused) {
            UpdateListViewMatchedPlayers();
        }
    }

    private void CheckBoxIgnoreCase_CheckedChanged(object sender, EventArgs e)
        => UpdateListViewMatchedPlayers();

    private void SplitContainerPlayers_DoubleClick(object sender, EventArgs e)
    {
        switch(splitContainerPlayers.Orientation) {
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
        => OpenSelectedPlayerHistory();

    private void OpenAoE2NetProfileToolStripMenuItem_Click(object sender, EventArgs e)
        => OpenSelectedPlayerProfile();

    private void CheckBoxEnableCountryFilter_CheckedChanged(object sender, EventArgs e)
        => UpdateListViewMatchedPlayers();
}
