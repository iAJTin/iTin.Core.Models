
using System;
using System.Diagnostics;
using System.Xml.Serialization;

using Newtonsoft.Json;

using iTin.Core.Helpers;
using iTin.Core.Models.ComponentModel.Exceptions;
using iTin.Core.Models.Design.Constants;
using iTin.Core.Models.Helpers;

using ModelsRegularExpressionHelper = iTin.Core.Models.Helpers.RegularExpressionHelper;

namespace iTin.Core.Models.Design.Table.Fields;

/// <summary>
/// 
/// </summary>
public partial class PacketField
{
    #region private members

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string _name;

    #endregion

    #region public properties

    /// <summary>
    /// 
    /// </summary>
    [JsonProperty]
    [XmlAttribute]
    public string InputFormat { get; set; }

    /// <summary>
    /// Gets or sets the name of the field.
    /// </summary>
    /// <value>
    /// The name of the field.
    /// Are only allow strings made ​​up of letters, numbers and following special chars <strong>'<c>_ - # * @ % $</c>'</strong>.
    /// </value>
    /// <remarks>
    /// <para><strong><u>Usage</u></strong>:</para>
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <Field Name="string" ...>
    /// ...
    /// </Field>
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
                ModelsRegularExpressionHelper.IsValidFieldName(value),
                new InvalidFieldIdentifierNameException(
                    ErrorMessageHelper.FieldIdentifierNameErrorMessage(
                        "Field", 
                        "Name", 
                        value)));

            _name = value;
        }
    }

    #endregion

    #region public static methods

    /// <summary>
    /// Returns input packet data format.
    /// </summary>
    /// <param name="format">Input format.</param>
    /// <returns>
    /// A value than represents input packet data format.
    /// </returns>
    public static string GetInputFormat(string format) =>
        format switch
        {
            nameof(KnownInputPacketFormat.ShortDateFormat) => KnownInputPacketFormat.ShortDateFormat,
            nameof(KnownInputPacketFormat.LongDateFormat) => KnownInputPacketFormat.LongDateFormat,
            nameof(KnownInputPacketFormat.FullDateFormat) => KnownInputPacketFormat.FullDateFormat,
            _ => format
        };

    #endregion
}
