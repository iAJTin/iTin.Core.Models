
using System.Collections;

using iTin.Core.Helpers;
using iTin.Core.Models.Data.Provider;

namespace iTin.Core.Models.Data.Input;

/// <summary>
/// Class than allows you to export an object of type <see cref="ArrayList" />.
/// </summary>
/// <typeparam name="T"></typeparam>
[DataInputOptions(AdapterType = typeof(DataSetProvider))]
public class ArrayListInput<T> : DataTableInput
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ArrayListInput{T}" /> class.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="name">The name.</param>
    public ArrayListInput(ArrayList data, string name)
        : base(SentinelHelper.PassThroughNonNull(data.ToDataTable<T>(name)))
    {
    }
}
