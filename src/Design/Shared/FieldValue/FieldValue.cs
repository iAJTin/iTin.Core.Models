
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

using iTin.Core.Helpers;
using iTin.Core.Models.ComponentModel.Exceptions;
using iTin.Core.Models.Design.Enums;
using iTin.Core.Models.Design.Table.Fields;
using iTin.Core.Models.Helpers;

using ModelsRegularExpressionHelper = iTin.Core.Models.Helpers.RegularExpressionHelper;

namespace iTin.Core.Models.Design;

/// <summary>
/// Reference to visual setting of value of the data field.
/// </summary>
/// <remarks>
/// <para>Belongs to: <strong><c>Field</c></strong>, please see <see cref="DataField"/><br/></para>
/// - Or - <strong><c>Fixed</c></strong>, please see <see cref="FixedField"/><br/>
/// - Or - <strong><c>Gap</c></strong>, please see <see cref="GapField"/><br/> 
/// - Or - <strong><c>Group</c></strong>, please see <see cref="GroupField"/><br/>
/// - Or - <strong><c>Packet</c></strong>, please see <see cref="PacketField"/><br/>.
/// <para><u><strong>Usage</strong></u></para>
/// <code lang="xml" title="ITEE Object Element Usage">
/// <![CDATA[
/// <Value .../>
/// ]]>
/// </code>
/// <para><strong><u>Attributes</u></strong></para>
/// <table>
///   <thead>
///     <tr>
///       <th>Attribute</th>
///       <th>Optional</th>
///       <th>Description</th>
///       </tr>
///   </thead>
///   <tbody>
///     <tr>
///       <td><see cref="FieldAggregate.Style" /></td>
///       <td align="center">No</td>
///       <td>Name of a style defined in the list of styles. The default is "<c>Default</c>".</td>
///     </tr>
///     <tr>
///       <td><see cref="FieldAggregate.Show" /></td>
///       <td align="center">Yes</td>
///       <td>Determines visibility of the element. The default is <see cref="YesNo.No"/>.</td>
///     </tr>
///   </tbody>
/// </table>
/// </remarks>
public partial class FieldValue
{
    #region private constants

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const YesNo DefaultShow = YesNo.Yes;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const string DefaultStyle = "Default";

    #endregion

    #region private members

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private YesNo _show;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string _style;

    #endregion

    #region constructor/s

    /// <summary>
    /// Initializes a new instance of the <see cref="FieldValue"/> class.
    /// </summary>
    public FieldValue()
    {
        Show = DefaultShow;
        Style = DefaultStyle;
    }

    #endregion

    #region public readonly properties

    /// <summary>
    /// Gets the parent element of the element.
    /// </summary>
    /// <value>
    /// The element that represents the container element of the element.
    /// </value>
    [XmlIgnore]
    [Browsable(false)]
    public BaseDataField Parent { get; private set; }

    #endregion

    #region public properties

    /// <summary>
    /// Gets or sets a value that determines visibility of the element.
    /// </summary>
    /// <value>
    /// <see cref="YesNo.Yes"/> if the item is displayed; otherwise, <strong><see cref="YesNo.No"/></strong>. The default is <see cref="YesNo.No"/>.
    /// </value>
    /// <remarks>
    /// <para><u><strong>Usage</strong></u></para>
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <Value Show="Yes|No" ...>
    /// ...
    /// </Value>
    /// ]]>
    /// </code>
    /// </remarks>
    /// <exception cref="InvalidEnumArgumentException">
    /// <paramref name="value"/> is not part of the enumeration.
    /// 
    /// -or-
    /// 
    /// <paramref name="value"/> is not an enumerated type.
    /// </exception>
    [XmlAttribute]
    [DefaultValue(DefaultShow)]
    public YesNo Show
    {
        get => GetStaticBindingValue(_show.ToString()).ToUpperInvariant() == "NO" ? YesNo.No : YesNo.Yes;
        set
        {
            SentinelHelper.IsEnumValid(value);

            _show = value;
        }
    }

    /// <summary>
    /// Gets or sets one of the styles defined in the element styles.
    /// </summary>
    /// <value>
    /// Name of a style defined in the list of styles. The default is "<c>Default</c>".
    /// Are only allow strings made ​​up of letters, numbers and following special chars <strong>'<c>_ - # @ % $</c>'</strong>.
    /// </value>
    /// <remarks>
    /// <para><u><strong>Usage</strong></u></para>
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <Value Style="string" ...>
    /// ...
    /// </Value>
    /// ]]>
    /// </code>
    /// </remarks>
    /// <exception cref="ArgumentNullException">If <paramref name="value" /> is <strong>null</strong>.</exception>
    /// <exception cref="InvalidIdentifierNameException"><paramref name="value" /> is not a valid identifier name.</exception>
    [XmlAttribute]
    [DefaultValue(DefaultStyle)]
    public string Style
    {
        get => GetStaticBindingValue(_style);
        set
        {
            SentinelHelper.ArgumentNull(value, nameof(value));

            var isBinded = ModelsRegularExpressionHelper.IsStaticBindingResource(value);
            if (!isBinded)
            {
                SentinelHelper.IsFalse(
                    ModelsRegularExpressionHelper.IsValidIdentifier(value), 
                    new InvalidIdentifierNameException(
                        ErrorMessageHelper.ModelIdentifierNameErrorMessage(
                            "FieldValue", 
                            "Style", 
                            value)));
            }

            _style = value;
        }
    }

    #endregion

    #region internal methods

    /// <summary>
    /// Sets the parent element of the element.
    /// </summary>
    /// <param name="reference">Reference to parent.</param>
    internal void SetParent(BaseDataField reference)
    {
        Parent = reference;
    }

    #endregion
}

//#region public methods

///// <summary>
///// Performs a test for check if there this name of the style into the user-defined styles list.
///// </summary>
///// <returns>
///// <strong>true</strong> if exist; otherwise, <strong>false</strong>.
///// </returns>
//public bool CheckStyleName() => 
//    Style.Equals("Default") || 
//    Parent.Owner.Parent.Parent.Owner.Resources.Styles.Contains(Style);

///// <summary>
///// Gets the value.
///// </summary>
///// <returns>
///// The value
///// </returns>
//public FieldValueInformation GetValue() => GetValue(null);

///// <summary>
///// Gets the value.
///// </summary>
///// <param name="specialChars">The special chars.</param>
///// <returns>
///// The value
///// </returns>
//public FieldValueInformation GetValue(IEnumerable<char> specialChars)
//{
//    //// return FieldValueInformationCache.Cache.Get(Parent, specialChars);

//    var value = FieldValueInformation.Default;
//    var unformattedValue = string.Empty;

//    var specialCharsList = new List<char>();
//    if (specialChars != null)
//    {
//        specialCharsList = specialChars.ToList();
//    }

//    if (Show == YesNo.No)
//    {
//        return value;
//    }

//    var found = Parent.Value.TryGetStyle(out var style);
//    if (!found)
//    {
//        // Mensaje de Log usando default style; 
//        style = StyleModel.Default;
//    }

//    value.Style = style;

//    if (Parent.DataSource == null)
//    {
//        return value;
//    }

//    var fieldType = Parent.FieldType;
//    switch (fieldType)
//    {
//        #region Field: Data
//        case KnownFieldType.Field:
//        {
//            var current = (DataField)Parent;
//            var parsedName = BaseProvider.Parse(current.Name, specialCharsList);

//            var fieldAsAttribute = Parent.DataSource.Attribute(parsedName);
//            if (fieldAsAttribute == null)
//            {
//                fieldAsAttribute = Parent.DataSource.Attribute(parsedName.ToUpperInvariant());
//                if (fieldAsAttribute == null)
//                {
//                    fieldAsAttribute = Parent.DataSource.Attribute(parsedName.ToLowerInvariant());
//                }                                
//            }

//            if (fieldAsAttribute != null)
//            {
//                unformattedValue = fieldAsAttribute.Value;
//            }
//        }

//            break;
//        #endregion

//        #region Field: Fixed
//        case KnownFieldType.Fixed:
//        {
//            var current = (FixedField)Parent;

//            var @fixed = Parent.Owner.Parent.Parent.Resources.Fixed;
//            var fixedItem = @fixed[current.Pieces];
//            fixedItem.DataSource = Parent.DataSource;

//            var parsedName = BaseProvider.Parse(current.Piece, specialCharsList);
//            var piece = fixedItem.Pieces[parsedName];
//            unformattedValue = piece.GetValue();
//        }

//            break;
//        #endregion

//        #region Field: Group
//        case KnownFieldType.Group:
//        {
//            var current = (GroupField)Parent;
//            var currentName = current.Name;

//            var @fixed = Parent.Owner.Parent.Parent.Owner.Resources.Fixed;
//            var groups = Parent.Owner.Parent.Parent.Owner.Resources.Groups;
//            var groupValue = string.Empty;
//            var builder = new StringBuilder();

//            var group = groups[currentName];
//            var groupFields = group.Fields;
//            foreach (var groupField in groupFields)
//            {
//                var parsedName = BaseProvider.Parse(groupField.Name, specialCharsList);
//                var asAttribute = Parent.DataSource.Attribute(parsedName);
//                if (asAttribute == null)
//                {
//                    foreach (var fixedwidth in @fixed)
//                    {
//                        fixedwidth.DataSource = Parent.DataSource;

//                        var piece = fixedwidth.Pieces[groupField.Name];
//                        if (piece == null)
//                        {
//                            continue;
//                        }

//                        groupValue = piece.GetValue();
//                    }
//                }
//                else
//                {
//                    groupField.DataSource = Parent.DataSource;
//                    groupValue = groupField.GetValue(); //asAttribute.Value;
//                }

//                builder.Append(groupValue);
//                builder.Append(GroupField.GetSeparatorChar(groupField.Separator));
//            }

//            unformattedValue = builder.ToString();
//        }

//            break;
//        #endregion

//        #region Field: Packet
//        case KnownFieldType.Packet:
//        {
//            var current = (PacketField)Parent;
//            var parsedName = BaseProvider.Parse(current.Name, specialCharsList);

//            var fieldAsAttribute = Parent.DataSource.Attribute(parsedName);
//            if (fieldAsAttribute != null)
//            {
//                var builder = new StringBuilder();
//                builder.Clear();

//                var inputFormat = current.InputFormat;
//                var fieldvalue = fieldAsAttribute.Value;
//                switch (inputFormat)
//                {
//                    #region InputFormat: FullDateFormat
//                    case KnownInputPacketFormat.FullDateFormat:
//                        if (!string.IsNullOrEmpty(fieldvalue) &&
//                            !fieldvalue.Trim().Equals("0"))
//                        {
//                            var adjustedValue = string.Concat(new string('0', 14), fieldvalue);
//                            adjustedValue = adjustedValue.Substring(adjustedValue.Length - 14, 14);

//                            builder.Append(adjustedValue.Substring(6, 2));
//                            builder.Append('/');
//                            builder.Append(adjustedValue.Substring(4, 2));
//                            builder.Append('/');
//                            builder.Append(adjustedValue.Substring(0, 4));
//                            builder.Append(' ');
//                            builder.Append(adjustedValue.Substring(8, 2));
//                            builder.Append(':');
//                            builder.Append(adjustedValue.Substring(10, 2));
//                            builder.Append(':');
//                            builder.Append(adjustedValue.Substring(12, 2));
//                        }
//                        break;
//                    #endregion

//                    #region InputFormat: LongDateFormat
//                    case KnownInputPacketFormat.LongDateFormat:
//                        if (!string.IsNullOrEmpty(fieldvalue) &&
//                            !fieldvalue.Trim().Equals("0"))
//                        {
//                            var adjustedValue = string.Concat(new string('0', 8), fieldvalue);
//                            adjustedValue = adjustedValue.Substring(adjustedValue.Length - 8, 8);

//                            builder.Append(adjustedValue.Substring(0, 4));
//                            builder.Append('/');
//                            builder.Append(adjustedValue.Substring(4, 2));
//                            builder.Append('/');
//                            builder.Append(adjustedValue.Substring(6, 2));
//                        }
//                        break;
//                    #endregion

//                    #region InputFormat: ShortDateFormat
//                    case KnownInputPacketFormat.ShortDateFormat:
//                        if (!string.IsNullOrEmpty(fieldvalue) &&
//                            !fieldvalue.Trim().Equals("0"))
//                        {
//                            var adjustedValue = string.Concat(new string('0', 6), fieldvalue);
//                            adjustedValue = adjustedValue.Substring(adjustedValue.Length - 6, 6);

//                            builder.Append(adjustedValue.Substring(0, 2));
//                            builder.Append('/');
//                            builder.Append(adjustedValue.Substring(2, 2));
//                            builder.Append('/');
//                            builder.Append(adjustedValue.Substring(4, 2));
//                        }
//                        break;
//                    #endregion
//                }

//                unformattedValue = builder.ToString();
//            }
//        }

//            break;
//        #endregion
//    }

//    return style.Content.DataType.GetFormattedDataValue(unformattedValue);
//}

//#endregion


///// <summary>
///// Gets a reference to the <see cref="T:iTin.Export.Model.StyleModel" /> from global resources.
///// </summary>
///// <param name="style">A <see cref="T:iTin.Export.Model.StyleModel" /> resource.</param>
///// <returns>
///// <strong>true</strong> if returns the style from resource; otherwise, <strong>false</strong>.
///// </returns>
//public bool TryGetStyle(out StyleModel style)
//{
//    style = StyleModel.Empty;

//    var found = TryGetResourceInformation(out var resource);
//    if (!found)
//    {
//        return false;
//    }

//    style = resource;

//    return true;
//}

//#region private methods

///// <summary>
///// Gets a reference to the image resource information.
///// </summary>
///// <param name="resource">Resource information.</param>
///// <returns>
///// <strong>true</strong> if exist available information about resource; otherwise, <strong>false</strong>.
///// </returns>
//private bool TryGetResourceInformation(out StyleModel resource)
//{
//    bool result;

//    resource = StyleModel.Empty;
//    if (string.IsNullOrEmpty(Style))
//    {
//        return false;
//    }

//    try
//    {
//        var field = Parent;
//        var fields = field.Owner;
//        var table = fields.Parent;
//        var export = table.Parent;
//        resource = export.Resources.GetStyleResourceByName(Style);

//        result = resource != null;
//    }
//    catch
//    {
//        result = false;
//    }

//    return result;
//}

//#endregion
