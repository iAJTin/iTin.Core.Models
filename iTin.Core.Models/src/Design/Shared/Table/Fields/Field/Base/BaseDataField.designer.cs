
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace iTin.Core.Models.Design.Table.Fields;

[Serializable]
[DebuggerStepThrough]
[DesignerCategory("code")]
[XmlInclude(typeof(FixedField))]
[XmlInclude(typeof(GapField))]
[XmlInclude(typeof(DataField))]
[XmlInclude(typeof(GroupField))]
[XmlInclude(typeof(PacketField))]
[GeneratedCode("System.Xml", "4.0.30319.18033")]
[XmlType(Namespace = "http://schemas.itin.com/models/core/v1.0")]
public abstract partial class BaseDataField : BaseModel<BaseDataField>
{
    /// <inheritdoc/>
    public override bool IsDefault =>
        Width.ToUpperInvariant().Equals(WidthDefault) &&
        Header.IsDefault &&
        Value.IsDefault &&
        Aggregate.IsDefault;


    /// <inheritdoc/>
    public override string ToString() => $"Alias=\"{Alias}\", Width=\"{Width}\"";
}
