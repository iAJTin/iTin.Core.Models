
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace iTin.Core.Models.Design.Table.Headers.Header;

[Serializable]
[DebuggerStepThrough]
[DesignerCategory("code")]
[GeneratedCode("System.Xml", "4.0.30319.18033")]
[XmlType(Namespace = "http://schemas.itin.com/models/core/v1.0")]
[XmlRoot(Namespace = "http://schemas.itin.com/models/core/v1.0", IsNullable = true)]
public partial class ColumnHeaderGroup : BaseModel<ColumnHeaderGroup>
{
    /// <inheritdoc/>
    [Browsable(false)]
    public override bool IsDefault =>
        base.IsDefault &&
        Collapsed.Equals(DefaultCollapsed) &&
        Level.Equals(DefaultLevel) &&
        Show.Equals(DefaultShow);
}
