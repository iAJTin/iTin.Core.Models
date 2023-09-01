
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace iTin.Core.Models.Design.Table.Resource;

[XmlInclude(typeof(MaximumCondition))]
[XmlInclude(typeof(MinimumCondition))]
[XmlInclude(typeof(RemarksCondition))]
[XmlInclude(typeof(WhenChangeCondition))]
[XmlInclude(typeof(ZeroCondition))]
[Serializable]
[DebuggerStepThrough]
[DesignerCategory("code")]
[GeneratedCode("System.Xml", "4.0.30319.18033")]
[XmlType(Namespace = "http://schemas.itin.com/models/core/v1.0")]

public abstract partial class BaseCondition : BaseModel<BaseCondition>
{
    /// <inheritdoc/>
    public override bool IsDefault =>
        string.IsNullOrEmpty(Key) &&
        string.IsNullOrEmpty(Field);

    /// <inheritdoc/>
    public override string ToString() => $"Key=\"{Key}\", Field=\"{Field}\"";
}
