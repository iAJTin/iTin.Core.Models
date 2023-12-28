
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace iTin.Core.Models.Design.Table.Resource.Filters;

[Serializable]
[DebuggerStepThrough]
[DesignerCategory("code")]
[GeneratedCode("System.Xml", "4.0.30319.18033")]
[XmlType(Namespace = "http://schemas.itin.com/models/core/v1.0")]
public partial class Filter : BaseModel<Filter>
{
    /// <inheritdoc/>
    [Browsable(false)]
    public override bool IsDefault => 
        string.IsNullOrEmpty(Key) && 
        string.IsNullOrEmpty(Field);
}
