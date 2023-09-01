
using System;
using System.Diagnostics;
using System.Xml.Serialization;

using iTin.Core.Helpers;
using iTin.Core.Models.ComponentModel.Exceptions;
using iTin.Core.Models.Helpers;

using Newtonsoft.Json;

using ModelsRegularExpressionHelper = iTin.Core.Models.Helpers.RegularExpressionHelper;

namespace iTin.Core.Models.Design.Table.Fields;

/// <summary>
/// A Specialization of <see cref="BaseDataField"/> class.<br/>
/// Represents a piece of a field fixed-width data.
/// </summary>
/// <remarks>
/// <para>Belongs to: <strong><c>Fields</c></strong>. For more information, please see <see cref="Fields"/>.</para>
/// <para><strong><u>Usage</u></strong>:</para>
/// <code lang="xml" title="ITEE Object Element Usage">
/// <![CDATA[
/// <Fixed ...>
///   <Header/>
///   <Value/>
///   <Aggregate/>
/// </Fixed>
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
///       <td><see cref="Pieces" /></td>
///       <td align="center">No</td>
///       <td>Name of the collection of pieces.</td>
///     </tr>
///     <tr>
///       <td><see cref="Piece" /></td>
///       <td align="center">No</td>
///       <td>Name of the piece.</td>
///     </tr>
///     <tr>
///       <td><see cref="BaseDataField.Alias" /></td>
///       <td align="center">Yes</td>
///       <td>The alias of data field. This value is used as the column header.</td>
///     </tr>
///   </tbody>
/// </table>
/// <para><strong><u>Elements</u></strong></para>
/// <list type="table">
///   <listheader>
///     <term>Element</term>
///     <description>Description</description>
///   </listheader>
///   <item>
///     <term><see cref="BaseDataField.Header" /></term> 
///     <description>Reference to visual setting of header of the data field.</description>
///   </item>
///   <item>
///     <term><see cref="BaseDataField.Value" /></term> 
///     <description>Reference to visual setting of value of the data field.</description>
///   </item>
///   <item>
///     <term><see cref="BaseDataField.Aggregate" /></term> 
///     <description>Reference to visual setting of aggregate function of the data field.</description>
///   </item>
/// </list>
/// </remarks>
public partial class FixedField
{
    #region private members

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string _piece;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string _pieces;

    #endregion

    #region public properties

    /// <summary>
    /// Gets or sets the name of the field.
    /// </summary>
    /// <value>
    /// Name of the collection of pieces.
    /// Are only allow strings made ​​up of letters, numbers and following special chars <strong>'<c>_ - # @ % $</c>'</strong>.
    /// </value>
    /// <remarks>
    /// <para><strong><u>Usage</u></strong>:</para>
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <Fixed Pieces="string" ...>
    /// ...
    /// </Fixed>
    /// ]]>
    /// </code>
    /// </remarks>
    /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/></exception>
    /// <exception cref="InvalidIdentifierNameException"><paramref name="value" /> not is a valid identifier name.</exception>
    [JsonProperty]
    [XmlAttribute]
    public string Pieces
    {
        get => _pieces;
        set
        {
            SentinelHelper.ArgumentNull(value, nameof(value));

            SentinelHelper.IsFalse(
                ModelsRegularExpressionHelper.IsValidIdentifier(value), 
                new InvalidIdentifierNameException(
                    ErrorMessageHelper.ModelIdentifierNameErrorMessage(
                        "Fixed", 
                        nameof(Pieces), 
                        value)));

            _pieces = value;
        }
    }

    /// <summary>
    /// Gets or sets name of the piece.
    /// </summary>
    /// <value>
    /// Name of the piece.
    /// Are only allow strings made ​​up of letters, numbers and following special chars <strong>'<c>_ - # @ % $</c>'</strong>.
    /// </value>
    /// <remarks>
    /// <para><strong><u>Usage</u></strong>:</para>
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <Fixed Piece="string" ...>
    /// ...
    /// </Fixed>
    /// ]]>
    /// </code>
    /// </remarks>
    /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/></exception>
    /// <exception cref="InvalidIdentifierNameException"><paramref name="value" /> not is a valid identifier name.</exception>
    [JsonProperty]
    [XmlAttribute]
    public string Piece
    {
        get => _piece;
        set
        {
            SentinelHelper.ArgumentNull(value, nameof(value));

            SentinelHelper.IsFalse(
                ModelsRegularExpressionHelper.IsValidIdentifier(value),
                new InvalidIdentifierNameException(
                    ErrorMessageHelper.ModelIdentifierNameErrorMessage(
                        "Fixed",
                        nameof(Piece),
                        value)));

            _piece = value;
        }
    }

    #endregion
}
