
using System.Data;
using System.Linq;

using iTin.Core.Helpers;

using iTin.Core.Models.Data.Provider;

namespace iTin.Core.Models.Data.Input;

/// <summary>
/// Class than allows you to export an object of type <see cref="DataTable" />.
/// </summary>
[DataInputOptions(AdapterType = typeof(DataSetProvider))]
public class DataTableInput : DataRowInput
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DataTableInput" /> class.
    /// </summary>
    /// <param name="table">The table.</param>
    public DataTableInput(DataTable table)
        : this(SentinelHelper.PassThroughNonNull(table), SentinelHelper.PassThroughNonNull(table).TableName)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DataTableInput" /> class.
    /// </summary>
    /// <param name="table">The table.</param>
    /// <param name="name">The name.</param>
    public DataTableInput(DataTable table, string name)
        : base(SentinelHelper.PassThroughNonNull(table).Rows.Cast<DataRow>(), name)
    {
    }
}
