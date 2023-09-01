
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

using iTin.Core.Helpers;

using iTin.Core.Models.ComponentModel.Exceptions;
using iTin.Core.Models.Design.Enums;
using iTin.Core.Models.Helpers;

using Newtonsoft.Json;

namespace iTin.Core.Models.Design.Table.Resource;

/// <summary>
/// Represents a new field composed of a field name and initial position and final position into the reference field.
/// </summary>
/// <remarks>
/// <para>
/// Belongs to: <strong><c>Fixed</c></strong>. For more information, please see <see cref="Fixed"/>.
/// <para><u><strong>Usage</strong></u></para>
/// <code lang="xml" title="ITEE Object Element Usage">
/// <![CDATA[
/// <Piece .../>
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
///   <description>Name of the piece.</description>
///  </item>
///  <item>
///   <term><see cref="From"/></term>
///   <term>No</term>
///   <description>Initial character of the piece into field reference.</description>
///  </item>
///  <item>
///   <term><see cref="Lenght"/></term>
///   <term>Yes</term>
///   <description>Length in characters of the piece.</description>
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
public partial class Piece
{
    #region private constants

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const YesNo DefaultTrim = YesNo.No;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const KnownTrimMode DefaultTrimMode = KnownTrimMode.All;

    #endregion
        
    #region private members

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private int _from;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private YesNo _trim;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private int _lenght;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string _name;
    
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private KnownTrimMode _trimMode;

    #endregion

    #region constructor/s

    #region [public] Piece(): Initializes a new instance of the class
    /// <summary>
    /// Initializes a new instance of the <see cref="Piece"/> class.
    /// </summary>
    public Piece()
    {
        Trim = DefaultTrim;
        TrimMode = DefaultTrimMode;
    }
    #endregion

    #endregion

    #region public readonly properties

    #region [public] (Fixed) Owner: Gets the element that owns this instance
    /// <summary>
    /// Gets the element that owns this <see cref="Piece"/>.
    /// </summary>
    /// <value>
    /// The <see cref="Fixed"/> that owns this <see cref="Piece"/>.
    /// </value>
    [JsonIgnore]
    [XmlIgnore]
    [Browsable(false)]
    public Fixed Owner { get; private set; }
    #endregion

    #endregion

    #region public properties

    #region [public] (int) From: Gets or sets the initial character of the piece into field reference
    /// <summary>
    /// Gets or sets the initial character of the piece into field reference.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The zero-based initial character position of a piece.
    /// </para>
    /// <strong><u>Usage</u></strong>:
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    ///   <Piece From="int" .../>
    /// ]]>
    /// </code>
    /// <example>
    /// <para><strong><u>Examples</u></strong>:</para>
    /// <para><c>XML</c></para>
    /// Suppose we have the following input data:
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <ARD740>
    ///   <R740D01 _x0023_LINE="10" SFLDTA="4 60027           27        55        75        13   20/02/13 " ... />
    ///   <R740D01 _x0023_LINE="20" SFLDTA="4 61535            3                   2             08/03/13 " ... />
    ///   ...
    ///   ...
    /// </ARD740>
    /// ]]>
    /// </code>
    /// <para>Now we create the collection of pieces:</para>
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <Pieces Name="SFLDTA_Pieces" Reference="SFLDTA">
    ///   <Piece Name="DCALL" From="0" Lenght="2"/>
    ///   <Piece Name="NOCOL" From="2" Lenght="16" Trim="Yes" TrimMode="All"/>
    ///   <Piece Name="SHOP" From="18" Lenght="10"/>
    ///   <Piece Name="SIT" From="28" Lenght="10"/>
    ///   <Piece Name="PIK" From="38" Lenght="5"/>
    ///   <Piece Name="PKG" From="48" Lenght="5"/>
    ///   <Piece Name="DUEDATE" From="53" Lenght="9" Trim="Yes" TrimMode="All"/>
    /// </Pieces>
    /// ]]>
    /// </code>
    /// </example>
    /// </remarks>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <c>value</c> is less than zero <br/>
    /// - or - <br/>
    /// <c>value</c> is greater than length of <see cref="Fixed.Reference"/> property.
    /// </exception>
    [JsonProperty]
    [XmlAttribute]
    public int From
    {
        get => _from;
        set
        {
            if (Owner != null)
            {
                SentinelHelper.ArgumentOutOfRange(
                    nameof(From),
                    value,
                    0, 
                    Owner.Reference.Length,
                    ErrorMessage.PieceArgumentOutOfRange);
            }
            else
            {
                SentinelHelper.ArgumentLessThan(nameof(From), value, 0);
            }
                                    
            _from = value;
        }
    }
    #endregion

    #region [public] (int) Lenght: Gets or sets the length in characters of the piece
    /// <summary>
    /// Gets or sets the length in characters of the piece.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Length in characters of the piece.
    /// </para>
    /// <strong><u>Usage</u></strong>:
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    ///   <Piece Lenght="int" .../>
    /// ]]>
    /// </code>
    /// <example>
    /// <para><strong><u>Examples</u></strong>:</para>
    /// <para><c>XML</c></para>
    /// Suppose we have the following input data:
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <ARD740>
    ///   <R740D01 _x0023_LINE="10" SFLDTA="4 60027           27        55        75        13   20/02/13 " ... />
    ///   <R740D01 _x0023_LINE="20" SFLDTA="4 61535            3                   2             08/03/13 " ... />
    ///   ...
    ///   ...
    /// </ARD740>
    /// ]]>
    /// </code>
    /// <para>Now we create the collection of pieces:</para>
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <Pieces Name="SFLDTA_Pieces" Reference="SFLDTA">
    ///   <Piece Name="DCALL" From="0" Lenght="2"/>
    ///   <Piece Name="NOCOL" From="2" Lenght="16" Trim="Yes" TrimMode="All"/>
    ///   <Piece Name="SHOP" From="18" Lenght="10"/>
    ///   <Piece Name="SIT" From="28" Lenght="10"/>
    ///   <Piece Name="PIK" From="38" Lenght="5"/>
    ///   <Piece Name="PKG" From="48" Lenght="5"/>
    ///   <Piece Name="DUEDATE" From="53" Lenght="9" Trim="Yes" TrimMode="All"/>
    /// </Pieces>
    /// ]]>
    /// </code>
    /// </example>
    /// </remarks>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <c>value</c> is less than zero <br/>
    /// - or - <br/>n
    /// <c>value</c> is greater than lenght of <see cref="Fixed.Reference"/> property minus property value <see cref="From"/>. 
    /// </exception>
    [JsonProperty]
    [XmlAttribute]
    public int Lenght
    {
        get => _lenght;
        set
        {
            if (Owner != null)
            {
                SentinelHelper.ArgumentOutOfRange(
                    nameof(Lenght),
                    value,
                    0, 
                    Owner.Reference.Length - From, ErrorMessage.PieceArgumentOutOfRange);
            }
            else
            {
                SentinelHelper.ArgumentLessThan(nameof(Lenght), value, 1);
            }

            _lenght = value;
        }
    }
    #endregion

    #region [public] (string) Name: Gets or sets the name of the piece
    /// <summary>
    /// Gets or sets the name of the piece.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Name of the piece.<br/>
    /// Are only allow strings made ​​up of letters, numbers and following special chars <strong>'<c>_ - # * @ % $</c>'</strong>.
    /// </para>
    /// <strong><u>Usage</u></strong>:
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    ///   <Piece Name="string" .../>
    /// ]]>
    /// </code>
    /// <example>
    /// <para><strong><u>Examples</u></strong>:</para>
    /// <para><c>XML</c></para>
    /// Suppose we have the following input data:
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <ARD740>
    ///   <R740D01 _x0023_LINE="10" SFLDTA="4 60027           27        55        75        13   20/02/13 " ... />
    ///   <R740D01 _x0023_LINE="20" SFLDTA="4 61535            3                   2             08/03/13 " ... />
    ///   ...
    ///   ...
    /// </ARD740>
    /// ]]>
    /// </code>
    /// <para>Now we create the collection of pieces:</para>
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <Pieces Name="SFLDTA_Pieces" Reference="SFLDTA">
    ///   <Piece Name="DCALL" From="0" Lenght="2"/>
    ///   <Piece Name="NOCOL" From="2" Lenght="16" Trim="Yes" TrimMode="All"/>
    ///   <Piece Name="SHOP" From="18" Lenght="10"/>
    ///   <Piece Name="SIT" From="28" Lenght="10"/>
    ///   <Piece Name="PIK" From="38" Lenght="5"/>
    ///   <Piece Name="PKG" From="48" Lenght="5"/>
    ///   <Piece Name="DUEDATE" From="53" Lenght="9" Trim="Yes" TrimMode="All"/>
    /// </Pieces>
    /// ]]>
    /// </code>
    /// </example>
    /// </remarks>
    /// <exception cref="System.ArgumentNullException">If <paramref name="value" /> is <see langword="null"/>.</exception>
    /// <exception cref="InvalidIdentifierNameException">If <paramref name="value" /> not is a valid identifier name.</exception>
    [JsonProperty]
    [XmlAttribute]
    public string Name
    {
        get => _name;
        set
        {
            SentinelHelper.ArgumentNull(value, nameof(value));
            SentinelHelper.IsFalse(
                Models.Helpers.RegularExpressionHelper.IsValidIdentifier(value), 
                new InvalidIdentifierNameException(ErrorMessageHelper.ModelIdentifierNameErrorMessage(nameof(Piece), nameof(Name), value)));

            _name = value;
        }
    }
    #endregion

    #region [public] (YesNo) Trim: Gets or sets the name of the piece
    /// <summary>
    /// Gets or sets a value indicating whether to trim the blanks in this piece.
    /// </summary>
    /// <remarks>
    /// <para>
    /// <see cref="YesNo.Yes"/> to trim the blanks in this piece; otherwise, <see cref="YesNo.No"/>. The default is <see cref="YesNo.No"/>.
    /// </para>
    /// <strong><u>Usage</u></strong>:
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    ///   <Piece Trim="Yes|No" .../>
    /// ]]>
    /// </code>
    /// <example>
    /// <para><strong><u>Examples</u></strong>:</para>
    /// <para><c>XML</c></para>
    /// Suppose we have the following input data:
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <ARD740>
    ///   <R740D01 _x0023_LINE="10" SFLDTA="4 60027           27        55        75        13   20/02/13 " ... />
    ///   <R740D01 _x0023_LINE="20" SFLDTA="4 61535            3                   2             08/03/13 " ... />
    ///   ...
    ///   ...
    /// </ARD740>
    /// ]]>
    /// </code>
    /// <para>Now we create the collection of pieces:</para>
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <Pieces Name="SFLDTA_Pieces" Reference="SFLDTA">
    ///   <Piece Name="DCALL" From="0" Lenght="2"/>
    ///   <Piece Name="NOCOL" From="2" Lenght="16" Trim="Yes" TrimMode="All"/>
    ///   <Piece Name="SHOP" From="18" Lenght="10"/>
    ///   <Piece Name="SIT" From="28" Lenght="10"/>
    ///   <Piece Name="PIK" From="38" Lenght="5"/>
    ///   <Piece Name="PKG" From="48" Lenght="5"/>
    ///   <Piece Name="DUEDATE" From="53" Lenght="9" Trim="Yes" TrimMode="All"/>
    /// </Pieces>
    /// ]]>
    /// </code>
    /// </example>
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
    /// Gets or sets a value that determines trim mode for strings.
    /// </summary>
    /// <remarks>
    /// <para>
    /// One of the <see cref="KnownTrimMode"/> values. The default is <see cref="KnownTrimMode.All"/>.
    /// </para>
    /// <strong><u>Usage</u></strong>:
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    ///   <Piece TrimMode="All|Start|End" .../>
    /// ]]>
    /// </code>
    /// <example>
    /// <para><strong><u>Examples</u></strong>:</para>
    /// <para><c>XML</c></para>
    /// Suppose we have the following input data:
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <ARD740>
    ///   <R740D01 _x0023_LINE="10" SFLDTA="4 60027           27        55        75        13   20/02/13 " ... />
    ///   <R740D01 _x0023_LINE="20" SFLDTA="4 61535            3                   2             08/03/13 " ... />
    ///   ...
    ///   ...
    /// </ARD740>
    /// ]]>
    /// </code>
    /// <para>Now we create the collection of pieces:</para>
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <Pieces Name="SFLDTA_Pieces" Reference="SFLDTA">
    ///   <Piece Name="DCALL" From="0" Lenght="2"/>
    ///   <Piece Name="NOCOL" From="2" Lenght="16" Trim="Yes" TrimMode="All"/>
    ///   <Piece Name="SHOP" From="18" Lenght="10"/>
    ///   <Piece Name="SIT" From="28" Lenght="10"/>
    ///   <Piece Name="PIK" From="38" Lenght="5"/>
    ///   <Piece Name="PKG" From="48" Lenght="5"/>
    ///   <Piece Name="DUEDATE" From="53" Lenght="9" Trim="Yes" TrimMode="All"/>
    /// </Pieces>
    /// ]]>
    /// </code>
    /// </example>
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
    /// <exception cref="ArgumentNullException">
    /// <see cref="Fixed.DataSource" /> is <see langword="null"/>.<br/>
    /// 
    /// -or-<br/>
    /// 
    /// <see cref="Fixed.Reference" /> not found.
    /// </exception>
    public string GetValue()
    {
        SentinelHelper.ArgumentNull(Owner.DataSource, ErrorMessage.DataSourceNotNull);

        var attribute = Owner.DataSource.Attribute(Owner.Reference);
        if (attribute == null)
        {
            throw new ArgumentNullException(Owner.Reference, ErrorMessage.PiecesReferenceNull);
        }

        var originalValue = attribute.Value.Substring(From, Lenght);
        var value = ParseValue(originalValue);

        return value;
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
