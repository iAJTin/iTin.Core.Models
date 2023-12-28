
namespace iTin.Core.Models.Design.Table.Resource;

/// <summary>
/// Collection of user-defined pieces. Each element is a collection of smaller pieces result of splitting the reference field.
/// </summary>
/// <remarks>
/// Belongs to: <strong><c>Resources</c></strong>. For more information, please see <see cref="Resources" />.
/// <para><u><strong>Usage</strong></u></para>
/// <code lang="xml" title="ITEE Object Element Usage">
/// <![CDATA[
/// <Fixed>
///   <Pieces/>
///   <Pieces/>
///   ...
/// </Fixed>
/// ]]>
/// </code>
/// </remarks>
public partial class FixedCollection
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FixedCollection"/> class with a parent resources instance.
    /// </summary>
    /// <param name="parent">The parent <see cref="Resources"/> instance.</param>
    public FixedCollection(Resources parent)
        : base(parent)
    {
    }
}
