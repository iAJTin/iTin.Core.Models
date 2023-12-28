
using iTin.Core.Models.Design.Table.Fields;

namespace iTin.Core.Models.Design.Enums;

/// <summary>
/// Specifies data field type.
/// </summary>
public enum KnownFieldType
{
    /// <summary>
    /// Data field, please see <see cref="DataField"/> for more information.
    /// </summary>
    Field,

    /// <summary>
    /// Gap field, please see <see cref="GapField"/> for more information.
    /// </summary>
    Gap,

    /// <summary>
    /// Group field, please see <see cref="GroupField"/> for more information.
    /// </summary>
    Group,

    /// <summary>
    /// Fixed field, please see <see cref="FixedField"/> for more information.
    /// </summary>
    Fixed,

    /// <summary>
    /// Packet data field, please see <see cref="PacketField"/> for more information.
    /// </summary>
    Packet
}
