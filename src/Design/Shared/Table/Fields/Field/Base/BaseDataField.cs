
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Linq;
using System.Xml.Serialization;

using Newtonsoft.Json;

using iTin.Core.Helpers;
using iTin.Core.Models.Design.Enums;

namespace iTin.Core.Models.Design.Table.Fields;

/// <summary>
/// Base class for the different types of data fields supported by <strong><c>iTin Export Engine</c></strong>.<br/>
/// Which acts as the base class for different data fields.
/// </summary>
/// <remarks>
/// <para>The following table shows different data fields.</para>
/// <list type="table">
///   <listheader>
///     <term>Class</term>
///     <description>Description</description>
///   </listheader>
///   <item>
///     <term><see cref="DataField"/></term>
///     <description>Represents a data field.</description>
///   </item>
///   <item>
///     <term><see cref="FixedField"/> (Packet field)</term>
///     <description>Represents a piece of a field fixed-width data.</description>
///   </item>
///   <item>
///     <term><see cref="GapField"/></term>
///     <description>Represents an empty data field.</description>
///   </item>
///   <item>
///     <term><see cref="GroupField"/> (inherits of <see cref="DataField"/>)</term>
///     <description>Represents a field as union of several data field.</description>
///   </item>
/// </list>
/// </remarks>
public partial class BaseDataField
{
    #region private constants

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const string WidthDefault = "Default";

    #endregion

    #region private members

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string _width;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private XElement _dataSource;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private FieldValue _value;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private FieldHeader _header;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private FieldAggregate _aggregate;

    #endregion

    #region constructor/s

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseDataField"/> class.
    /// </summary>
    protected BaseDataField()
    {
        Width = WidthDefault;
    }

    #endregion

    #region public static methods

    /// <summary>
    /// Gets the name of field.
    /// </summary>
    /// <param name="field">The field.</param>
    /// <returns>
    /// Returns name of specified field.
    /// </returns>
    public static string GetFieldNameFrom(BaseDataField field) =>
        field.FieldType switch
        {
            KnownFieldType.Field => ((DataField)field).Name,
            KnownFieldType.Group => ((GroupField)field).Name,
            KnownFieldType.Fixed => ((FixedField)field).Piece,
            KnownFieldType.Packet => ((PacketField)field).Name,
            KnownFieldType.Gap => string.Empty,
            _ => string.Empty
        };

    #endregion

    #region public abstract readonly properties

    /// <summary>
    /// Gets a value indicating data field type.
    /// </summary>
    /// <value>
    /// One of the <see cref="KnownFieldType" /> values.
    /// </value>
    [JsonIgnore]
    [XmlIgnore]
    public abstract KnownFieldType FieldType { get; }

    #endregion

    #region public readonly properties

    /// <summary>
    /// Gets the element that owns this data field.
    /// </summary>
    /// <value>
    /// The <see cref="FieldsCollection" /> that owns this data field.
    /// </value>
    [JsonIgnore]
    [XmlIgnore]
    [Browsable(false)]
    public FieldsCollection Owner { get; private set; }

    #endregion

    #region public properties

    /// <summary>
    /// Gets or sets a reference that contains the visual setting of aggregate function of the data field.
    /// </summary>
    /// <value>
    /// Visual setting of aggregate function of the data field.
    /// </value>
    /// <remarks>
    /// <para><strong><u>Usage</u></strong>:</para>
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <Field|Fixed|Gap|Group ...>
    ///   <Aggregate .../>
    ///   ...
    /// <Field|Fixed|Gap|Group>
    /// ]]>
    /// </code>
    /// </remarks>
    /// <example>
    /// In the following example shows how create a data field.
    /// <para><strong><u>Usage</u></strong>:</para>
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <Field Name="##LINE" Alias="Line">
    ///     <Header Style="CommonHeader" Show="Yes"/>
    ///     <Value Style="LineValue"/>
    ///     <Aggregate Style="TopAggregate" Type="Count" Location="Top" Show="Yes"/>
    /// </Field>
    /// ]]>
    /// </code>
    /// <code lang="cs">
    /// DataField lineField = new DataField
    /// {
    ///     Name = "##LINE",
    ///     Alias = "Line",
    ///     Value = new FieldValue { Style = "LineValue" },
    ///     Header = new FieldHeader { Style = "CommonHeader", Show = YesNo.Yes },
    ///     Aggregate = new FieldAggregate
    ///     {
    ///         Show = YesNo.Yes,
    ///         Style = "TopAggregate",
    ///         Location = KnownAggregateLocation.Top,
    ///         AggregateType = KnownAggregateType.Count
    ///     }
    /// };
    /// </code>
    /// </example>
    [JsonProperty]
    [XmlElement]
    public FieldAggregate Aggregate
    {
        get
        {
            _aggregate ??= new FieldAggregate();
            _aggregate.SetParent(this);

            return _aggregate;
        }
        set => _aggregate = value;
    }

    /// <summary>
    /// Gets or sets the alias of data field.
    /// </summary>
    /// <value>
    /// The alias of data field. This value is used as the column header.
    /// </value>
    /// <remarks>
    /// <para><strong><u>Usage</u></strong>:</para>
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <Field|Fixed|Gap|Group Alias="string" ...>
    /// ...
    /// </Field|Fixed|Gap|Group>
    /// ]]>
    /// </code>
    /// </remarks>
    /// <example>
    /// In the following example shows how create a data field.
    /// <para><strong><u>Usage</u></strong>:</para>
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <Field Name="##LINE" Alias="Line">
    ///     <Header Style="CommonHeader" Show="Yes"/>
    ///     <Value Style="LineValue"/>
    ///     <Aggregate Style="TopAggregate" Type="Count" Location="Top" Show="Yes"/>
    /// </Field>
    /// ]]>
    /// </code>
    /// <code lang="cs">
    /// DataField lineField = new DataField
    /// {
    ///     Name = "##LINE",
    ///     Alias = "Line",
    ///     Value = new FieldValue { Style = "LineValue" },
    ///     Header = new FieldHeader { Style = "CommonHeader", Show = YesNo.Yes },
    ///     Aggregate = new FieldAggregate
    ///     {
    ///         Show = YesNo.Yes,
    ///         Style = "TopAggregate",
    ///         Location = KnownAggregateLocation.Top,
    ///         AggregateType = KnownAggregateType.Count
    ///     }
    /// };
    /// </code>
    /// </example>
    [JsonProperty]
    [XmlAttribute]
    public string Alias { get; set; }

    /// <summary>
    /// Gets or sets the <strong>XML</strong> data source for the field value.
    /// </summary>
    /// <value>
    /// The <strong>XML</strong> data source element associated with the field value.
    /// </value>
    /// <remarks>
    /// This property provides access to the <strong>XML</strong> data source element that holds the value associated with this field.
    /// </remarks>
    [JsonIgnore]
    [XmlIgnore]
    public XElement DataSource
    {
        get => _dataSource;
        set
        {
            if (CanSetData)
            {
                _dataSource = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets a reference that contains the visual setting of header the data field.
    /// </summary>
    /// <value>
    /// Visual setting of header the data field.
    /// </value>
    /// <remarks>
    /// <para><strong><u>Usage</u></strong>:</para>
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <Field|Fixed|Gap|Group ...>
    ///    <Header .../>
    ///   ...
    /// </Field|Fixed|Gap|Group>
    /// ]]>
    /// </code>
    /// </remarks>
    /// <example>
    /// In the following example shows how create a data field.
    /// <para><strong><u>Usage</u></strong>:</para>
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <Field Name="##LINE" Alias="Line">
    ///     <Header Style="CommonHeader" Show="Yes"/>
    ///     <Value Style="LineValue"/>
    ///     <Aggregate Style="TopAggregate" Type="Count" Location="Top" Show="Yes"/>
    /// </Field>
    /// ]]>
    /// </code>
    /// <code lang="cs">
    /// DataField lineField = new DataField
    /// {
    ///     Name = "##LINE",
    ///     Alias = "Line",
    ///     Value = new FieldValue { Style = "LineValue" },
    ///     Header = new FieldHeader { Style = "CommonHeader", Show = YesNo.Yes },
    ///     Aggregate = new FieldAggregate
    ///     {
    ///         Show = YesNo.Yes,
    ///         Style = "TopAggregate",
    ///         Location = KnownAggregateLocation.Top,
    ///         AggregateType = KnownAggregateType.Count
    ///     }
    /// };
    /// </code>
    /// </example>
    [JsonProperty]
    [XmlElement]
    public FieldHeader Header
    {
        get
        {
            _header ??= new FieldHeader();
            _header.SetParent(this);

            return _header;
        }
        set => _header = value;
    }

    /// <summary>
    /// Gets or sets a reference that contains the visual setting of value the data field.
    /// </summary>
    /// <value>
    /// Visual setting of value the data field.
    /// </value>
    /// <remarks>
    /// <para><strong><u>Usage</u></strong>:</para>
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <Field|Fixed|Gap|Group ...>
    ///   <Value .../>
    ///   ...
    /// </Field|Fixed|Gap|Group>
    /// ]]>
    /// </code>
    /// </remarks>
    /// <example>
    /// In the following example shows how create a data field.
    /// <para><strong><u>Usage</u></strong>:</para>
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <Field Name="##LINE" Alias="Line">
    ///     <Header Style="CommonHeader" Show="Yes"/>
    ///     <Value Style="LineValue"/>
    ///     <Aggregate Style="TopAggregate" Type="Count" Location="Top" Show="Yes"/>
    /// </Field>
    /// ]]>
    /// </code>
    /// <code lang="cs">
    /// DataField lineField = new DataField
    /// {
    ///     Name = "##LINE",
    ///     Alias = "Line",
    ///     Value = new FieldValue { Style = "LineValue" },
    ///     Header = new FieldHeader { Style = "CommonHeader", Show = YesNo.Yes },
    ///     Aggregate = new FieldAggregate
    ///     {
    ///         Show = YesNo.Yes,
    ///         Style = "TopAggregate",
    ///         Location = KnownAggregateLocation.Top,
    ///         AggregateType = KnownAggregateType.Count
    ///     }
    /// };
    /// </code>
    /// </example>
    [JsonProperty]
    [XmlElement]
    public FieldValue Value
    {
        get
        {
            _value ??= new FieldValue();
            _value.SetParent(this);

            return _value;
        }
        set => _value = value;
    }

    /// <summary>
    /// Gets or sets the preferred width of field, indicate a size as multiply of 100 (ex. 9.3 => 930). The default is 'Default'.
    /// </summary>
    /// <value>
    /// A <see cref="string"/> that represents a size as multiply of 100 (ex. 9.3 => 930).
    /// </value>
    /// <remarks>
    /// <para><strong><u>Usage</u></strong>:</para>
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <Field|Fixed|Gap|Group Width="Default|BestFit|A number as multiply of 100" ...>
    ///   ...
    /// </Field|Fixed|Gap|Group>
    /// ]]>
    /// </code>
    /// </remarks>
    /// <example>
    /// <para><strong><u>Usage</u></strong>:</para>
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <Field Name="##LINE" Alias="Line" Width="BestFit">
    /// ...
    /// </Field>
    /// ]]>
    /// </code>
    /// <code lang="cs">
    /// DataField lineField = new DataField
    /// {
    ///     Name = "##LINE",
    ///     Alias = "Line",
    ///     Value = new FieldValue { Style = "LineValue" },
    ///     Header = new FieldHeader { Style = "CommonHeader", Show = YesNo.Yes },
    ///     Aggregate = new FieldAggregate
    ///     {
    ///         Show = YesNo.Yes,
    ///         Style = "TopAggregate",
    ///         Location = KnownAggregateLocation.Top,
    ///         AggregateType = KnownAggregateType.Count
    ///     }
    /// };
    /// </code>
    /// </example>
    /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/></exception>
    [JsonProperty]
    [XmlAttribute]
    [DefaultValue(WidthDefault)]
    public string Width
    {
        get => GetStaticBindingValue(_width);
        set
        {
            SentinelHelper.ArgumentNull(value, nameof(value));

            _width = value;
        }
    }

    #endregion

    #region protected virtual properties

    /// <summary>
    /// When overridden in a derived class, gets a value indicating whether the current data field supports data.
    /// </summary>
    /// <value>
    /// <strong>true</strong> if the data field supports data; otherwise, <strong>false</strong>.
    /// </value>
    [JsonIgnore]
    [XmlIgnore]
    protected virtual bool CanSetData => true;

    #endregion

    #region public methods

    /// <summary>
    /// Calculates field width from Width property measured in points.
    /// </summary>
    /// <returns>
    /// A <see cref="double"/> value thats contains the field width measured in points
    /// </returns>
    public double CalculateWidthValue()
    {
        var ok = int.TryParse(Width, out var fieldWidth);
        if (!ok)
        {
            return Width.ToUpperInvariant() == "DEFAULT"
                ? 9.140625d
                : double.NaN;
        }

        return fieldWidth / 100.0d;
    }

    /// <summary>
    /// Sets the element that owns this data field.
    /// </summary>
    /// <param name="reference">Reference to owner.</param>
    public void SetOwner(FieldsCollection reference)
    {
        Owner = reference;
    }

    #endregion
}
