
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Xml.Serialization;

using iTin.Core.Helpers;
using iTin.Core.Models.Collections;

namespace iTin.Core.Models.Design.Table.Resource
{
    [Serializable]
    //[DebuggerStepThrough]
    [DesignerCategory("code")]
    [GeneratedCode("System.Xml", "4.0.30319.18033")]
    [XmlType(Namespace = "http://schemas.itin.com/export/engine/2014/configuration/v1.0")]
    public partial class ConditionsCollection : BaseComplexModelCollection<BaseCondition, Resources, string>
    {
        /// <summary>
        /// Sets the owner of the provided <paramref name="item"/> to this <see cref="ConditionsCollection"/> instance.
        /// </summary>
        /// <param name="item">The <see cref="BaseCondition"/> for which to set the owner.</param>
        /// <remarks>
        /// This method assigns this <see cref="ConditionsCollection"/> instance as the owner of the specified <paramref name="item"/>, establishing a parent-child relationship between them.
        /// </remarks>
        protected override void SetOwner(BaseCondition item)
        {
            SentinelHelper.ArgumentNull(item, nameof(item));

            item.SetOwner(this);
        }

        /// <summary>
        /// Gets a <see cref="BaseCondition"/> by its value.
        /// </summary>
        /// <param name="value">The value to search for.</param>
        /// <returns>
        /// The <see cref="BaseCondition"/> if found; otherwise, <see langword="null"/>.
        /// </returns>
        public override BaseCondition GetBy(string value) => this.FirstOrDefault(condition => condition.Key == name);
    }
}
