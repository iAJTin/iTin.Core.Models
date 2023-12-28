
using System.Data;

using iTin.Core.Models.Data.Provider;

namespace iTin.Core.Models.Data.Input;

/// <summary>
/// Class than allows you to export an object of type <see cref="DataSet" />.
/// </summary>
[DataInputOptions(AdapterType = typeof(DataSetProvider))]
public class DataSetInput : BaseDataInput
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DataSetInput" /> class.
    /// </summary>
    /// <param name="dataSet">A <see cref="DataSet" /> object than contains the information.</param>
    public DataSetInput(DataSet dataSet) 
        : base(dataSet)
    {
    }
}
