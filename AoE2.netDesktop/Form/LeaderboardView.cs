namespace AoE2NetDesktop.Form;
using AoE2NetDesktop.LibAoE2Net.Parameters;

using System.Drawing;

/// <summary>
/// LeaderboardView parameters class.
/// </summary>
public class LeaderboardView
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LeaderboardView"/> class.
    /// </summary>
    /// <param name="index">listview index.</param>
    /// <param name="text">display text.</param>
    /// <param name="leaderboardId">kind of leaderboard.</param>
    /// <param name="color">display forecolor.</param>
    public LeaderboardView(int index, string text, LeaderboardId leaderboardId, Color color)
    {
        Index = index;
        Text = text;
        Color = color;
        LeaderboardId = leaderboardId;
    }

    /// <summary>
    /// Gets or sets listview index.
    /// </summary>
    public int Index { get; set; }

    /// <summary>
    /// Gets or sets display text.
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    /// Gets or sets kind of leaderboard.
    /// </summary>
    public LeaderboardId LeaderboardId { get; set; }

    /// <summary>
    /// Gets or sets display forecolor.
    /// </summary>
    public Color Color { get; set; }
}
