
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

using iTin.Core.Helpers;
using iTin.Core.Models.Collections;

namespace iTin.Core.Models.Design.Table
{
    [Serializable]
    //[DebuggerStepThrough]
    [DesignerCategory("code")]
    [GeneratedCode("System.Xml", "4.0.30319.18033")]
    [XmlType(Namespace = "http://schemas.iTin.com/style/v1.0")]
    public partial class ColumnsHeadersCollection : BaseComplexModelCollection<IColumnHeader, IParent, string>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override IColumnHeader GetBy(string value) => 
            this.Find(columnHeader => columnHeader.Equals(value));

        /// <summary>
        /// Sets the element that owns this <see cref="ColumnsHeadersCollection"/>.
        /// </summary>
        /// <param name="item">Reference to owner.</param>
        /// <exception cref="ArgumentNullException"><paramref name="item"/> is <see langword="null"/></exception>
        protected override void SetOwner(IColumnHeader item)
        {
            SentinelHelper.ArgumentNull(item, nameof(item));

            item.SetOwner(this);
        }
    }
}
