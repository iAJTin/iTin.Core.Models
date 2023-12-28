﻿
using System.Collections.Generic;
using System.Data;

using iTin.Core.Helpers;
using iTin.Core.Models.Data.Provider;

namespace iTin.Core.Models.Data.Input;

/// <summary>
/// Class than allows you to export an object of type <see cref="System.Data.DataRow"/>.
/// </summary>
[DataInputOptions(AdapterType = typeof(DataSetProvider))]
public class DataRowInput : DataSetInput
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DataRowInput" /> class.
    /// </summary>
    /// <param name="rows">A <see cref="DataRow"/> array object than contains the information.</param>
    /// <param name="name">The name.</param>
    protected DataRowInput(IEnumerable<DataRow> rows, string name)
        : base(SentinelHelper.PassThroughNonNull(CreateDataSetFrom(rows, name)))
    {
    }

    /// <summary>
    /// Creates the data set from.
    /// </summary>
    /// <param name="rows">The rows.</param>
    /// <param name="name">The name.</param>
    /// <returns>
    /// <see cref="DataSet" /> which contains the specified rows.
    /// </returns>
    private static DataSet CreateDataSetFrom(IEnumerable<DataRow> rows, string name)
    {
        var dt = rows.CopyToDataTable();
        dt.TableName = name;
        
        using var tempDs = new DataSet();
        tempDs.Locale = dt.Locale;
        tempDs.Tables.Add(dt);

        var ds = tempDs;

        return ds;
    }
}
