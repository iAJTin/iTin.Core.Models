
namespace iTin.Core.Models.Design.Table.Resource;

/// <summary>
/// Collection of user-defined field groups. Each element is result of the union of a field list.
/// </summary>
/// <remarks>
/// <para>
/// Belongs to: <strong><c>Resources</c></strong>. For more information, please see <see cref="Resources" />.
/// <code lang="xml" title="ITEE Object Element Usage">
/// <![CDATA[
/// <?xml version="1.0" encoding="utf-8">
/// <Groups>
///   <Group/>
///   <Group/>
///   ...
/// </Groups>
/// ]]>
/// </code>
/// </para>    
/// </remarks>
public partial class ConditionsCollection
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Groups"/> class with a parent resources instance.
    /// </summary>
    /// <param name="parent">The parent <see cref="Resources"/> instance.</param>
    public ConditionsCollection(Resources parent)
        : base(parent)
    {
    }
}
