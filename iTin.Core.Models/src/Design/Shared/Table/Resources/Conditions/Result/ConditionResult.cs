
namespace iTin.Core.Models.Design.Table.Resource;

/// <summary>
/// Class that defines the result of applying a condition to a data field.
/// </summary>
public class ConditionResult
{
    #region constructor/s

    /// <summary>
    /// Initializes a new instance of the <see cref="ConditionResult"/> class.
    /// </summary>
    public ConditionResult()
    {
        CanApply = false;
        Style = string.Empty;
    }

    #endregion

    #region public static readonly properties

    /// <summary>
    /// Gets a default condition result
    /// </summary>
    /// <value>
    /// A default condition result.
    /// </value>
    public static ConditionResult Default => new();

    #endregion

    #region public properties

    /// <summary>
    /// gets a value that indicates if the condition can be applied.
    /// </summary>
    /// <value>
    /// <see langword="true"/> if the condition has been met and can be applied; otherwise, <see langword="false"/>,
    /// </value>
    public bool CanApply { get; internal set; }

    /// <summary>
    /// Gets a value that constains the style to apply.
    /// </summary>
    /// <value>
    /// A <see cref="T:System.String"/> that contains style name to apply.
    /// </value>
    public string Style { get; internal set; }

    #endregion
}
