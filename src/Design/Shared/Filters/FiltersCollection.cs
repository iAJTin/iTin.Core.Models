
namespace iTin.Core.Models.Design;

/// <summary>
/// Defines a filters collection.
/// </summary>
public partial class FiltersCollection : IFilters
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FiltersCollection"/> class.
    /// </summary>
    public FiltersCollection() : base(null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FiltersCollection"/> class.
    /// </summary>
    /// <param name="parent">The parent.</param>
    public FiltersCollection(object parent) : base(parent)
    {
    }
}
