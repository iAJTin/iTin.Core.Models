
namespace iTin.Core.Models.Design.Table.Resource;

/// <summary>
/// Collection of user-defined field groups. Each element is result of the union of a field list.
/// </summary>
/// <remarks>
/// Belongs to: <strong><c>Resources</c></strong>. For more information, please see <see cref="Resources"/>.
/// <para><u><strong>Usage</strong></u></para>
/// <code lang="xml" title="ITEE Object Element Usage">
/// <![CDATA[
/// <Groups>
///   <Group .../>
///   <Group .../>
///   ...
/// </Groups>
/// ]]>
/// </code>
/// </remarks>
public partial class GroupsCollection
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GroupsCollection"/> class with a parent instance.
    /// </summary>
    /// <param name="parent">The parent <see cref="Resources"/> instance.</param>
    public GroupsCollection(Resources parent)
        : base(parent)
    {
    }
}
