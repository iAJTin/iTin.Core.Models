
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

using iTin.Core.Helpers;
using iTin.Core.Models.Collections;
using iTin.Core.Models.Design.Enums;
using iTin.Core.Models.Design.Table.Fields;

namespace iTin.Core.Models.Design.Table
{
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [GeneratedCode("System.Xml", "4.0.30319.18033")]
    [XmlType(Namespace = "http://schemas.iTin.com/style/v1.0")]
    public partial class FieldsCollection : BaseComplexModelCollection<BaseDataField, TableDefinition, string>
    {
        /// <summary>
        /// Returns specified field by identifier.
        /// </summary>
        /// <param name="value">The field to locate on this collection" /&gt;.</param>
        /// <returns>
        /// <b>true</b> if <paramref name="value" /> is found in this collection; otherwise, <b>false</b>.
        /// </returns>
        public override BaseDataField GetBy(string value)
        {
            var fieldIndex = -1;
            BaseDataField item = null;

            foreach (var field in this)
            {
                var found = false;

                var fieldType = field.FieldType;
                switch (fieldType)
                {
                    case KnownFieldType.Field:
                        if (((DataField)field).Name.Equals(value, StringComparison.Ordinal))
                        {
                            found = true;
                            fieldIndex = IndexOf(field);
                        }

                        break;

                    case KnownFieldType.Fixed:
                        if (((FixedField)field).Piece.Equals(value, StringComparison.Ordinal))
                        {
                            found = true;
                            fieldIndex = IndexOf(field);
                        }

                        break;

                    case KnownFieldType.Gap:
                        if (field.Alias.Equals(value, StringComparison.Ordinal))
                        {
                            found = true;
                            fieldIndex = IndexOf(field);
                        }

                        break;

                    case KnownFieldType.Group:
                        if (((GroupField)field).Name.Equals(value, StringComparison.Ordinal))
                        {
                            found = true;
                            fieldIndex = IndexOf(field);
                        }

                        break;

                    case KnownFieldType.Packet:
                        if (((PacketField)field).Name.Equals(value, StringComparison.Ordinal))
                        {
                            found = true;
                            fieldIndex = IndexOf(field);
                        }

                        break;
                }

                if (found)
                {
                    break;
                }
            }

            if (fieldIndex != -1)
            {
                item = this[fieldIndex];
            }

            return item;
        }


        /// <summary>
        /// Sets this collection as the owner of the specified item.
        /// </summary>
        /// <param name="item">Target item to set owner.</param>
        protected override void SetOwner(BaseDataField item)
        {
            SentinelHelper.ArgumentNull(item, nameof(item));

            item.SetOwner(this);
        }
    }
}
