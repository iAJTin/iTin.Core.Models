
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
    public partial class PacketField : BaseDataField
    {
        /// <summary>
        /// Gets a value indicating data field type.
        /// </summary>
        /// <value>
        /// Always returns <see cref="KnownFieldType.Packet"/>.
        /// </value>
        public override KnownFieldType FieldType => KnownFieldType.Packet;


        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A <see cref="string"/> that represents the current object.
        /// </returns>
        /// <remarks>
        /// This method <see cref="ToString()"/> returns a string that includes field name and field alias.
        /// </remarks>
        public override string ToString() => $"Name=\"{Name}\", {base.ToString()}";
    }
}
