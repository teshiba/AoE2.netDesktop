namespace AoE2NetDesktop.Form;

/// <summary>
/// Stacked bar graph data struct.
/// </summary>
public class StackedBarGraphData
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StackedBarGraphData"/> class.
    /// </summary>
    /// <param name="lower">Lower data.</param>
    /// <param name="upper">Upper data.</param>
    public StackedBarGraphData(double lower, double? upper)
    {
        Lower = lower;
        Upper = upper;
    }

    /// <summary>
    /// Gets or sets lower data of stacked graph.
    /// </summary>
    public double Lower { get; set; }

    /// <summary>
    /// Gets or sets upper data of stacked graph.
    /// </summary>
    public double? Upper { get; set; }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"L:{Lower} U:{Upper?.ToString() ?? "null"}";
    }
}
