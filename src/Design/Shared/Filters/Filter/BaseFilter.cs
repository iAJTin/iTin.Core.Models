
using System.Diagnostics;

using iTin.Core.Models.Design.Enums;

namespace iTin.Core.Models.Design;

/// <summary>
/// A Specialization of <see cref="IFilter"/> interface.<br/>
/// Which acts as the base class for different filter definitions.
/// </summary>
public partial class BaseFilter
{
    #region private constants

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const YesNo DefaultActive = YesNo.Yes;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const string DefaultValue = "";

    #endregion

    #region private memebrs

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private YesNo _active;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private KnownOperator _criterial;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string _field;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string _key;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private object _value;

    #endregion

    #region constructor/s

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseFilter"/> class.
    /// </summary>
    public BaseFilter()
    {
        Value = DefaultValue;
        Active = DefaultActive;
    }

    #endregion

    #region public static readonly properties

    /// <summary>
    /// Gets an empty filter.
    /// </summary>
    /// <value>
    /// An empty condition.
    /// </value>
    public static BaseFilter Empty => new();

    #endregion
}
