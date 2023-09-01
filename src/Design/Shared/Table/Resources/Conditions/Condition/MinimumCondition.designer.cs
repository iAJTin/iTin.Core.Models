
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace iTin.Core.Models.Design.Table.Resource
{
    [GeneratedCode("System.Xml", "4.0.30319.18033")]
    [Serializable()]
    //[DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://schemas.itin.com/export/engine/2014/configuration/v1.0")]
    public partial class MinimumCondition : BaseCondition
    {
        /// <summary>
        /// Gets a value indicating whether this instance is default.
        /// </summary>
        /// <value>
        /// <strong>true</strong> if this instance contains the default; otherwise, <strong>false</strong>.
        /// </value>
        public override bool IsDefault => 
            base.IsDefault &&
            string.IsNullOrEmpty(Style);
    }
}
