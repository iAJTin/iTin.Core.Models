
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Xml.Serialization;

using Newtonsoft.Json;

using iTin.Core.Helpers;
using iTin.Core.Models.ComponentModel.Exceptions;
using iTin.Core.Models.Design.Enums;
using iTin.Core.Models.Design.Table;
using iTin.Core.Models.Design.Table.Fields;
using iTin.Core.Models.Design.Table.Headers;
using iTin.Core.Models.Helpers;

using ModelRegularExpressionHelper = iTin.Core.Models.Helpers.RegularExpressionHelper;

namespace iTin.Core.Models.Design;

/// <summary>
/// Includes the description of a table.
/// </summary>
/// <remarks>
/// <code lang="xml" title="ITEE Object Element Usage">
/// <![CDATA[
/// <?xml version="1.0" encoding="utf-8">
/// <Table ...>
///   <Headers/>
///   <Fields/>
/// </Table>
/// ]]>
/// </code>
/// <para><strong><u>Attributes</u></strong></para>
/// <table>
///  <thead>
///   <tr>
///    <th>Attribute</th>
///    <th>Optional</th>
///    <th>Description</th>
///   </tr>
///  </thead>
///  <tbody>
///   <tr>
///    <td><see cref="Name"/></td>
///    <td align="center">No</td>
///    <td>Name of the table.</td>
///   </tr>
///   <tr>
///    <td><see cref="Alias"/></td>
///    <td align="center" > Yes </td>
///    <td> Alias of the table.The default is an empty string ("").</td>
///   </tr>
///   <tr>
///    <td><see cref="Filter"/></td>
///    <td align="center" > Yes </td>
///    <td>Filter to apply</td>
///   </tr>
///   <tr>
///    <td ><see cref="ShowColumnHeaders"/></td>
///    <td align="center"> Yes </td>
///    <td> Determines whether shows column headers.The default is <see cref="YesNo.Yes"/>.</td>
///   </tr>
///   <tr>
///    <td ><see cref="ShowDataValues"/></td>
///    <td align="center"> Yes </td>
///    <td> Determines whether shows data values.The default is <see cref="YesNo.Yes"/>.</td>
///   </tr>
///  </tbody>
/// </table>
/// <para><strong><u>Elements</u></strong></para>
/// <list type="table">
///  <listheader>
///   <term> Element </term>
///   <description>Description</description>
///  </listheader>
///  <item>
///   <term><see cref="Fields"/></term>
///   <description> Collection of data fields. Each element is a data field.</description>
///  </item>
/// </list>
/// </remarks>
public partial class TableDefinition : IParent
{
    #region private constants

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const string DefaultAlias = "";

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const YesNo DefaultShowColumnHeaders = YesNo.Yes;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const YesNo DefaultShowDataValues = YesNo.Yes;

    #endregion

    #region private members

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private IColumnsHeaders _headers;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private YesNo _showColumnHeaders;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string _name;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private IFilter _filter;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private FieldsCollection _fields;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private IStyles _styles;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private YesNo _showDataValues;

    #endregion

    #region constructor/s

    /// <summary>
    /// Initializes a new instance of the <see cref="Table"/> class.
    /// </summary>
    public TableDefinition()
    {
        Alias = DefaultAlias;
        ShowColumnHeaders = DefaultShowColumnHeaders;
    }

    #endregion

    #region public readonly properties

    /// <summary>
    /// Gets a value indicating whether there are fields defined.
    /// </summary>
    /// <value>
    /// <strong>true</strong> if there are fields defined; otherwise, <strong>false</strong>.
    /// </value>
    public bool HasFields => Fields.Any();

    #endregion

    #region public properties

    /// <summary>
    /// Gets or sets the alias of the table.
    /// </summary>
    /// <value>
    /// The alias of the table. The default is an empty string ("").
    /// </value>
    /// <remarks>
    /// <strong><u>Usage</u></strong>:
    /// <para><strong>C#</strong></para>
    /// <code lang="cs">
    /// ...
    /// var table = new Table
    /// {
    ///     Alias = "Sample alias",
    ///     ...
    ///     ...
    /// };
    /// ...
    /// </code>
    /// <para><strong>XML</strong></para>
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// ...
    /// <Table Alias="string"...>
    ///   ...
    /// </Table>
    /// ]]>
    /// </code>
    /// </remarks>
    [JsonProperty]
    [XmlAttribute]
    [DefaultValue(DefaultAlias)]
    public string Alias { get; set; }

    /// <summary>
    /// Gets or sets name of the table.
    /// </summary>
    /// <value>
    /// The name of the table.
    /// <para>
    /// Are only allow strings made ​​up of letters, numbers and following special chars <strong>'<c>_ - # * @ % $</c>'</strong>
    /// </para>
    /// </value>
    /// <remarks>
    /// <strong><u>Usage</u></strong>:
    /// <para><strong>C#</strong></para>
    /// <code lang="cs">
    /// ...
    /// var table = new TableModel
    /// {
    ///     Name = "Sample name",
    ///     ...
    ///     ...
    /// };
    /// ...
    /// </code>
    /// <para><strong>XML</strong></para>
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// ...
    /// <Table Name="string" ...>
    ///   ...
    /// </Table>
    /// ]]>
    /// </code>
    /// </remarks>
    /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/></exception>
    /// <exception cref="InvalidFieldIdentifierNameException"><paramref name="value"/> is an invalid identifier.</exception>
    [JsonProperty]
    [XmlAttribute]
    public string Name
    {
        get => _name;
        set
        {
            SentinelHelper.ArgumentNull(value, nameof(value));

            SentinelHelper.IsFalse(ModelRegularExpressionHelper.IsValidIdentifier(value), new InvalidFieldIdentifierNameException(ErrorMessageHelper.ModelIdentifierNameErrorMessage("Table", "Name", value)));

            _name = value;
        }
    }

    /// <summary>
    /// Gets or sets the data filter to apply.
    /// </summary>
    /// <value>
    /// 
    /// </value>
    [JsonProperty]
    [XmlAttribute]
    public IFilter Filter
    {
        get => _filter ??= new BaseFilter();
        set => _filter = value;
    }

    /// <summary>
    /// Gets or sets the data filter to apply.
    /// </summary>
    /// <value>
    /// 
    /// </value>
    [JsonProperty]
    [XmlAttribute]
    public IStyles Styles
    {
        get;
        set;
    }

    /// <summary>
    /// Gets or sets the show column headers.
    /// </summary>
    /// <value>
    /// The show column headers.
    /// </value>
    /// <exception cref="InvalidEnumArgumentException">
    /// <paramref name="value"/> is not part of the enumeration.
    /// 
    /// -or-
    /// 
    /// <paramref name="value"/> is not an enumerated type.
    /// </exception>
    [JsonProperty]
    [XmlAttribute]
    [DefaultValue(DefaultShowColumnHeaders)]
    public YesNo ShowColumnHeaders
    {
        get => GetStaticBindingValue(_showColumnHeaders.ToString()).ToUpperInvariant() == "NO" ? YesNo.No : YesNo.Yes;
        set
        {
            SentinelHelper.IsEnumValid(value);

            _showColumnHeaders = value;
        }
    }

    /// <summary>
    /// Gets or sets a value that indicates whether the data is displayed.
    /// </summary>
    /// <value>
    /// <see cref="YesNo.Yes"/> if the data is displayed; otherwise, <see cref="YesNo.No"/>.
    /// </value>
    /// <exception cref="InvalidEnumArgumentException">
    /// <paramref name="value"/> is not part of the enumeration.
    /// 
    /// -or-
    /// 
    /// <paramref name="value"/> is not an enumerated type.
    /// </exception>
    [JsonProperty]
    [XmlAttribute]
    [DefaultValue(DefaultShowDataValues)]
    public YesNo ShowDataValues
    {
        get => GetStaticBindingValue(_showDataValues.ToString()).ToUpperInvariant() == "NO" ? YesNo.No : YesNo.Yes;
        set
        {
            SentinelHelper.IsEnumValid(value);

            _showDataValues = value;
        }
    }

    ///// <summary>
    ///// Gets or sets the headers.
    ///// </summary>
    ///// <value>
    ///// The headers.
    ///// </value>
    //[XmlArrayItem("Header", typeof(BaseColumnHeader))]
    //public virtual IColumnsHeaders Headers
    //{
    //    get => _headers ??= new ColumnsHeadersCollection(this);
    //    set => _headers = value;
    //}

    /// <summary>
    /// Gets or sets collection of data fields.
    /// </summary>
    /// <value>
    /// Collection of data fields. Each element is a data field.
    /// </value>
    /// <remarks>
    /// <strong><u>Usage</u></strong>:
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <Table ...>
    ///   <Fields .../>
    /// </Table>
    /// ]]>
    /// </code>
    /// </remarks>
    /// <example>
    /// <para><strong>C#</strong></para>
    /// <code lang="cs">
    /// ...
    /// var table = new Table
    /// {
    ///     Fields = 
    ///     ...
    ///     ...
    /// };
    /// ...
    /// </code>
    /// <para><strong>XML</strong></para>
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// ...
    /// <Table ...>
    ///   <Fields .../>
    /// </Table>
    /// ]]>
    /// </code>
    /// </example>
    [XmlArrayItem("Field", typeof(DataField))]
    [XmlArrayItem("Fixed", typeof(FixedField))]
    [XmlArrayItem("Gap", typeof(GapField))]
    [XmlArrayItem("Group", typeof(GroupField))]
    [XmlArrayItem("Packet", typeof(PacketField))]
    public FieldsCollection Fields
    {
        get => _fields ??= new FieldsCollection(this);
        set => _fields = value;
    }

    #endregion
}
