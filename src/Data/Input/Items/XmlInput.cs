
using System;

using iTin.Core.Models.Data.Provider;

namespace iTin.Core.Models.Data.Input;

/// <summary>
/// Class than allows you to export an object of type <see cref="Uri"/>.
/// </summary>
[DataInputOptions(AdapterType = typeof(XmlProvider))]
public class XmlInput : BaseDataInput
{
    /// <summary>
    /// Initializes a new instance of the <see cref="XmlInput"/> class.
    /// </summary>
    /// <param name="xml">The XML.</param>
    public XmlInput(Uri xml)
        : base(xml)
    {
    }
}
