
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

using Newtonsoft.Json;

namespace iTin.Core.Models.Design;

/// <summary>
/// Defines a generic interface that defines an element can be owner another reference.
/// </summary>
public interface ITenant
{
    /// <summary>
    /// Gets the element that owns this <see cref="IOwner"/>.
    /// </summary>
    /// <value>
    /// The <see cref="ITenant" /> that owns this <see cref="IOwner"/>.
    /// </value>
    [JsonIgnore]
    [XmlIgnore]
    [Browsable(false)]
    IOwner Owner { get; }

    /// <summary>
    /// Sets the owner of the provided <paramref name="item"/> to this <see cref="ITenant"/> instance.
    /// </summary>
    /// <param name="item">The <see cref="IOwner"/> for which to set the owner.</param>
    /// <remarks>
    /// This method assigns this <see cref="ITenant"/> instance as the owner of the specified <paramref name="item"/>, establishing a parent-child relationship between them.
    /// </remarks>
    void SetOwner(IOwner item);
}
