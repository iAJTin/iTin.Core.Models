
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Xml.Serialization;

namespace iTin.Core.Models.Design.Table.Headers
{
    [Serializable]
    //[DebuggerStepThrough]
    [DesignerCategory("code")]
    [GeneratedCode("System.Xml", "4.0.30319.18033")]
    [XmlType(Namespace = "http://schemas.iTin.com/style/v1.0")]
    public partial class BaseColumnHeader : BaseModel<BaseColumnHeader>
    {
        /// <summary>
        /// Gets a value indicating whether this instance is default.
        /// </summary>
        /// <value>
        /// <b>true</b> if this instance contains the default; otherwise, <b>false</b>.
        /// </value>
        [Browsable(false)]
        public override bool IsDefault =>
            Text.Equals(DefaultText) &&
            Show.Equals(DefaultShow) &&
            Style.Equals(DefaultStyle);
    }
}