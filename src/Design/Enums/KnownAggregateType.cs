
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;

using System;

namespace iTin.Core.Models.Design.Enums;

/// <summary>
/// Defines availables aggregate types.
/// </summary>
[Serializable]
[JsonConverter(typeof(StringEnumConverter))]
public enum KnownAggregateType
{
    /// <summary>
    /// None aggregate
    /// </summary>
    None,

    /// <summary>
    /// Average aggregate
    /// </summary>        
    Average,

    /// <summary>
    /// Count aggregate
    /// </summary>        
    Count,

    /// <summary>
    /// Max aggregate
    /// </summary>        
    Max,

    /// <summary>
    /// Min aggregate
    /// </summary>        
    Min,

    /// <summary>
    /// Sum aggregate
    /// </summary>
    Sum
}
