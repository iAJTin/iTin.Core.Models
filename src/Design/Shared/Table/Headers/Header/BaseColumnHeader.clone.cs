
using System;

namespace iTin.Core.Models.Design.Table.Headers;

public partial class BaseColumnHeader
{
    /// <summary>
    /// Creates a new object that is a copy of the current instance.
    /// </summary>
    /// <returns>
    /// A new object that is a copy of this instance.
    /// </returns>
    object ICloneable.Clone() => Clone();

    /// <summary>
    /// Clones this instance.
    /// </summary>
    /// <returns>
    /// A new object that is a copy of this instance.
    /// </returns>
    public BaseColumnHeader Clone()
    {
        var cloned = (BaseColumnHeader)MemberwiseClone();
        cloned.Properties = Properties.Clone();

        return cloned;
    }
}
