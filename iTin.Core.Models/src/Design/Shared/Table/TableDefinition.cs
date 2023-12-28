
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
using iTin.Core.Models.Helpers;

using ModelRegularExpressionHelper = iTin.Core.Models.Helpers.RegularExpressionHelper;

namespace iTin.Core.Models.Design;

/// <summary>
/// Represents a model that defines the properties and configuration of a data table.
/// </summary>
/// <remarks>
/// <strong><u>Usage</u></strong>:
/// <code lang="xml" title="ITEE Object Element Usage">
/// <![CDATA[
/// <?xml version="1.0" encoding="utf-8">
/// <Table ...>
///   <Fields/>
///   <Resources/>
///   <References/>
/// </Table>
/// ]]>
/// </code>
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
///   <description>Name of the table.</description>
///  </item>
///  <item>
///   <term><see cref="Alias"/></term>
///   <term>Yes</term>
///   <description>Alias of the table.</description>
///  </item>
///  <item>
///   <term><see cref="Filter"/></term>
///   <term>Yes</term>
///   <description>Filter key to use. One of the keys defined in filters.</description>
///  </item>
///  <item>
///   <term><see cref="Show"/></term>
///   <term>Yes</term>
///   <description>Determines whether shows the table. The default is <see cref="YesNo.Yes"/>.</description>
///  </item>
///  <item>
///   <term><see cref="ShowColumnHeaders"/></term>
///   <term>Yes</term>
///   <description>Determines whether shows column headers.The default is <see cref="YesNo.Yes"/>.</description>
///  </item>
///  <item>
///   <term><see cref="ShowDataValues"/></term>
///   <term>Yes</term>
///   <description>Determines whether shows data values.The default is <see cref="YesNo.Yes"/>.</description>
///  </item>
/// </list>
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
///  <item>
///   <term><see cref="References"/></term>
///   <description> Collection of external references. Each element contains an external reference.</description>
///  </item>
///  <item>
///   <term><see cref="Resources"/></term>
///   <description>Resources associated with the table.</description>
///  </item>
/// </list>
/// </remarks>
public partial class TableDefinition : IModel
{
    #region private constants

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const string DefaultAlias = "";

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const YesNo DefaultShow = YesNo.Yes;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const YesNo DefaultShowColumnHeaders = YesNo.Yes;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const YesNo DefaultShowDataValues = YesNo.Yes;

    #endregion

    #region private members

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private FieldsCollection _fields;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string _filter;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string _name;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private ReferencesCollection _references;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private Resources _resources;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private YesNo _show;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private YesNo _showColumnHeaders;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private YesNo _showDataValues;

    #endregion

    #region constructor/s

    #region [public] TableDefinition(): Initializes a new instance of the class
    /// <summary>
    /// Initializes a new instance of the <see cref="TableDefinition"/> class.
    /// </summary>
    public TableDefinition()
    {
        Show = DefaultShow;
        Alias = DefaultAlias;
        ShowColumnHeaders = DefaultShowColumnHeaders;
    }
    #endregion

    #endregion

    #region interfaces

    #region IModel

    #region public properties

    #region [public] (string) Name: Gets or sets name of the table
    /// <summary>
    /// Gets or sets name of the table.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The name of the table.<br/>
    /// Are only allow strings made ​​up of letters, numbers and following special chars <strong>'<c>_ - # * @ % $</c>'</strong>.
    /// </para>
    /// <strong><u>Usage</u></strong>:
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <Table Name="string"...>
    ///   ...
    /// </Table>
    /// ]]>
    /// </code>
    /// <para><strong><u>Examples</u></strong>:</para>
    /// <para><c>C#</c></para>
    /// <code lang="cs">
    /// ...
    /// var table = new TableDefinition
    /// {
    ///     Name = "Sample name",
    ///     ...
    ///     ...
    /// };
    /// ...
    /// </code>
    /// <para><c>XML</c></para>
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// ...
    /// <Table Name="Sample name" ...>
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

            SentinelHelper.IsFalse(
                ModelRegularExpressionHelper.IsValidIdentifier(value), 
                new InvalidFieldIdentifierNameException(ErrorMessageHelper.ModelIdentifierNameErrorMessage("Table", nameof(Name), value)));

            _name = value;
        }
    }
    #endregion

    #region [public] (Resources) Resources: Gets or sets the resources associated with the table
    /// <summary>
    /// Gets or sets the resources associated with the table.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This property provides access to the resources associated with the table.<br/>
    /// Resources can include various settings and configurations that are used when rendering or exporting the table data.<br/>
    /// The resources can control aspects such as formatting, styling, and other visual properties of the table.
    /// </para>
    /// <strong><u>Usage</u></strong>:
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <Table ...>
    ///   <Resources>
    ///     <Filters />
    ///     <Fixed />
    ///     <Groups />
    ///     <Styles />
    ///   </Resources>
    /// </Table>
    /// ]]>
    /// </code>
    /// </remarks>
    [JsonProperty]
    [XmlElement]
    public Resources Resources
    {
        get => _resources ??= new Resources(this);
        set => _resources = value;
    }
    #endregion

    #region [public] (YesNo) Show: Gets or sets a value that determines whether displays this table
    /// <summary>
    /// Gets or sets a value that determines whether displays the table.<br/>
    /// The default is <see cref="YesNo.Yes"/>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// <see cref="YesNo.Yes"/> displays this <see cref="TableDefinition"/>; Otherwise, <see cref="YesNo.No"/>.
    /// </para>
    /// <strong><u>Usage</u></strong>:
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <Table Show="Yes|No" ...>
    ///   ...
    ///   ...
    /// </Table>
    /// ]]>
    /// </code>
    /// <para><strong><u>Examples</u></strong>:</para>
    /// <para><c>C#</c></para>
    /// <code lang="cs">
    /// ...
    /// var table = new TableDefinition
    /// {
    ///     Show = YesNo.Yes,
    ///     Name = "Sample name",
    ///     Alias = "Sample alias",
    ///     ...
    /// };
    /// ...
    /// </code>
    /// <para><c>XML</c></para>
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// ...
    /// <Table Show="Yes" ...>
    ///   ...
    /// </Table>
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
    [XmlAttribute]
    [JsonProperty]
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
    #endregion

    #endregion

    #endregion

    #endregion

    #region public readonly properties

    #region [public] (bool) HasFields: Gets a value indicating whether there are fields defined in the table
    /// <summary>
    /// Gets a value indicating whether there are fields defined in the table.
    /// </summary>
    /// <value>
    /// <see langword="true"/> if there are fields defined; otherwise, <see langword="false"/>.
    /// </value>
    [XmlIgnore]
    [JsonIgnore]
    public bool HasFields => Fields.Any();
    #endregion

    #region [public] (bool) ReferencesSpecified: Gets a value that tells the serializer if the referenced item is to be included
    /// <summary>
    /// Gets a value that tells the serializer if the referenced item is to be included.
    /// </summary>
    /// <value>
    /// <see langword="true"/> if the serializer has to include the element; otherwise, <see langword="false"/>.
    /// </value>
    [XmlIgnore]
    [JsonIgnore]
    [Browsable(false)]
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public bool ReferencesSpecified => !References.IsDefault;
    #endregion

    #region [public] (bool) ResourcesSpecified: Gets a value that tells the serializer if the referenced item is to be included
    /// <summary>
    /// Gets a value that tells the serializer if the referenced item is to be included.
    /// </summary>
    /// <value>
    /// <see langword="true"/> if the serializer has to include the element; otherwise, <see langword="false"/>.
    /// </value>
    [XmlIgnore]
    [JsonIgnore]
    [Browsable(false)]
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public bool ResourcesSpecified => !Resources.IsDefault;
    #endregion

    #endregion

    #region public properties

    #region [public] (string) Alias: Gets or sets the alias of the table
    /// <summary>
    /// Gets or sets the alias of the table.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The alias of the table. The default is an empty string ("").
    /// </para>
    /// <strong><u>Usage</u></strong>:
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <Table Alias="string"...>
    ///   ...
    /// </Table>
    /// ]]>
    /// </code>
    /// <para><strong><u>Examples</u></strong>:</para>
    /// <para><c>C#</c></para>
    /// <code lang="cs">
    /// ...
    /// var table = new TableDefinition
    /// {
    ///     Alias = "Sample alias",
    ///     ...
    ///     ...
    /// };
    /// ...
    /// </code>
    /// <para><c>XML</c></para>
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// ...
    /// <Table Alias="string"...>
    ///   ...
    /// </Table>
    /// ...
    /// ]]>
    /// </code>
    /// </remarks>
    [JsonProperty]
    [XmlAttribute]
    [DefaultValue(DefaultAlias)]
    public string Alias { get; set; }
    #endregion

    #region [public] (FieldsCollection) Fields: Gets or sets collection of data fields
    /// <summary>
    /// Gets or sets collection of data fields.
    /// </summary>
    /// <remarks>
    /// <para>
    ///  Collection of data fields. Each element is a data field..
    /// </para>
    /// <strong><u>Usage</u></strong>:
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <Table ...>
    ///   <Fields>
    ///   ...
    ///   ... 
    ///   </Fields>
    /// </Table>
    /// ]]>
    /// </code>
    /// <example>
    /// <para><strong><u>Examples</u></strong>:</para>
    /// <para><c>C#</c></para>
    /// <code lang="cs">
    /// ...
    /// var table = new TableDefinition
    /// {
    ///     Fields =
    ///     {
    ///         new DataField { Name = "DATE", Alias = "Date", Header = { Style = "DateHeaderStyle", Show = YesNo.Yes }, Value = { Style = "DateValueStyle" } },
    ///         new DataField { Name = "AUD", Alias = "AUD", Header = { Style = "HeaderStyle", Show = YesNo.Yes }, Value = { Style = "DecimalValueStyle" } }
    ///     },
    ///     ...
    /// };
    /// ...
    /// </code>
    /// <para><c>XML</c></para>
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// ...
    /// <Table Name="Sample name" ...>
    ///   <Fields>
    ///     <Field Name = "DATE" Alias="Date">
    ///       <Header Style = "DateHeaderStyle" />
    ///       <Value Style="DateValueStyle"/>
    ///     </Field>
    ///     <Field Name = "AUD" Alias="AUD">
    ///       <Header Style = "HeaderStyle" />
    ///       <Value Style="DecimalValueStyle"/>
    ///     </Field>
    ///   <Fields>
    ///   <Filter Field="AUD" Criterial="EqualTo" Value="6.17350" />
    /// </Table>
    /// ...
    /// ]]>
    /// </code>
    /// </example>
    /// </remarks>
    [JsonProperty("fields")]
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

    #region [public] (string) Filter: Gets or sets the data filter to apply
    /// <summary>
    /// Gets or sets the data filter to apply.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Filter key to use.<br/>
    /// One of the keys defined in filters.
    /// </para>
    /// <strong><u>Usage</u></strong>:
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <Table ...>
    ///   <Filter .../>
    /// </Table>
    /// ]]>
    /// </code>
    /// <para><strong><u>Examples</u></strong>:</para>
    /// <para><c>C#</c></para>
    /// <code lang="cs">
    /// ...
    /// var table = new TableDefinition
    /// {
    ///     Name = "Sample name",
    ///     Filter = "Filter key to use",
    ///     Fields =
    ///     {
    ///         new DataField { Name = "DATE", Alias = "Date", Header = { Style = "DateHeaderStyle", Show = YesNo.Yes }, Value = { Style = "DateValueStyle" } },
    ///         new DataField { Name = "AUD", Alias = "AUD", Header = { Style = "HeaderStyle", Show = YesNo.Yes }, Value = { Style = "DecimalValueStyle" } }
    ///     },
    ///     ...
    /// };
    /// ...
    /// </code>
    /// <para><c>XML</c></para>
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// ...
    /// <Table Name="Sample name" Filter="Filter key to use"...>
    ///   <Fields>
    ///     <Field Name = "DATE" Alias="Date">
    ///       <Header Style = "DateHeaderStyle" />
    ///       <Value Style="DateValueStyle"/>
    ///     </Field>
    ///     <Field Name = "AUD" Alias="AUD">
    ///       <Header Style = "HeaderStyle" />
    ///       <Value Style="DecimalValueStyle"/>
    ///     </Field>
    ///   <Fields>
    /// </Table>
    /// ...
    /// ]]>
    /// </code>
    /// </remarks>
    /// <exception cref="ArgumentException"><paramref name="value"/> is empty</exception>
    /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/></exception>
    [JsonProperty]
    [XmlAttribute]
    public string Filter
    {
        get => _filter;
        set
        {
            SentinelHelper.NotEmpty(value);
            SentinelHelper.ArgumentNull(value, nameof(value));

            _filter = value;
        }
    }
    #endregion

    #region [public] (YesNo) ShowColumnHeaders: Gets or sets a value that indicates whether column headers are shown in the table
    /// <summary>
    /// Gets or sets a value that indicates whether column headers are shown in the table.<br/>
    /// The default is <see cref="YesNo.Yes"/>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// <see cref="YesNo.Yes"/> if column headers are shown in the table; otherwise <see cref="YesNo.No"/>.
    /// </para>
    /// <strong><u>Usage</u></strong>:
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <Table ShowColumnHeaders="Yes|No" ...>
    ///   ...
    ///   ...
    /// </Table>
    /// ]]>
    /// </code>
    /// <para><strong><u>Examples</u></strong>:</para>
    /// <para><c>C#</c></para>
    /// <code lang="cs">
    /// ...
    /// var table = new TableDefinition
    /// {
    ///     Name = "Sample name",
    ///     Alias = "Sample alias",
    ///     ShowColumnHeaders = YesNo.Yes,
    ///     ...
    /// };
    /// ...
    /// </code>
    /// <para><c>XML</c></para>
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// ...
    /// <Table Name="Sample name" Alias="Sample alias" ShowColumnHeaders="Yes" ...>
    ///   ...
    /// </Table>
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
    #endregion

    #region [public] (YesNo) ShowDataValues: Gets or sets a value that indicates whether column headers are shown in the table
    /// <summary>
    /// Gets or sets a value that indicates whether the data is displayed.<br/>
    /// The default is <see cref="YesNo.Yes"/>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// <see cref="YesNo.Yes"/> if the data is displayed; otherwise, <see cref="YesNo.No"/>.
    /// </para>
    /// <strong><u>Usage</u></strong>:
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <Table ShowDataValues="Yes|No" ...>
    ///   ...
    ///   ...
    /// </Table>
    /// ]]>
    /// </code>
    /// <para><strong><u>Examples</u></strong>:</para>
    /// <para><c>C#</c></para>
    /// <code lang="cs">
    /// ...
    /// var table = new TableDefinition
    /// {
    ///     Name = "Sample name",
    ///     Alias = "Sample alias",
    ///     ShowColumnHeaders = YesNo.Yes,
    ///     ShowDataValues = YesNo.Yes,
    ///     ...
    /// };
    /// ...
    /// </code>
    /// <para><c>XML</c></para>
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// ...
    /// <Table Name="Sample name" Alias="Sample alias" ShowColumnHeaders="Yes" ShowDataValues="Yes" ...>
    ///   ...
    /// </Table>
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
    #endregion

    #region [public] (ReferencesCollection) References: Gets or sets the external references associated with the table
    /// <summary>
    /// Gets or sets the external references associated with the table.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This property provides access to the references associated with the table.<br/>
    /// References can include external resources, dependencies, or other elements that the table relies on.<br/>
    /// These references can be used for various purposes such as data sources, styles, or other configurations used by the table.
    /// </para>
    /// <strong><u>Usage</u></strong>:
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <Table ...>
    ///   <References>
    ///     <Reference .../>
    ///     <Reference .../>
    ///     ...
    ///   </Resources>
    /// </Table>
    /// ]]>
    /// </code>
    /// </remarks>
    [JsonProperty]
    [XmlArrayItem("Reference", typeof(Reference))]
    public ReferencesCollection References
    {
        get => _references ??= new ReferencesCollection(this);
        set => _references = value;
    }
    #endregion

    #endregion
}
