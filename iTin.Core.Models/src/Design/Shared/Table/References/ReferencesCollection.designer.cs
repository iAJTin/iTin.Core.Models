
using iTin.Core.Helpers;
using iTin.Core.Models.Collections;

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace iTin.Core.Models.Design.Table;

[Serializable]
[DebuggerStepThrough]
[DesignerCategory("code")]
[GeneratedCode("System.Xml", "4.0.30319.18033")]
[XmlType(Namespace = "http://schemas.itin.com/models/core/v1.0")]
public partial class ReferencesCollection : BaseSimpleModelCollection<Reference, TableDefinition>
{
    /// <summary>
    /// Sets the owner of the provided <paramref name="item"/> to this <see cref="ReferencesCollection"/> instance.
    /// </summary>
    /// <param name="item">The <see cref="Reference"/> for which to set the owner.</param>
    /// <remarks>
    /// This method assigns this <see cref="ReferencesCollection"/> instance as the owner of the specified <paramref name="item"/>, establishing a parent-child relationship between them.
    /// </remarks>
    protected override void SetOwner(Reference item)
    {
        SentinelHelper.ArgumentNull(item, nameof(item));

        item.SetOwner(this);
    }
}
