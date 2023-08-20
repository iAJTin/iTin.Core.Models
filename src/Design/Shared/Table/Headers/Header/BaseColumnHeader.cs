
namespace iTin.Core.Models.Design.Table.Headers;

/// <summary>
/// A Specialization of <see cref="IColumnHeader"/> interface.<br/>
/// Which acts as the base class for different column header configurations.
/// </summary>
public partial class BaseColumnHeader : IColumnHeader
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BaseColumnHeader"/> class.
    /// </summary>
    public BaseColumnHeader()
    {
        Show = DefaultShow;
        Text = DefaultText;
        Style = DefaultStyle;
    }
}
