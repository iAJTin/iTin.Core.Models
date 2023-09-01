
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace iTin.Core.Models.Design;

[Serializable]
//[DebuggerStepThrough]
[DesignerCategory("code")]
[XmlType(Namespace = "http://schemas.itin.com/models/core/v1.0")]
[XmlRoot(Namespace = "http://schemas.itin.com/models/core/v1.0", IsNullable = true)]
public partial class Location : BaseModel<Location>
{
}
