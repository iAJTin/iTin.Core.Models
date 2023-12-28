
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

using iTin.Core.Helpers;
using iTin.Core.Models.Collections;
using iTin.Core.Models.Design.Table.Resource.Filters;

namespace iTin.Core.Models.Design.Table.Resource;

[Serializable]
[DebuggerStepThrough]
[DesignerCategory("code")]
[GeneratedCode("System.Xml", "4.0.30319.18033")]
[XmlType(Namespace = "http://schemas.itin.com/models/core/v1.0")]
public partial class FiltersCollection : BaseComplexModelCollection<Filter, Resources, string>
{
    /// <summary>
    /// Gets a <see cref="Filter"/> by its value.
    /// </summary>
    /// <param name="value">The value to search for.</param>
    /// <returns>
    /// The <see cref="Filter"/> if found; otherwise, <see langword="null"/>.
    /// </returns>
    public override Filter GetBy(string value) => Find(filter => filter.Key == value);

    /// <summary>
    /// Sets the owner of the provided <paramref name="item"/> to this <see cref="FiltersCollection"/> instance.
    /// </summary>
    /// <param name="item">The <see cref="Filter"/> for which to set the owner.</param>
    /// <remarks>
    /// This method assigns this <see cref="FiltersCollection"/> instance as the owner of the specified <paramref name="item"/>, establishing a parent-child relationship between them.
    /// </remarks>
    /// <exception cref="ArgumentNullException"><paramref name="item"/> is <see langword="null"/></exception>
    protected override void SetOwner(Filter item)
    {
        SentinelHelper.ArgumentNull(item, nameof(item));

        item.SetOwner(this);
    }
}
