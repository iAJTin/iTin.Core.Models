
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
/// Reference to visual setting of aggregate function of the data field.
/// </summary>
/// <remarks>
/// <para>Belongs to: <strong><c>Field</c></strong>, please see <see cref="DataField"/><br /></para>
/// - Or - <strong><c>Fixed</c></strong>, please see <see cref="FixedField"/><br />
/// - Or - <strong><c>Gap</c></strong>, please see <see cref="GapField"/><br /> 
/// - Or - <strong><c>Group</c></strong>, please see <see cref="GroupField"/><br />.
/// <para><u><strong>Usage</strong></u></para>
/// <code lang="xml" title="ITEE Object Element Usage">
/// <![CDATA[
/// <Aggrgate .../>
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
///       <td><see cref="Style"/></td>
///       <td align="center">No</td>
///       <td>Name of a style defined in the list of styles. The default is "<c>Default</c>".</td>
///     </tr>
///     <tr>
///       <td><see cref="Show"/></td>
///       <td align="center">Yes</td>
///       <td>Determines visibility of the element. The default is <see cref="YesNo.No"/>.</td>
///     </tr>
///     <tr>
///       <td><see cref="Location"/></td>
///       <td align="center">Yes</td>
///       <td>Preferred location in which to add the aggregate. The default is <see cref="KnownAggregateLocation.Top"/>.</td>
///     </tr>
///     <tr>
///       <td><see cref="AggregateType"/></td>
///       <td align="center">Yes</td>
///       <td>Preferred type of aggregate that will generate. The default is <see cref="KnownAggregateType.None"/>.</td>
///     </tr>
///     <tr>
///       <td><see cref="Text"/></td>
///       <td align="center">Yes</td>
///       <td>Preferred text for this aggregate. The default is "<c>Total</c>".</td>
///     </tr>
///   </tbody>
/// </table>
/// </remarks>
public partial class FieldAggregate
{
    #region private constants

    [DebuggerBrowsable(DebuggerBrowsableState.Never)] 
    private const string DefaultText = "Text";

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const YesNo DefaultShow = YesNo.No;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const string DefaultStyle = "Default";

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const KnownAggregateType DefaultAggregateType = KnownAggregateType.None;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const KnownAggregateLocation DefaultLocation = KnownAggregateLocation.Top;

    #endregion

    #region private members

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private YesNo _show;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string _style;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private KnownAggregateLocation _location;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private KnownAggregateType _aggregateType;

    #endregion

    #region constructor/s

    /// <summary>
    /// Initializes a new instance of the <see cref="FieldAggregate"/> class.
    /// </summary>
    public FieldAggregate()
    {
        Text = DefaultText;
        Show = DefaultShow;
        Style = DefaultStyle;
        Location = DefaultLocation;
        AggregateType = DefaultAggregateType;
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
    /// Gets or sets preferred location in which to add the aggregate.
    /// </summary>
    /// <value>
    /// One <see cref="KnownAggregateType"/> value. Preferred type of aggregate that will generate. The default is <see cref="KnownAggregateType.None"/>.
    /// </value>
    /// <remarks>
    /// <para><u><strong>Usage</strong></u></para>
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <Aggregate Type="None|Average|Count|Max|Min|Sum|Text" ...>
    /// ...
    /// </Aggregate>
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
    [XmlAttribute("Type")]
    [DefaultValue(DefaultAggregateType)]
    public KnownAggregateType AggregateType
    {
        get => _aggregateType;
        set
        {
            SentinelHelper.IsEnumValid(value);

            _aggregateType = value;
        }
    }

    /// <summary>
    /// Gets or sets preferred location in which to add the aggregate.
    /// </summary>
    /// <value>
    /// One <see cref="KnownAggregateLocation"/> value. Preferred location in which to add the aggregate. The default is <see cref="KnownAggregateLocation.Top"/>
    /// </value>
    /// <remarks>
    /// <para><u><strong>Usage</strong></u></para>
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <Aggregate Location="Top|Bottom" ...>
    /// ...
    /// </Aggregate>
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
    [DefaultValue(DefaultLocation)]
    public KnownAggregateLocation Location
    {
        get => _location;
        set
        {
            SentinelHelper.IsEnumValid(value);

            _location = value;
        }
    }

    /// <summary>
    /// Gets or sets a value that determines visibility of the element
    /// </summary>
    /// <value>
    /// <see cref="YesNo.Yes" /> if the item is displayed; otherwise, <strong><see cref="YesNo.No"/></strong>. The default is <see cref="YesNo.No" />.
    /// </value>
    /// <remarks>
    /// <para><u><strong>Usage</strong></u></para>
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <Aggregate Show="Yes|No" ...>
    /// ...
    /// </Aggregate>
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
    /// <Aggregate Style="string" ...>
    /// ...
    /// </Aggregate>
    /// ]]>
    /// </code>
    /// </remarks>
    /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/></exception>
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
                    new InvalidFieldIdentifierNameException(
                        ErrorMessageHelper.ModelIdentifierNameErrorMessage(
                            "FieldAggregate",
                            "Style",
                            value)));
            }

            _style = value;
        }
    }

    /// <summary>
    /// Gets or sets preferred text for this aggregate.
    /// </summary>
    /// <value>
    /// Preferred text for this aggregate. If aggregate is of text type then this attribute specifies preferred text for this aggregate. The default is "<c>Total</c>".
    /// </value>
    /// <remarks>
    /// <para><u><strong>Usage</strong></u></para>
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <Aggregate Text="string" ...>
    /// ...
    /// </Aggregate>
    /// ]]>
    /// </code>
    /// </remarks>           
    [XmlAttribute]
    [DefaultValue(DefaultText)]
    public string Text { get; set; }

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
//    Style.Equals(DefaultStyle) || 
//    Parent.Owner.Parent.Parent.Owner.Resources.Styles.Contains(Style);

///// <summary>
///// Gets a reference to the <see cref="T:iTin.Export.Model.StyleModel" /> from global resources.
///// </summary>
///// <returns>
///// <strong>true</strong> if returns the style from resource; otherwise, <strong>false</strong>.
///// </returns>
//public StyleModel GetStyle()
//{
//    var hasStyle = TryGetStyle(out var tempStyle);

//    return hasStyle ? tempStyle : StyleModel.Default;
//}

///// <summary>
///// Gets a reference to the image resource information.
///// </summary>
///// <param name="resource">Resource information.</param>
///// <returns>
///// <strong>true</strong> if exist available information about resource; otherwise, <strong>false</strong>.
///// </returns>
//public bool TryGetResourceInformation(out StyleModel resource)
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

//        result = true;
//    }
//    catch
//    {
//        result = false;
//    }

//    return result;
//}

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

//    var foudResource = TryGetResourceInformation(out var resource);
//    if (!foudResource)
//    {
//        return true;
//    }

//    style = resource;

//    return true;
//}

//#endregion
