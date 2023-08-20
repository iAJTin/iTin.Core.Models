
using System.ComponentModel;
using System.Xml.Serialization;

using Newtonsoft.Json;

namespace iTin.Core.Models.Design;

public partial class BaseFilter
{
    /// <summary>
    /// Gets the element that owns this instance.
    /// </summary>
    /// <value>
    /// The reference that owns this instance.
    /// </value>
    [JsonIgnore]
    [XmlIgnore]
    [Browsable(false)]
    IOwner ITenant.Owner => Owner;

    /// <summary>
    /// Sets the element that owns this instance.
    /// </summary>
    /// <param name="reference">Reference to owner.</param>
    void ITenant.SetOwner(IOwner reference) => SetOwner((IFilters)reference);


    /// <summary>
    /// Sets the element that owns this <see cref="IFilters"/>.
    /// </summary>
    /// <param name="reference">Reference to owner.</param>
    internal void SetOwner(IFilters reference)
    {
        Owner = reference;
    }
}
