namespace AoE2NetDesktop.Utility.Forms;

using System;
using System.Collections;
using System.Windows.Forms;

/// <summary>
/// Comparer for ListView column sorting.
/// </summary>
public class ListViewItemComparer : IComparer
{
    private int column;
    private ComparerMode[] columnModes;

    /// <summary>
    /// Initializes a new instance of the <see cref="ListViewItemComparer"/> class.
    /// </summary>
    public ListViewItemComparer()
    {
        Column = 0;
        Order = SortOrder.None;
    }

    /// <summary>
    /// Gets or sets target Column of sorting.
    /// if set same sort target column, switch sort order.
    /// </summary>
    public int Column
    {
        get => column;

        set
        {
            if(column == value) {
                Order = Order switch {
                    SortOrder.None => SortOrder.Ascending,
                    SortOrder.Ascending => SortOrder.Descending,
                    SortOrder.Descending => SortOrder.Ascending,
                    _ => SortOrder.Ascending,
                };
            }

            column = value;
        }
    }

    /// <summary>
    /// Gets or sets sort order whether Descending or Ascending.
    /// </summary>
    public SortOrder Order { get; set; }

    /// <summary>
    /// Sets sort mode for each column.
    /// </summary>
    public ComparerMode[] ColumnModes
    {
        set => columnModes = value;
    }

    /// <inheritdoc/>
    public int Compare(object x, object y)
    {
        int result = 0;

        if(Order != SortOrder.None) {
            if(columnModes != null && columnModes.Length > column) {
                var textx = ((ListViewItem)x).SubItems[column].Text;
                var texty = ((ListViewItem)y).SubItems[column].Text;

                result = GetCompareResult(textx, texty, columnModes[column]);

                if(Order == SortOrder.Descending) {
                    result = -result;
                }
            }
        }

        return result;
    }

    private static int GetCompareResult(string textx, string texty, ComparerMode columnModes)
    {
        var ret = columnModes switch {
            ComparerMode.Integer => int.Parse(textx).CompareTo(int.Parse(texty)),
            ComparerMode.DateTime => DateTime.Compare(DateTime.Parse(textx), DateTime.Parse(texty)),
            _ => string.Compare(textx, texty),
        };
        return ret;
    }
}
