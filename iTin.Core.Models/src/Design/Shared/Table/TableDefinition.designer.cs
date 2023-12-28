
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace iTin.Core.Models.Design;

[Serializable]
[DebuggerStepThrough]
[DesignerCategory("code")]
[XmlType(Namespace = "http://schemas.itin.com/models/core/v1.0")]
[XmlRoot("Table", Namespace = "http://schemas.itin.com/models/core/v1.0", IsNullable = false)]

public partial class TableDefinition : BaseModel<TableDefinition>
{
    /// <inheritdoc/>
    [Browsable(false)]
    public override bool IsDefault =>
        Fields.IsDefault &&
        Alias.Equals(DefaultAlias) &&
        Show.Equals(DefaultShow) &&
        ShowDataValues.Equals(DefaultShowDataValues) &&
        ShowColumnHeaders.Equals(DefaultShowColumnHeaders);

    /// <inheritdoc/>
    public override string ToString() => $"Name=\"{Name}\", Show={Show}";
}
