
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace iTin.Core.Models.Design.Table.Fields
{
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlInclude(typeof(FixedField))]
    [XmlInclude(typeof(GapField))]
    [XmlInclude(typeof(DataField))]
    [XmlInclude(typeof(GroupField))]
    [XmlInclude(typeof(PacketField))]
    [GeneratedCode("System.Xml", "4.0.30319.18033")]
    [XmlType(Namespace = "http://schemas.iTin.com/style/v1.0")]
    public abstract partial class BaseDataField : BaseModel<BaseDataField>
    {
        /// <summary>
        /// Gets a value indicating whether this instance is default.
        /// </summary>
        /// <value>
        /// <b>true</b> if this instance contains the default; otherwise, <b>false</b>.
        /// </value>
        public override bool IsDefault =>
            Width.ToUpperInvariant().Equals(WidthDefault) &&
            Header.IsDefault &&
            Value.IsDefault &&
            Aggregate.IsDefault;


        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A <see cref="string"/> that represents the current object.
        /// </returns>
        /// <remarks>
        /// This method <see cref="M:iExportEngine.Model.Export.Table.Fields.DataFieldModel.ToString"/> returns a string that includes field alias.
        /// </remarks>
        public override string ToString() => $"Alias=\"{Alias}\", Width=\"{Width}\"";
    }
}
