
using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace iTin.Core.Models.Design
{
    [Serializable]
    //[DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://schemas.iTin.com/charting/chart/v1.0")]
    public partial class TableDefinition : BaseModel<TableDefinition>
    {
        /// <summary>
        /// Gets a value indicating whether this instance is default.
        /// </summary>
        /// <value>
        /// <b>true</b> if this instance contains the default; otherwise, <b>false</b>.
        /// </value>
        [Browsable(false)]
        public override bool IsDefault =>
            Fields.IsDefault &&
            //Headers.IsDefault &&
            Alias.Equals(DefaultAlias) &&
            ShowDataValues.Equals(DefaultShowDataValues) &&
            ShowColumnHeaders.Equals(DefaultShowColumnHeaders);


        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents the current object.
        /// </returns>
        public override string ToString() => $"Name=\"{Name}\"";
    }
}
