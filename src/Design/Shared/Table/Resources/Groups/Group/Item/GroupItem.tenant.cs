
using System;
using System.Diagnostics;

using iTin.Core.Helpers;

namespace iTin.Core.Models.Design.Table.Resource;

public partial class GroupItem : ITenant
{
    /// <inheritdoc/>
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    IOwner ITenant.Owner => Owner;

    /// <inheritdoc/>
    void ITenant.SetOwner(IOwner item) => SetOwner((Group)item);

    /// <summary>
    /// Sets the owner of the provided <paramref name="item"/> to this <see cref="Group"/> instance.
    /// </summary>
    /// <param name="item">The <see cref="GroupItem"/> for which to set the owner.</param>
    /// <remarks>
    /// This method assigns this <see cref="Group"/> instance as the owner of the specified <paramref name="item"/>, establishing a parent-child relationship between them.
    /// </remarks>
    /// <exception cref="ArgumentNullException"><paramref name="item"/> is <see langword="null"/></exception>
    internal void SetOwner(Group item)
    {
        SentinelHelper.ArgumentNull(item, nameof(item));

        Owner = item;
    }
}
