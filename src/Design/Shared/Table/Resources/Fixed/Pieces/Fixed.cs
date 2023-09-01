
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;

using Newtonsoft.Json;

using iTin.Core.Helpers;
using iTin.Core.Models.ComponentModel.Exceptions;
using iTin.Core.Models.Helpers;

using ModelRegularExpressionHelper = iTin.Core.Models.Helpers.RegularExpressionHelper;

namespace iTin.Core.Models.Design.Table.Resource;

/// <summary>
/// Contains a collection of pieces. Each element is a new collection of smaller fields resulting from splitting a reference field.
/// </summary>
/// <remarks>
/// <para>
/// Belongs to: <strong><c>Fixed</c></strong>. For more information, please see <see cref="FixedCollection"/>.
/// </para>    
/// <strong><u>Usage</u></strong>:
/// <code lang="xml" title="ITEE Object Element Usage">
/// <![CDATA[
/// <?xml version="1.0" encoding="utf-8">
/// <Pieces ...>
///   <Piece/>
///   <Piece/>
///   ... 
/// </Pieces>
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
///   <term><see cref="Reference"/></term>
///   <term>No</term>
///   <description>Data field name reference.</description>
///  </item>
/// </list>
/// <para><strong><u>Elements</u></strong></para>
/// <list type="table">
///   <listheader>
///     <term>Element</term>
///     <description>Description</description>
///   </listheader>
///   <item>
///     <term><see cref="Pieces"/></term> 
///     <description>Collection of smaller fields resulting from splitting a reference field. Each element is composed of a field name and initial position and final position into the reference field.</description>
///   </item>
/// </list>
/// </remarks>
public partial class Fixed
{
    #region private members

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string _name;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private List<Piece> _pieces;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string _reference;

    #endregion

    #region public readonly properties

    #region [public] (FixedCollection) Owner: Gets the element that owns this instance
    /// <summary>
    /// Gets the element that owns this <see cref="Fixed"/>.
    /// </summary>
    /// <value>
    /// The <see cref="FixedCollection"/> that owns this <see cref="Fixed"/>.
    /// </value>
    [JsonIgnore]
    [XmlIgnore]
    [Browsable(false)]
    public FixedCollection Owner { get; private set; }
    #endregion

    #endregion

    #region public properties

    #region [public] (XElement) DataSource: Gets or sets a reference to source data of pieces
    /// <summary>
    /// Gets or sets a reference to source data of pieces.
    /// </summary>
    /// <value>
    /// Source data of pieces.
    /// </value>
    [JsonIgnore]
    [XmlIgnore]
    [Browsable(false)]
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public XElement DataSource { get; set; }
    #endregion

    #region [public] (string) Name: Gets or sets the name of the pieces
    /// <summary>
    /// Gets or sets the name of the pieces.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Name of the collection of pieces.<br/>
    /// Are only allow strings made ​​up of letters, numbers and following special chars <strong>'<c>_ - # * @ % $</c>'</strong>.
    /// </para>
    /// <strong><u>Usage</u></strong>:
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <?xml version="1.0" encoding="utf-8">
    /// <Pieces Name="string" ...>
    ///     ...
    /// </Pieces>
    /// ]]>
    /// </code>
    /// </remarks>
    /// <exception cref="System.ArgumentNullException">If <paramref name="value" /> is <see langword="null"/>.</exception>
    /// <exception cref="InvalidIdentifierNameException">If <paramref name="value" /> not is a valid identifier.</exception>
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
                new InvalidIdentifierNameException(ErrorMessageHelper.ModelIdentifierNameErrorMessage(nameof(Pieces), nameof(Name), value)));

            _name = value;
        }
    }
    #endregion

    #region [public] (List<Piece>) Pieces: Gets or sets collection of smaller fields resulting from splitting a reference field
    /// <summary>
    /// Gets or sets collection of smaller fields resulting from splitting a reference field.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Collection of smaller fields resulting from splitting a reference field.<br/>
    /// Each element is composed of a field name and initial position and final position into the reference field.
    /// </para>
    /// <strong><u>Usage</u></strong>:
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <Pieces>
    ///   <Piece .../>
    ///   <Piece .../>
    ///   ...
    /// </Pieces>
    /// ]]>
    /// </code>
    /// </remarks>
    [JsonProperty("pieces")]
    [XmlElement("Piece")]
    public List<Piece> Pieces
    {
        get
        {
            _pieces ??= new List<Piece>();
            foreach (var item in _pieces)
            {
                item.SetOwner(this);
            }

            return _pieces;
        }
        set => _pieces = value;
    }
    #endregion

    #region [public] (string) Reference: Gets or sets the name of the reference field
    /// <summary>
    /// Gets or sets the name of the reference field.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Name of the reference field.<br/>
    /// Are only allow strings made ​​up of letters, numbers and following special chars <strong>'<c>_ - # * @ % $</c>'</strong>.
    /// </para>
    /// <strong><u>Usage</u></strong>:
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <Pieces Reference="string" ...>
    ///   ...
    /// </Pieces>
    /// ]]>
    /// </code>
    /// </remarks>
    /// <exception cref="ArgumentNullException">If <paramref name="value" /> is <see langword="null"/>.</exception>
    /// <exception cref="InvalidFieldIdentifierNameException">If <paramref name="value" /> not is a valid field identifier.</exception>
    [JsonProperty]
    [XmlAttribute]
    public string Reference
    {
        get => _reference;
        set
        {
            SentinelHelper.ArgumentNull(value, nameof(value));
            SentinelHelper.IsFalse(
                ModelRegularExpressionHelper.IsValidFieldName(value), 
                new InvalidFieldIdentifierNameException(ErrorMessageHelper.FieldIdentifierNameErrorMessage(nameof(Pieces), nameof(Reference), value)));

            _reference = value;
        }
    }
    #endregion

    #endregion

    #region public methods

    #region [public] (Dictionary<string, string>) ToDictionary():  Returns a dictionary of string/string pairs containing the name of the piece and its value
    /// <summary>
    /// Returns a dictionary of <see cref="string"/>/<see cref="string"/> pairs containing the name of the piece and its value.
    /// </summary>
    /// <returns>
    /// A dictionary of <see cref="string"/>/<see cref="string"/> pairs containing the name of the piece and its value.
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown if <see cref="DataSource" /> property is <see langword="null"/>.</exception>
    public Dictionary<string, string> ToDictionary()
    {
        SentinelHelper.ArgumentNull(DataSource, ErrorMessage.DataSourceNotNull);

        return Pieces.ToDictionary(piece => piece.Name, piece => piece.GetValue());
    }
    #endregion

    #region [public] (void) SetOwner(FixedCollection): Sets the element that owns this Fixed
    /// <summary>
    /// Sets the element that owns this <see cref="FixedCollection"/>.
    /// </summary>
    /// <param name="reference">Reference to owner.</param>
    public void SetOwner(FixedCollection reference)
    {
        Owner = reference;
    }
    #endregion

    #endregion
}
