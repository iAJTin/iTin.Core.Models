
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
    public partial class GroupField : DataField
    {
        /// <summary>
        /// Gets a value indicating data field type.
        /// </summary>
        /// <value>
        /// Always returns <see cref="KnownFieldType.Group"/>.
        /// </value>
        public override KnownFieldType FieldType => KnownFieldType.Group;


        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A <see cref="string"/> that represents the current object.
        /// </returns>
        /// <remarks>
        /// This method <see cref="ToString()"/> returns a string that includes name of group and alias of field.
        /// </remarks>
        public override string ToString() => $"Group=\"{Name}\", {base.ToString()}";
    }
}
