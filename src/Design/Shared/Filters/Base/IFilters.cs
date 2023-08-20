
using System;

using Newtonsoft.Json;

namespace iTin.Core.Models.Design;

/// <summary>
/// Defines a generic filters collection
/// </summary>
[JsonArray(AllowNullItems = true)]
public interface IFilters : ICloneable, IOwner
{
}
