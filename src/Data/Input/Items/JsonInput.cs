
using System;

using iTin.Core.Models.Data.Provider;

namespace iTin.Core.Models.Data.Input;

/// <summary>
/// Class than allows you to export an object of type <see cref="Uri"/>.
/// </summary>
[DataInputOptions(AdapterType = typeof(JsonProvider))]
public class JsonInput : BaseDataInput
{
    /// <summary>
    /// Initializes a new instance of the <see cref="JsonInput"/> class.
    /// </summary>
    /// <param name="json">The Xml.</param>
    /// <param name="configuration"></param>
    public JsonInput(Uri json, DataInputConfiguration configuration)
        : base(json, configuration)
    {
    }
}
