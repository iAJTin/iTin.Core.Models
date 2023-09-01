
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
public partial class Reference : BaseModel<Reference>
{
    /// <inheritdoc/>
    [Browsable(false)]
    public override bool IsDefault =>
        string.IsNullOrEmpty(Assembly) &&
        Path.Equals(DefaultPath);
}
