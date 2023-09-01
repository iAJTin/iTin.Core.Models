
using System.Xml.Serialization;

using Newtonsoft.Json;

using iTin.Core.Models.Design.Enums;
using iTin.Core.Models.Design.Table;

namespace iTin.Core.Models.Design;

/// <summary>
/// Defines a generic model
/// </summary>
public interface IModel : IParent
{
    /// <summary>
    /// Gets or sets preferred model name.
    /// </summary>
    /// <value>
    /// Preferred model name
    /// </value>
    [XmlAttribute]
    [JsonProperty("name")]
    string Name { get; set; }

    /// <summary>
    /// Gets or sets the resources used by the input data model.
    /// </summary>
    [JsonProperty]
    [XmlElement]
    public Resources Resources { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [XmlAttribute]
    [JsonProperty("name")]
    YesNo Show { get; set; }
}
