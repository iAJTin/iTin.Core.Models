
using iTin.Core.Models.Design.Table.Fields;

namespace iTin.Core.Models.Design.Constants;

/// <summary>
/// Defines availables packet input data formats.
/// </summary>
public static class KnownInputPacketFormat
{
    /// <summary>
    /// Represents ShortDateFormat. For more information please see <see cref="P:iTin.Export.Model.PacketFieldModel.InputFormat"/>.
    /// </summary>
    public const string ShortDateFormat = "ShortDateFormat";

    /// <summary>
    /// Represents LongDateFormat. For more information please see <see cref="PacketField.InputFormat"/>.
    /// </summary>
    public const string LongDateFormat = "LongDateFormat";

    /// <summary>
    /// Represents FullDateFormat. For more information please see <see cref="PacketField.InputFormat"/>.
    /// </summary>
    public const string FullDateFormat = "FullDateFormat";
}
