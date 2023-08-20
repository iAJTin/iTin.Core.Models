
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

using iTin.Core.Helpers;
using iTin.Core.Models.Collections;

namespace iTin.Core.Models.Design
{
    [Serializable]
    //[DebuggerStepThrough]
    [DesignerCategory("code")]
    [GeneratedCode("System.Xml", "4.0.30319.18033")]
    [XmlType(Namespace = "http://schemas.iTin.com/charting/chart/v1.0")]
    [XmlRoot(Namespace = "http://schemas.iTin.com/charting/chart/v1.0", IsNullable = true)]
    public partial class FiltersCollection : BaseComplexModelCollection<BaseFilter, object, string>
    {
        /// <summary>
        /// Returns the element specified.
        /// </summary>
        /// <param name="value">The object to locate in the <see cref="BaseComplexModelCollection{TItem, TParent, TSearch}"/>.</param>
        /// <returns>
        /// Returns the specified element.
        /// </returns>
        public override BaseFilter GetBy(string value) => Find(filter => filter.Key == value);

        /// <summary>
        /// Sets this collection as the owner of the specified item.
        /// </summary>
        /// <param name="item">Target item to set owner.</param>
        protected override void SetOwner(BaseFilter item)
        {
            SentinelHelper.ArgumentNull(item, nameof(item));

            item.SetOwner(this);
        }

    }
}
