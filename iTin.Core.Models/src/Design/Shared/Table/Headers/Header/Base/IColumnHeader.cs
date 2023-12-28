
using System;
using System.ComponentModel;
using System.Xml.Serialization;

using Newtonsoft.Json;

using iTin.Core.Models.Design.Enums;

namespace iTin.Core.Models.Design;

/// <summary>
/// Defines a generic column header
/// </summary>
public interface IColumnHeader : ICloneable, ITenant
{
    /// <summary>
    /// Gets a value indicating whether this instance is default.
    /// </summary>
    /// <value>
    /// <b>true</b> if this instance contains the default; otherwise, <b>false</b>.
    /// </value>
    bool IsDefault { get; }


    /// <summary>
    /// Gets or sets begin column name.
    /// </summary>
    /// <value>
    /// Begin column name.
    /// </value>
    [JsonProperty]
    [XmlAttribute]
    string From { get; set; }

    /// <summary>
    /// Gets or sets end column name.
    /// </summary>
    /// <value>
    /// Begin column name.
    /// </value>
    [JsonProperty]
    [XmlAttribute]
    string To { get; set; }

    /// <summary>
    /// Gets or sets style name.
    /// </summary>
    /// <value>
    /// Style name.
    /// </value>
    [JsonProperty]
    [XmlAttribute]
    string Style { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether show column header.
    /// </summary>
    /// <value>
    /// <see cref="YesNo.Yes"/> displays column header; Otherwise, <see cref="YesNo.No"/>. The default is <see cref="YesNo.Yes"/>.
    /// </value>
    /// <exception cref="InvalidEnumArgumentException">
    /// <paramref name="value"/> is not part of the enumeration.
    /// 
    /// -or-
    /// 
    /// <paramref name="value"/> is not an enumerated type.
    /// </exception>
    [JsonProperty]
    YesNo Show { get; set; }

    /// <summary>
    /// Gets or sets text of column header.
    /// </summary>
    /// <value>
    /// Text of column header.
    /// </value>
    [JsonProperty]
    [XmlAttribute]
    public string Text { get; set; }


    /// <summary>
    /// Sets the element that owns this <see cref="IColumnHeader"/>.
    /// </summary>
    /// <param name="reference">Reference to owner.</param>
    void SetOwner(IColumnsHeaders reference);
}
