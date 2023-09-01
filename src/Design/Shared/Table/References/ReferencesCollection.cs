
namespace iTin.Core.Models.Design.Table;

/// <summary>
/// Collection of external assembly references. Each element defines an external assembly reference.
/// </summary>
/// <remarks>
/// <para>Belongs to: <strong><c>Table</c></strong>. For more information, please see <see cref="TableDefinition"/></para>
/// <para><strong><u>Usage</u></strong>:</para>
/// <code lang="xml" title="ITEE Object Element Usage">
/// <![CDATA[
/// <Table ...>
///   <References>
///     <Reference .../>
///     ...
///     ...
///   </References>
/// </Table>
/// ]]>
/// </code>
/// </remarks>
public partial class ReferencesCollection
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ReferencesCollection"/> class with a parent instance.
    /// </summary>
    /// <param name="parent">The parent <see cref="TableDefinition"/> instance.</param>
    public ReferencesCollection(TableDefinition parent) : base(parent)
    {
    }
}
