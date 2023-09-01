
using System.Collections.Generic;
using System.Data;

using iTin.Core.Helpers;

using iTin.Core.Models.Data.Provider;

namespace iTin.Core.Models.Data.Input;

/// <summary>
/// Class than allows you to export an object of type <see cref="DataRow"/>.
/// </summary>
/// <typeparam name="T">Enumeration type</typeparam>
[DataInputOptions(AdapterType = typeof(DataSetProvider))]
public class EnumerableInput<T> : DataTableInput where T : class
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EnumerableInput{T}"/> class.
    /// </summary>
    /// <param name="data">A <see cref="DataRow"/> array object than contains the information.</param>
    /// <param name="name">The name.</param>
    public EnumerableInput(IEnumerable<T> data, string name)
        : base(SentinelHelper.PassThroughNonNull(data.ToDataTable<T>(name)))
    {
    }
}
