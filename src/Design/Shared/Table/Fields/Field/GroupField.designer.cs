
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

using iTin.Core.Models.Design.Enums;

namespace iTin.Core.Models.Design.Table.Fields;

[Serializable]
[DebuggerStepThrough]
[DesignerCategory("code")]
[GeneratedCode("System.Xml", "4.0.30319.18033")]
[XmlType(Namespace = "http://schemas.itin.com/models/core/v1.0")]
public partial class GroupField : DataField
{
    /// <summary>
    /// Gets a value indicating data field type.
    /// </summary>
    /// <value>
    /// Always returns <see cref="KnownFieldType.Group"/>.
    /// </value>
    public override KnownFieldType FieldType => KnownFieldType.Group;


    /// <inheritdoc/>
    public override string ToString() => $"Group=\"{Name}\", {base.ToString()}";
}
