
using System;
using System.Diagnostics;

using iTin.Core.Helpers;

namespace iTin.Core.Models.Design.Table;

public partial class Reference : ITenant
{
    /// <inheritdoc/>
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    IOwner ITenant.Owner => Owner;

    /// <inheritdoc/>
    void ITenant.SetOwner(IOwner item) => SetOwner((ReferencesCollection)item);

    /// <summary>
    /// Sets the owner of the provided <paramref name="item"/> to this <see cref="ReferencesCollection"/> instance.
    /// </summary>
    /// <param name="item">The <see cref="Reference"/> for which to set the owner.</param>
    /// <remarks>
    /// This method assigns this <see cref="ReferencesCollection"/> instance as the owner of the specified <paramref name="item"/>, establishing a parent-child relationship between them.
    /// </remarks>
    /// <exception cref="ArgumentNullException"><paramref name="item"/> is <see langword="null"/></exception>
    internal void SetOwner(ReferencesCollection item)
    {
        SentinelHelper.ArgumentNull(item, nameof(item));

        Owner = item;
    }
}
