
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

using iTin.Core.Helpers;
using iTin.Core.Models.Collections;
using iTin.Core.Models.Design.Enums;

namespace iTin.Core.Models.Design;

[Serializable]
//[DebuggerStepThrough]
[DesignerCategory("code")]
[XmlType(Namespace = "http://schemas.itin.com/models/core/v1.0")]
public partial class BordersCollection : BaseComplexModelCollection<IBorder, IParent, KnownBorderPosition>
{
    /// <summary>
    /// Gets a <see cref="IBorder"/> by its value.
    /// </summary>
    /// <param name="value">The value to search for.</param>
    /// <returns>
    /// The <see cref="IBorder"/> if found; otherwise, <see langword="null"/>.
    /// </returns>
    public override IBorder GetBy(KnownBorderPosition value) => Find(border => border.Position == value);

    /// <summary>
    /// Sets the owner of the provided <paramref name="item"/> to this <see cref="BordersCollection"/> instance.
    /// </summary>
    /// <param name="item">The <see cref="IBorder"/> for which to set the owner.</param>
    /// <remarks>
    /// This method assigns this <see cref="BordersCollection"/> instance as the owner of the specified <paramref name="item"/>, establishing a parent-child relationship between them.
    /// </remarks>
    protected override void SetOwner(IBorder item)
    {
        SentinelHelper.ArgumentNull(item, nameof(item));

        item.SetOwner(this);
    }
}
