
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Xml.Serialization;

namespace iTin.Core.Models.Design.Table.Headers;

[Serializable]
//[DebuggerStepThrough]
[DesignerCategory("code")]
[GeneratedCode("System.Xml", "4.0.30319.18033")]
[XmlType(Namespace = "http://schemas.itin.com/models/core/v1.0")]
public partial class BaseColumnHeader : BaseModel<BaseColumnHeader>
{
    /// <inheritdoc/>
    [Browsable(false)]
    public override bool IsDefault =>
        Text.Equals(DefaultText) &&
        Show.Equals(DefaultShow) &&
        Style.Equals(DefaultStyle);
}
