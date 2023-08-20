
using System;

namespace iTin.Core.Models.Design;

public partial class FiltersCollection
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
    public FiltersCollection Clone()
    {
        var cloned = new FiltersCollection(Parent)
        {
            Properties = Properties.Clone()
        };

        foreach (var border in this)
        {
            cloned.Add(border.Clone());
        }

        return cloned;
    }
}
