
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Linq;
using System.Xml.Serialization;

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
///     <term><see cref="FixedField"/></term>
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
    public abstract KnownFieldType FieldType { get; }

    #endregion

    #region public readonly properties

    /// <summary>
    /// Gets the element that owns this data field.
    /// </summary>
    /// <value>
    /// The <see cref="FieldsCollection" /> that owns this data field.
    /// </value>
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
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// &lt;Field|Fixed|Gap|Group ...&gt;
    ///   &lt;Aggregate .../&gt;
    ///   ...
    /// &lt;/Field|Fixed|Gap|Group&gt;
    /// </code>
    /// <para>
    /// <para><strong>Compatibility table with native writers.</strong></para>
    /// <table>
    ///   <thead>
    ///     <tr>
    ///       <th>Comma-Separated Values<br/><see cref="T:iTin.Export.Writers.CsvWriter" /></th>
    ///       <th>Tab-Separated Values<br/><see cref="T:iTin.Export.Writers.TsvWriter" /></th>
    ///       <th>SQL Script<br/><see cref="T:iTin.Export.Writers.SqlScriptWriter" /></th>
    ///       <th>XML Spreadsheet 2003<br/><see cref="T:iTin.Export.Writers.Spreadsheet2003TabularWriter" /></th>
    ///     </tr>
    ///   </thead>
    ///   <tbody>
    ///     <tr>
    ///       <td align="center">No has effect</td>
    ///       <td align="center">No has effect</td>
    ///       <td align="center">No has effect</td>
    ///       <td align="center">X</td>
    ///     </tr>
    ///   </tbody>
    /// </table>
    /// A <strong><c>X</c></strong> value indicates that the writer supports this element.
    /// </para>
    /// </remarks>
    /// <example>
    /// In the following example shows how create a data field.
    /// <code lang="xml">
    ///   &lt;Field Name="##LINE" Alias="Line"&gt;
    ///     &lt;Header Style="CommonHeader" Show="Yes"/&gt;
    ///     &lt;Value Style="LineValue"/&gt;
    ///     &lt;Aggregate Style="TopAggregate" Type="Count" Location="Top" Show="Yes"/&gt;
    ///   &lt;/Field&gt;
    /// </code>
    /// <code lang="cs">
    /// DataFieldModel lineField = new DataFieldModel
    ///                            {
    ///                                Name = "##LINE",
    ///                                Alias = "Line",
    ///                                Value = new FieldValueModel { Style = "LineValue" },
    ///                                Header = new FieldHeaderModel { Style = "CommonHeader", Show = YesNo.Yes },
    ///                                Aggregate = new FieldAggregateModel
    ///                                {
    ///                                    Show = YesNo.Yes,
    ///                                    Style = "TopAggregate", 
    ///                                    Location = KnownAggregateLocation.Top,
    ///                                    AggregateType = KnownAggregateType.Count
    ///                                }
    ///                            };
    /// </code>
    /// </example>
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
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// &lt;Field|Fixed|Gap|Group Alias="string" ...&gt;
    ///   ...
    /// &lt;/Field|Fixed|Gap|Group&gt;
    /// </code>
    /// <para>
    /// <para><strong>Compatibility table with native writers.</strong></para>
    /// <table>
    ///   <thead>
    ///     <tr>
    ///       <th>Comma-Separated Values<br/><see cref="T:iTin.Export.Writers.CsvWriter"/></th>
    ///       <th>Tab-Separated Values<br/><see cref="T:iTin.Export.Writers.TsvWriter"/></th>
    ///       <th>SQL Script<br/><see cref="T:iTin.Export.Writers.SqlScriptWriter"/></th>
    ///       <th>XML Spreadsheet 2003<br/><see cref="T:iTin.Export.Writers.Spreadsheet2003TabularWriter"/></th>
    ///     </tr>
    ///   </thead>
    ///   <tbody>
    ///     <tr>
    ///       <td align="center">X</td>
    ///       <td align="center">X</td>
    ///       <td align="center">X</td>
    ///       <td align="center">X</td>
    ///     </tr>
    ///   </tbody>
    /// </table>
    /// A <strong><c>X</c></strong> value indicates that the writer supports this element.
    /// </para>
    /// </remarks>
    /// <example>
    /// <code lang="xml">
    /// &lt;Field Name="##LINE" Alias="Line"&gt;
    /// ...
    /// &lt;/Field&gt;
    /// </code>
    /// <code lang="cs">
    /// DataFieldModel lineField = new DataFieldModel
    ///                            {
    ///                                Name = "##LINE",
    ///                                Alias = "Line",
    ///                                Value = new FieldValueModel { Style = "LineValue" },
    ///                                Header = new FieldHeaderModel { Style = "CommonHeader", Show = YesNo.Yes },
    ///                                Aggregate = new FieldAggregateModel
    ///                                {
    ///                                    Show = YesNo.Yes,
    ///                                    Style = "TopAggregate", 
    ///                                    Location = KnownAggregateLocation.Top,
    ///                                    AggregateType = KnownAggregateType.Count,
    ///                                }
    ///                            };
    /// </code>
    /// </example>
    [XmlAttribute]
    public string Alias { get; set; }

    /// <summary>
    /// Gets or sets a reference for pieces data.
    /// </summary>
    /// <value>
    /// A <see cref="XElement"/> that contains the pieces data.
    /// </value>
    /// <exception cref="InvalidOperationException">If not supported for this data field.</exception>
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
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// &lt;Field|Fixed|Gap|Group ...&gt;
    ///    &lt;Header .../&gt;
    ///   ...
    /// &lt;/Field|Fixed|Gap|Group&gt;
    /// </code>
    /// <para>
    /// <para><strong>Compatibility table with native writers.</strong></para>
    /// <table>
    ///   <thead>
    ///     <tr>
    ///       <th>Comma-Separated Values<br/><see cref="T:iTin.Export.Writers.CsvWriter"/></th>
    ///       <th>Tab-Separated Values<br/><see cref="T:iTin.Export.Writers.TsvWriter"/></th>
    ///       <th>SQL Script<br/><see cref="T:iTin.Export.Writers.SqlScriptWriter"/></th>
    ///       <th>XML Spreadsheet 2003<br/><see cref="T:iTin.Export.Writers.Spreadsheet2003TabularWriter"/></th>
    ///     </tr>
    ///   </thead>
    ///   <tbody>
    ///     <tr>
    ///       <td align="center">No has effect</td>
    ///       <td align="center">No has effect</td>
    ///       <td align="center">No has effect</td>
    ///       <td align="center">X</td>
    ///     </tr>
    ///   </tbody>
    /// </table>
    /// A <strong><c>X</c></strong> value indicates that the writer supports this element.
    /// </para>
    /// </remarks>
    /// <example>
    /// In the following example shows how create a data field.
    /// <code lang="xml">
    ///   &lt;Field Name="##LINE" Alias="Line"&gt;
    ///     &lt;Header Style="CommonHeader" Show="Yes"/&gt;
    ///     &lt;Value Style="LineValue"/&gt;
    ///     &lt;Aggregate Style="TopAggregate" Type="Count" Location="Top" Show="Yes"/&gt;
    ///   &lt;/Field&gt;
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
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// &lt;Field|Fixed|Gap|Group ...&gt;
    ///   &lt;Value .../&gt;
    ///   ...
    /// &lt;/Field|Fixed|Gap|Group&gt;
    /// </code>
    /// <para>
    /// <para><strong>Compatibility table with native writers.</strong></para>
    /// <table>
    ///   <thead>
    ///     <tr>
    ///       <th>Comma-Separated Values<br/><see cref="T:iTin.Export.Writers.CsvWriter" /></th>
    ///       <th>Tab-Separated Values<br/><see cref="T:iTin.Export.Writers.TsvWriter" /></th>
    ///       <th>SQL Script<br/><see cref="T:iTin.Export.Writers.SqlScriptWriter" /></th>
    ///       <th>XML Spreadsheet 2003<br/><see cref="T:iTin.Export.Writers.Spreadsheet2003TabularWriter" /></th>
    ///     </tr>
    ///   </thead>
    ///   <tbody>
    ///     <tr>
    ///       <td align="center">No has effect</td>
    ///       <td align="center">No has effect</td>
    ///       <td align="center">No has effect</td>
    ///       <td align="center">X</td>
    ///     </tr>
    ///   </tbody>
    /// </table>
    /// A <strong><c>X</c></strong> value indicates that the writer supports this element.
    /// </para>
    /// </remarks>
    /// <example>
    /// In the following example shows how create a data field.
    /// <code lang="xml" title="ITEE Object Element Usage">
    ///   &lt;Field Name="##LINE" Alias="Line"&gt;
    ///     &lt;Header Style="CommonHeader" Show="Yes"/&gt;
    ///     &lt;Value Style="LineValue"/&gt;
    ///     &lt;Aggregate Style="TopAggregate" Type="Count" Location="Top" Show="Yes"/&gt;
    ///   &lt;/Field&gt;
    /// </code>
    /// <code lang="cs">
    /// DataFieldModel lineField = new DataFieldModel
    ///                            {
    ///                                Name = "##LINE",
    ///                                Alias = "Line",
    ///                                Value = new FieldValueModel { Style = "LineValue" },
    ///                                Header = new FieldHeaderModel { Style = "CommonHeader", Show = YesNo.Yes },
    ///                                Aggregate = new FieldAggregateModel
    ///                                {
    ///                                    Show = YesNo.Yes,
    ///                                    Style = "TopAggregate", 
    ///                                    Location = KnownAggregateLocation.Top,
    ///                                    AggregateType = KnownAggregateType.Count
    ///                                }
    ///                            };
    /// </code>
    /// </example>
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
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// &lt;Field|Fixed|Gap|Group Width="Default|BestFit|A number as multiply of 100" ...&gt;
    ///   ...
    /// &lt;/Field|Fixed|Gap|Group&gt;
    /// </code>
    /// <para>
    /// <para><strong>Compatibility table with native writers.</strong></para>
    /// <table>
    ///   <thead>
    ///     <tr>
    ///       <th>Comma-Separated Values<br/><see cref="T:iTin.Export.Writers.CsvWriter"/></th>
    ///       <th>Tab-Separated Values<br/><see cref="T:iTin.Export.Writers.TsvWriter"/></th>
    ///       <th>SQL Script<br/><see cref="T:iTin.Export.Writers.SqlScriptWriter"/></th>
    ///       <th>XML Spreadsheet 2003<br/><see cref="T:iTin.Export.Writers.Spreadsheet2003TabularWriter"/></th>
    ///     </tr>
    ///   </thead>
    ///   <tbody>
    ///     <tr>
    ///       <td align="center">No has effect</td>
    ///       <td align="center">No has effect</td>
    ///       <td align="center">No has effect</td>
    ///       <td align="center">X</td>
    ///     </tr>
    ///   </tbody>
    /// </table>
    /// A <strong><c>X</c></strong> value indicates that the writer supports this element.
    /// </para>
    /// </remarks>
    /// <example>
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// &lt;Field Name="##LINE" Alias="Line" Width="BestFit"&gt;
    /// ...
    /// &lt;/Field&gt;
    /// </code>
    /// <code lang="cs">
    /// DataFieldModel lineField = new DataFieldModel
    ///                            {
    ///                                Name = "##LINE",
    ///                                Alias = "Line",
    ///                                Width = "BestFit",
    ///                                Value = new FieldValueModel { Style = "LineValue" },
    ///                                Header = new FieldHeaderModel { Style = "CommonHeader", Show = YesNo.Yes },
    ///                                Aggregate = new FieldAggregateModel
    ///                                {
    ///                                    Show = YesNo.Yes,
    ///                                    Style = "TopAggregate", 
    ///                                    Location = KnownAggregateLocation.Top,
    ///                                    AggregateType = KnownAggregateType.Count,
    ///                                }
    ///                            };
    /// </code>
    /// </example>
    /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/></exception>
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
    protected virtual bool CanSetData => true;

    #endregion

    #region public methods

    /// <summary>
    /// Calculates field width from Width property measured in points.
    /// </summary>
    /// <returns>
    /// A <see cref="T:System.Double"/> value thats contains the field width measured in points
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
