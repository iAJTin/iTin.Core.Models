
namespace iTin.Core.Models.Design.Table.Fields;

/// <summary>
/// A Specialization of <see cref="BaseDataField"/> class.<br/>
/// Represents an empty data field.
/// </summary>
/// <remarks>
/// <para>Belongs to: <strong><c>Fields</c></strong>. For more information, please see <see cref="Fields"/>.</para>
/// <para><strong><u>Usage</u></strong>:</para>
/// <code lang="xml" title="ITEE Object Element Usage">
/// <![CDATA[
/// <Gap ...>
///   <Header/>
///   <Value/>
///   <Aggregate/>
/// </Gap>
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
///       <td><see cref="BaseDataField.Alias"/></td>
///       <td align="center">Yes</td>
///       <td>The alias of data field. This value is used as the column header.</td>
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
public partial class GapField
{
}
