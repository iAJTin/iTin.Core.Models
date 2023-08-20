
using System;

using iTin.Core.Models.Design.Table.Headers;

namespace iTin.Core.Models.Design.Table;

/// <summary>
/// 
/// </summary>
public partial class ColumnsHeadersCollection
{
    /// <inheritdoc />
    object ICloneable.Clone() => Clone();


    /// <summary>
    /// Clones this instance.
    /// </summary>
    /// <returns>
    /// A new object that is a copy of this instance.
    /// </returns>
    public ColumnsHeadersCollection Clone()
    {
        var cloned = new ColumnsHeadersCollection(Parent)
        {
            Properties = Properties.Clone()
        };

        foreach (var border in this)
        {
            cloned.Add((BaseColumnHeader)border.Clone());
        }

        return cloned;
    }
}
