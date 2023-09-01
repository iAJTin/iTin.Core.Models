
namespace iTin.Core.Models.Design.Table.Resource;

/// <summary>
/// Collection of user-defined filters. Each element is a data filter specification.
/// </summary>
/// <remarks>
/// <para>
/// Belongs to: <strong><c>Resources</c></strong>. For more information, please see <see cref="Resources"/>.
/// <para><u><strong>Usage</strong></u></para>
/// <code lang="xml" title="ITEE Object Element Usage">
/// <![CDATA[
/// <Filters>
///   <Filter .../>
///   <Filter .../>
///   ...
/// </Filters>
/// ]]>
/// </code>
/// </para>    
/// </remarks>
public partial class FiltersCollection
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FiltersCollection"/> class with a parent instance.
    /// </summary>
    /// <param name="parent">The parent <see cref="Resources"/> instance.</param>
    public FiltersCollection(Resources parent) : base(parent)
    {
    }
}
