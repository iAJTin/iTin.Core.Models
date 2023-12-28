
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

using iTin.Core.Helpers;
using iTin.Core.Models.Collections;

namespace iTin.Core.Models.Design.Table;

[Serializable]
//[DebuggerStepThrough]
[DesignerCategory("code")]
[GeneratedCode("System.Xml", "4.0.30319.18033")]
[XmlType(Namespace = "http://schemas.itin.com/models/core/v1.0")]
public partial class ColumnsHeadersCollection : BaseComplexModelCollection<IColumnHeader, IParent, string>
{
    /// <summary>
    /// Gets a <see cref="IColumnHeader"/> by its value.
    /// </summary>
    /// <param name="value">The value to search for.</param>
    /// <returns>
    /// The <see cref="IColumnHeader"/> if found; otherwise, <see langword="null"/>.
    /// </returns>
    public override IColumnHeader GetBy(string value) => this.Find(columnHeader => columnHeader.Equals(value));

    /// <summary>
    /// Sets the owner of the provided <paramref name="item"/> to this <see cref="ColumnsHeadersCollection"/> instance.
    /// </summary>
    /// <param name="item">The <see cref="IColumnHeader"/> for which to set the owner.</param>
    /// <remarks>
    /// This method assigns this <see cref="ColumnsHeadersCollection"/> instance as the owner of the specified <paramref name="item"/>, establishing a parent-child relationship between them.
    /// </remarks>
    protected override void SetOwner(IColumnHeader item)
    {
        SentinelHelper.ArgumentNull(item, nameof(item));

        item.SetOwner(this);
    }
}
