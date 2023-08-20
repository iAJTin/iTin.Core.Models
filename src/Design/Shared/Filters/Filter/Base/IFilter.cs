
using System;
using System.ComponentModel;

using Newtonsoft.Json;

using iTin.Core.Models.Design.Enums;
using System.Xml.Serialization;
using System.Xml.Linq;
using iTin.Core.ComponentModel.Patterns;

namespace iTin.Core.Models.Design;

/// <summary>
/// Defines a generic style
/// </summary>
public interface IFilter : ICloneable, ITenant
{
    /// <summary>
    /// Gets a value indicating whether this filter is an empty filter.
    /// </summary>
    /// <value>
    /// <b>true</b> if is an empty style; otherwise, <b>false</b>.
    /// </value>        
    bool IsEmpty { get; }


    /// <summary>
    /// Gets or sets value indicating whether this filter is active.
    /// </summary>
    /// <value>
    /// <b><see cref="YesNo.Yes"/></b> if is an active filter; otherwise, <b><see cref="YesNo.No"/></b>.
    /// </value>
    /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/></exception>
    [XmlAttribute]
    [JsonProperty]
    YesNo Active { get; set; }

    /// <summary>
    /// Gets or sets preferred filter operator.
    /// </summary>
    /// <value>
    /// Preferred filter operator.
    /// </value>
    /// <exception cref="InvalidEnumArgumentException">
    /// <paramref name="value"/> is not part of the enumeration.
    /// 
    /// -or-
    /// 
    /// <paramref name="value"/> is not an enumerated type.
    /// </exception>
    [XmlAttribute]
    [JsonProperty]
    KnownOperator Criterial { get; set; }

    /// <summary>
    /// Gets or sets the target field acts as filter.
    /// </summary>
    /// <value>
    /// The target field acts as filter.
    /// </value>
    /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/></exception>
    /// <exception cref="InvalidEnumArgumentException"><paramref name="value"/> is not a valid field identifier</exception>
    [XmlAttribute]
    [JsonProperty]
    string Field { get; set; }


    /// <summary>
    /// Gets or sets the filter key into filters collection.
    /// </summary>
    /// <value>
    /// The filter key into filters collection.
    /// </value>
    /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/></exception>
    [XmlAttribute]
    [JsonProperty]
    string Key { get; set; }

    /// <summary>
    /// Gets or sets the filter value.
    /// </summary>
    /// <value>
    /// The filter value.
    /// </value>
    /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/></exception>
    [XmlAttribute]
    [JsonProperty]
    string Value { get; set; }


    /// <summary>
    /// 
    /// </summary>
    /// <returns>
    /// </returns>
    ISpecification<XElement> BuildFilterExpression();

    /// <summary>
    /// Sets the element that owns this <see cref="IFilter"/>.
    /// </summary>
    /// <param name="reference">Reference to owner.</param>
    void SetOwner(IFilters reference);
}
