
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace iTin.Core.Models.Design.Styles;

[Serializable]
[DebuggerStepThrough]
[DesignerCategory("code")]
[XmlType(Namespace = "http://schemas.itin.com/models/core/v1.0")]
public partial class BaseStyle : BaseModel<BaseStyle>
{
    /// <inheritdoc/>
    public override bool IsDefault =>
        base.IsDefault &&
        Content.IsDefault &&
        Borders.IsDefault &&
        string.IsNullOrEmpty(Inherits);
}
