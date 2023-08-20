
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

using iTin.Core.Models.Design.Enums;

namespace iTin.Core.Models.Design.Table.Fields
{
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [GeneratedCode("System.Xml", "4.0.30319.18033")]
    [XmlType(Namespace = "http://schemas.iTin.com/style/v1.0")]
    public partial class GapField : BaseDataField
    {
        /// <summary>
        /// Gets a value indicating data field type.
        /// </summary>
        /// <value>
        /// Always returns <see cref="KnownFieldType.Gap"/>.
        /// </value>
        public override KnownFieldType FieldType => KnownFieldType.Gap;


        /// <summary>
        /// Gets a value indicating whether current data field supports data.
        /// </summary>
        /// <value>
        /// Always returns <strong>false</strong>".
        /// </value>
        protected override bool CanSetData => false;
    }
}
