
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using iTin.Core.Models.Data.Provider;

namespace iTin.Core.Models.Data.Input;

/// <summary>
/// Base class for the different input types.
/// Which acts as the base class for the different input types.
/// </summary>
/// <remarks>
///   <para>
///   The following table shows the different input types.
///   </para>
///   <list type="table">
///     <listheader>
///       <term>Class</term>
///       <description>Description</description>
///     </listheader>
///     <item>
///       <term><see cref="ArrayListInput{TData}"/></term>
///       <description>Represents an input for array of <see cref="ArrayList"/> types. For more information please see <see cref="ArrayListInput{TData}" /></description>
///     </item>
///     <item>
///       <term><see cref="DataRowInput"/></term>
///       <description>Represents an input for array of <see cref="DataRow"/> types. For more information please see <see cref="DataRowInput" /></description>
///     </item>
///     <item>
///       <term><see cref="DataSetInput"/></term>
///       <description>Represents an input for <see cref="DataSet"/> types. For more information please see <see cref="DataSetInput" /></description>
///     </item>
///     <item>
///       <term><see cref="DataTableInput"/></term>
///       <description>Represents an input for <see cref="DataTable"/> types. For more information please see <see cref="DataTableInput" /></description>
///     </item>
///     <item>
///       <term><see cref="EnumerableInput{TData}"/></term>
///       <description>Represents an input for <see cref="IEnumerable{TData}"/> types. For more information please see <see cref="EnumerableInput{TData}" /></description>
///     </item>
///     <item>
///       <term><see cref="XmlInput"/></term>
///       <description>Represents an input for <c>XML</c> type. For more information please see <see cref="XmlInput"/></description>
///     </item>
///     <item>
///       <term><see cref="JsonInput"/></term>
///       <description>Represents an input for <c>Json</c> type. For more information please see <see cref="JsonInput"/></description>
///     </item>
///   </list>
/// </remarks>
public abstract class BaseDataInput : IDataInput
{
    #region constructor/s

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseDataInput" /> class.
    /// </summary>
    /// <param name="data">The data.</param>
    protected BaseDataInput(object data)
    {
        Data = data;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseDataInput" /> class.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="configuration"></param>
    protected BaseDataInput(object data, DataInputConfiguration configuration)
    {
        Data = data;
        Configuration = configuration;
    }

    #endregion

    #region public properties

    /// <summary>
    /// Gets a reference that contains the input data to export.
    /// </summary>
    /// <value>
    /// A <see cref="T:System.Object" /> that contains the input data to export.
    /// </value>
    public object Data { get; protected set; }

    public DataInputConfiguration Configuration { get; protected set; }

    #endregion

    #region public methods

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public IDataProvider GetDataProvider()
    {
        var attributes = this.GetType().GetCustomAttributes(false);
        var inputOptionAttribute = (DataInputOptionsAttribute)attributes.SingleOrDefault(attr => attr is DataInputOptionsAttribute);

        var dataProvider = inputOptionAttribute!.AdapterType;

        var dataProviderInstance = Activator.CreateInstance(
            dataProvider, 
            dataProvider == typeof(JsonProvider) 
                ? new[] { Data, new DataProviderConfiguration { InputNodes = Configuration.InputNodes, OutputTable = Configuration.OutputTable} }
                : new[] { Data });

        return (IDataProvider)dataProviderInstance;
    }

    #endregion
}
