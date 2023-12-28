
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Linq;
using System.Xml.Serialization;

using Newtonsoft.Json;

using iTin.Core.Helpers;
using iTin.Core.Models.ComponentModel.Exceptions;
using iTin.Core.Models.Design.Constants;
using iTin.Core.Models.Design.Enums;
using iTin.Core.Models.Helpers;

using ModelRegularExpressionHelper = iTin.Core.Models.Helpers.RegularExpressionHelper;

namespace iTin.Core.Models.Design.Table.Resource;

/// <summary>
/// Defines field name and a field separator of a group item.
/// </summary>
/// <remarks>
/// <para>
/// Belongs to: <strong><c>Group</c></strong>. For more information, please see <see cref="Group"/>.
/// <para><u><strong>Usage</strong></u></para>
/// <code lang="xml" title="ITEE Object Element Usage">
/// <![CDATA[
/// <?xml version="1.0" encoding="utf-8">
/// <Field .../>
/// ]]>
/// </code>
/// </para>
/// <para><strong><u>Attributes</u></strong></para>
/// <list type="table">
///  <listheader>
///   <term>Attribute</term>
///   <term>Optional</term>
///   <description>Description</description>
///  </listheader>
///  <item>
///   <term><see cref="Name"/></term>
///   <term>No</term>
///   <description>Name of the field.</description>
///  </item>
///  <item>
///   <term><see cref="Separator"/></term>
///   <term>Yes</term>
///   <description>Field separator. The default is "<c>None</c>".</description>
///  </item>
///  <item>
///   <term><see cref="Trim"/></term>
///   <term>Yes</term>
///   <description>Determines whether to apply string trim mode. The default <see cref="YesNo.No"/>.</description>
///  </item>
///  <item>
///   <term><see cref="TrimMode"/></term>
///   <term>Yes</term>
///   <description>Use this attribute to specify trim mode for strings. The default is <see cref="KnownTrimMode.All"/>.</description>
///  </item>
/// </list>
/// </remarks>
public partial class GroupItem
{
    #region private constants

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const string DefaultSeparator = "None";

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const YesNo DefaultTrim = YesNo.No;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const KnownTrimMode DefaultTrimMode = KnownTrimMode.All;

    #endregion

    #region private members
    
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string _name;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private YesNo _trim;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private KnownTrimMode _trimMode;

    #endregion

    #region constructor/s

    #region [public] GroupItem(): Initializes a new instance of the class
    /// <summary>
    /// Initializes a new instance of the <see cref="GroupItem" /> class.
    /// </summary>
    public GroupItem()
    {
        Trim = DefaultTrim;
        TrimMode = DefaultTrimMode;
        Separator = DefaultSeparator;
    }
    #endregion

    #endregion

    #region public readonly properties

    #region [public] (Group) Owner: Gets the element that owns this instance
    /// <summary>
    /// Gets the element that owns this <see cref="GroupItem"/>.
    /// </summary>
    /// <value>
    /// The <see cref="Group"/> that owns this <see cref="GroupItem"/>.
    /// </value>
    [JsonIgnore]
    [XmlIgnore]
    [Browsable(false)]
    public Group Owner { get; private set; }
    #endregion

    #endregion

    #region public properties

    #region [public] (XElement) DataSource: Gets or sets a reference to source data of group
    /// <summary>
    /// Gets or sets a reference to source data of group.
    /// </summary>
    /// <value>
    /// Source data of pieces.
    /// </value>
    [JsonIgnore]
    [XmlIgnore]
    public XElement DataSource { get; set; }
    #endregion

    #region [public] (string) Name: Gets or sets the name of the field
    /// <summary>
    /// Gets or sets the name of the field.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The name of the field.<br/>
    /// Are only allow strings made ​​up of letters, numbers and following special chars <strong>'<c>_ - # * @ % $</c>'</strong>.
    /// </para>
    /// <strong><u>Usage</u></strong>:
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <Field Name="string" .../>
    /// ]]>
    /// </code>
    /// </remarks>
    /// <exception cref="ArgumentNullException">If <paramref name="value" /> is <see langword="null"/>.</exception>
    /// <exception cref="InvalidFieldIdentifierNameException">If <paramref name="value" /> not is a valid field identifier name.</exception>
    [JsonProperty]
    [XmlAttribute]
    public string Name
    {
        get => _name;
        set
        {
            SentinelHelper.ArgumentNull(value, nameof(value));
            SentinelHelper.IsFalse(
                ModelRegularExpressionHelper.IsValidFieldName(value), 
                new InvalidFieldIdentifierNameException(ErrorMessageHelper.FieldIdentifierNameErrorMessage("Field", nameof(Name), value)));

            _name = value;
        }
    }
    #endregion

    #region [public] (string) Separator: Gets or sets the field separator
    /// <summary>
    /// Gets or sets the field separator.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The field separator.<br/>
    /// The default is "<c>None</c>".
    /// </para>
    /// <para>
    /// <c>ITEE</c> recognizes the following strings as valid separators:
    /// <list type="table">
    ///   <listheader><term>Value</term><description>Description</description></listheader>
    ///   <item><term>None</term><description>An empty string</description></item>
    ///   <item><term>Space</term><description>A whitespace</description></item>
    ///   <item><term>Dash</term><description>-</description></item>
    ///   <item><term>Dot</term><description>.</description></item>
    ///   <item><term>Comma</term><description>,</description></item>
    ///   <item><term>Colon</term><description>:</description></item>
    ///   <item><term>Semi Colon</term><description>;</description></item>
    ///   <item><term>New Line</term><description>A new line</description></item>
    ///   <item><term>Other value</term><description>Defined by user</description></item>
    /// </list>
    /// </para>
    /// <strong><u>Usage</u></strong>:
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <Field Separator="None|Space|Slash|Backslash|Dash|Dot|Comma|Colon|Semi Colon|New Line|string" .../>
    /// ]]>
    /// </code>
    /// <para><strong><u>Examples</u></strong>:</para>
    /// <example>
    /// The following example shows the use of the property.<br/> The new group consists of three fields separated by commas.
    /// <para><c>XML</c></para>
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <Groups>
    ///   <Group Name="AddressGroup">
    ///     <Field Name="CMADR1" Separator="Comma"/>
    ///     <Field Name="CMCITY" Separator="Comma"/>
    ///     <Field Name="CMPSTAL"/>
    ///   </Group>
    /// </Groups>
    /// ]]>
    /// </code>
    /// <para><c>C#</c></para>
    /// <code lang="cs">
    /// public void CreateGroup()
    /// {
    ///     Groups groups = new Groups();
    ///
    ///     Group addressGroup = new Group
    ///     {
    ///         Name = "AddressGroup",
    ///         Fields = new List&lt;GroupItem&gt;
    ///         {
    ///             new GroupItem { Name = "CMADR1", Separator = "Comma" },
    ///             new GroupItem { Name = "CMCITY", Separator = "Comma" },
    ///             new GroupItem { Name = "CMPSTAL" }
    ///         }
    ///     };
    /// 
    ///     addressGroup.SetOwner(groups);
    ///     groups.Items.Add(addressGroup);
    /// }
    /// </code>
    /// </example>
    /// <para>
    /// <c>ITEE</c> provides the <see cref="KnownItemGroupSeparator"/> static class, it contains list of constants with the known elements.
    /// </para>
    /// </remarks>
    [JsonProperty]
    [XmlAttribute]
    [DefaultValue(DefaultSeparator)]
    public string Separator { get; set; }
    #endregion

    #region [public] (YesNo) Trim: Gets or sets a value indicating whether to trim the blanks in this group field
    /// <summary>
    /// Gets or sets a value indicating whether to trim the blanks in this group field.<br/>
    /// The default is <see cref="YesNo.No" />
    /// </summary>
    /// <remarks>
    /// <para>
    /// <see cref="YesNo.Yes" /> to trim the blanks in this group field; otherwise, <see cref="YesNo.No" />. .
    /// </para>
    /// <strong><u>Usage</u></strong>:
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <Field Trim="Yes|No" .../>
    /// ]]>
    /// </code>
    /// </remarks>
    /// <exception cref="InvalidEnumArgumentException">
    /// <paramref name="value"/> is not part of the enumeration.<br/>
    /// 
    /// -or-<br/>
    /// 
    /// <paramref name="value"/> is not an enumerated type.
    /// </exception>
    [JsonProperty]
    [XmlAttribute]
    [DefaultValue(DefaultTrim)]
    public YesNo Trim
    {
        get => GetStaticBindingValue(_trim.ToString()).ToUpperInvariant() == "NO" ? YesNo.No : YesNo.Yes;
        set
        {
            SentinelHelper.IsEnumValid(value);

            _trim = value;
        }
    }
    #endregion

    #region [public] (KnownTrimMode) TrimMode: Gets or sets a value that determines trim mode for strings
    /// <summary>
    /// Gets or sets a value that determines trim mode for strings.<br/>
    /// The default is <see cref="KnownTrimMode.All"/>
    /// </summary>
    /// <remarks>
    /// <para>
    /// One of the <see cref="KnownTrimMode"/> values. .
    /// </para>
    /// <strong><u>Usage</u></strong>:
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <Field TrimMode="All|Start|End" .../>
    /// ]]>
    /// </code>
    /// </remarks>
    /// <exception cref="InvalidEnumArgumentException">
    /// <paramref name="value"/> is not part of the enumeration.<br/>
    /// 
    /// -or-<br/>
    /// 
    /// <paramref name="value"/> is not an enumerated type.
    /// </exception>
    [JsonProperty]
    [XmlAttribute]
    [DefaultValue(DefaultTrimMode)]
    public KnownTrimMode TrimMode
    {
        get => _trimMode;
        set
        {
            SentinelHelper.IsEnumValid(value);

            _trimMode = value;
        }
    }
    #endregion

    #endregion

    #region public methods

    #region [public] (string) GetValue(): Returns the value containing this piece
    /// <summary>
    /// Returns the value containing this piece.
    /// </summary>
    /// <returns>
    /// The piece value.
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown if <see cref="FixedItem.DataSource" /> is <strong>null</strong> or <see cref="FixedItem.Reference" /> not found.</exception>
    public string GetValue()
    {
        SentinelHelper.ArgumentNull(DataSource, ErrorMessage.DataSourceNotNull);

        var attribute = DataSource.Attribute(Name);
        switch (attribute)
        {
            case null:
                throw new ArgumentNullException(
                    Name, 
                    @"The specified field doesn't exist. Make sure that exist or is well written.");

            default:
            {
                var value = ParseValue(attribute.Value);

                return value;
            }
        }
    }
    #endregion

    #endregion

    #region private methods

    #region [public] (string) ParseValue(string): Parses input value and applies string trim mode
    /// <summary>
    /// Parses input value and applies string trim mode.
    /// </summary>
    /// <param name="value"><see cref="string"/> to parse.</param>
    /// <returns>
    /// The parsed value.
    /// </returns>
    private string ParseValue(string value)
    {
        if (Trim != YesNo.Yes)
        {
            return value;
        }

        var result = TrimMode switch
        {
            KnownTrimMode.All => value.Trim(),
            KnownTrimMode.Start => value.TrimStart(),
            KnownTrimMode.End => value.TrimEnd(),
            _ => value
        };

        return result;
    }
    #endregion

    #endregion
}
