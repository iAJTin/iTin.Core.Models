
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
[XmlRoot("Table", Namespace = "http://schemas.itin.com/models/core/v1.0", IsNullable = false)]
public partial class Resources : BaseModel<Resources>
{
    /// <inheritdoc/>
    [Browsable(false)]
    public override bool IsDefault =>
        Filters.IsDefault &&
        Fixed.IsDefault &&
        Groups.IsDefault;
}
