
using iTin.Core.Models.Design.Constants;

namespace iTin.Core.Models.Design.Table.Fields;

/// <summary>
/// A Specialization of <see cref="DataField"/> class.<br/>
/// Represents a field as union of several data field.
/// </summary>
/// <remarks>
/// <para>Belongs to: <strong><c>Fields</c></strong>. For more information, please see <see cref="Fields"/>.</para>
/// <strong><u>Usage</u></strong>:
/// <code lang="xml" title="ITEE Object Element Usage">
/// <![CDATA[
/// <Group ...>
///   <Header/>
///   <Value/>
///   <Aggregate/>
/// </Group>
/// ]]>
/// </code>
/// <para><strong><u>Attributes</u></strong></para>
/// <table>
///   <thead>
///     <tr>
///       <th>Attribute</th>
///       <th>Optional</th>
///       <th>Description</th>
///       </tr>
///   </thead>
///   <tbody>
///     <tr>
///       <td><see cref="DataField.Name"/></td>
///       <td align="center">No</td>
///       <td>Name of the group field.</td>
///     </tr>
///     <tr>
///       <td><see cref="BaseDataField.Alias" /></td>
///       <td align="center">Yes</td>
///       <td>The alias of group data field. This value is used as the column header.</td>
///     </tr>
///   </tbody>
/// </table>
/// <para><strong><u>Elements</u></strong></para>
/// <list type="table">
///   <listheader>
///     <term>Element</term>
///     <description>Description</description>
///   </listheader>
///   <item>
///     <term><see cref="BaseDataField.Header"/></term> 
///     <description>Reference to visual setting of header of the data field.</description>
///   </item>
///   <item>
///     <term><see cref="BaseDataField.Value"/></term> 
///     <description>Reference to visual setting of value of the data field.</description>
///   </item>
///   <item>
///     <term><see cref="BaseDataField.Aggregate"/></term> 
///     <description>Reference to visual setting of aggregate function of the data field.</description>
///   </item>
/// </list>
/// </remarks>
public partial class GroupField
{
    /// <summary>
    /// Returns separator char.
    /// </summary>
    /// <param name="separator">Separator text.</param>
    /// <returns>
    /// A value than represents separator char.
    /// </returns>
    public static string GetSeparatorChar(string separator) =>
        separator switch
        {
            "None" => KnownItemGroupSeparator.EmptySeparator,
            "Space" => KnownItemGroupSeparator.SpaceSeparator,
            "Backslash" => KnownItemGroupSeparator.BackslashSeparator,
            "Dash" => KnownItemGroupSeparator.DashSeparator,
            "Dot" => KnownItemGroupSeparator.DotSeparator,
            "Comma" => KnownItemGroupSeparator.CommaSeparator,
            "Colon" => KnownItemGroupSeparator.ColonSeparator,
            "Slash" => KnownItemGroupSeparator.SlashSeparator,
            "Semi Colon" => KnownItemGroupSeparator.SemiColonSeparator,
            "New Line" => KnownItemGroupSeparator.NewLineSeparator,
            _ => separator
        };
}
