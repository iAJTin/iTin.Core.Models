
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Xml.Serialization;

namespace iTin.Core.Models.Design.Table.Resource;

[Serializable]
[DebuggerStepThrough]
[DesignerCategory("code")]
[GeneratedCode("System.Xml", "4.0.30319.18033")]
[XmlType(Namespace = "http://schemas.itin.com/models/core/v1.0")]
[XmlRoot(Namespace = "http://schemas.itin.com/models/core/v1.0", IsNullable = true)]
public partial class Fixed : BaseModel<Fixed>
{
    /// <inheritdoc/>
    [Browsable(false)]
    public override bool IsDefault =>
        string.IsNullOrEmpty(Name) &&
        string.IsNullOrEmpty(Reference) &&
        !Pieces.Any();

    /// <inheritdoc />
    //public override string ToString() => $"Count = {Pieces.Count}";
}
