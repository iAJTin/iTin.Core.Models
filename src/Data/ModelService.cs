
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml.Linq;

using iTin.Core.Models.Data.Input;
using iTin.Core.Models.Data.Provider;
using iTin.Core.Models.Design;
using iTin.Core.Models.Design.Enums;
using iTin.Core.Models.Design.Table;
using iTin.Core.Models.Design.Table.Fields;
using iTin.Core.Models.Design.Table.Resource;

namespace iTin.Core.Models.Data;

/// <summary>
/// Represents a service for managing model data and related information.
/// </summary>
public class ModelService
{
    #region public static readonly properties

    /// <summary>
    /// Gets a unique instance of the <see cref="ModelService"/> class.
    /// </summary>
    public static ModelService Instance { get; } = new();

    #endregion

    #region public readonly properties

    /// <summary>
    /// Gets the current column index.
    /// </summary>
    public int CurrentCol { get; private set; }

    /// <summary>
    /// Gets the current data field.
    /// </summary>
    public BaseDataField CurrentField { get; private set; }

    /// <summary>
    /// Gets the current filter applied to the data.
    /// </summary>
    public string CurrentFilter { get; private set; }

    /// <summary>
    /// Gets the current filter applied to the data.
    /// </summary>
    public IModel CurrentModel { get; private set; }

    /// <summary>
    /// Gets the current row index.
    /// </summary>
    public int CurrentRow { get; private set; }

    /// <summary>
    /// Gets the data input source.
    /// </summary>
    public IDataInput DataInput { get; private set; }

    /// <summary>
    /// Gets the data provider used for accessing data.
    /// </summary>
    public IDataProvider CurrentProvider { get; private set; }

    /// <summary>
    /// Gets the user-defined filters.
    /// </summary>
    public FiltersCollection Filters { get; private set; }

    /// <summary>
    /// Gets the raw data in <strong>XML</strong> format.
    /// </summary>
    public XElement[] RawData { get; private set; }

    /// <summary>
    /// Gets the external references reference.
    /// </summary>
    public ReferencesCollection References { get; private set; }

    /// <summary>
    /// Gets the filtered raw data based on the current filter in <strong>XML</strong> format.
    /// </summary>
    public XElement[] RawDataFiltered
    {
        get
        {
            var hasDataFilter = !string.IsNullOrEmpty(CurrentFilter);
            if (!hasDataFilter)
            {
                return RawData;
            }

            var filter = ((FiltersCollection)Filters).GetBy(CurrentFilter);
            if (filter == null)
            {
                return RawData;
            }


            if (filter.Active == YesNo.No)
            {
                return RawData;
            }

            var expression = filter.BuildFilterExpression();
            var rows = RawData.ToList().FindAll(item => expression.IsSatisfiedBy(item));

            return (XElement[])rows.ToArray().Clone();
        }
    }

    #endregion

    #region public methods

    /// <summary>
    /// Sets the current column index.
    /// </summary>
    /// <param name="col">The column index to set.</param>
    public void SetCurrentCol(int col)
    {
        CurrentCol = col;
    }

    /// <summary>
    /// Sets the current data field.
    /// </summary>
    /// <param name="field">The data field to set as current.</param>
    public void SetCurrentField(BaseDataField field)
    {
        CurrentField = field;
    }

    /// <summary>
    /// Sets the current row index.
    /// </summary>
    /// <param name="row">The row index to set.</param>
    public void SetCurrentRow(int row)
    {
        CurrentRow = row;
    }

    /// <summary>
    /// Sets the data input data model and initializes related properties.
    /// </summary>
    /// <param name="inputDataModel">The data input data model to set.</param>
    public void SetInputDataModel(InputDataModel inputDataModel)
    {
        References = inputDataModel.References;

        CurrentModel = inputDataModel.Model;
        Filters = CurrentModel.Resources.Filters;
        CurrentFilter = inputDataModel.CurrentFilter;

        DataInput = inputDataModel.DataInput;
        CurrentProvider = DataInput.GetDataProvider();
        CurrentProvider.SetInputDataModel(inputDataModel);
        RawData = CurrentProvider.ToXml().ToArray();
    }

    /// <summary>
    /// Tries to retrieve the underlying data as a <see cref="DataTable"/> from the data provider.
    /// </summary>
    /// <param name="data">The retrieved <see cref="DataTable"/> if successful; otherwise, null.</param>
    /// <returns>
    /// <see langword="true"/> if the data was successfully retrieved as a <see cref="DataTable"/>; otherwise, <see langword="false"/>.
    /// </returns>
    public bool TryGetUnderlyingDataAsDataTable(out DataTable data)
    {
        data = null;
        if (!CurrentProvider.CanGetDataTable)
        {
            return false;
        }

        data = CurrentProvider.ToDataTable();

        return true;
    }

    /// <summary>
    /// Tries to retrieve the underlying data as an <see cref="IEnumerable{XElement}"/> from the data provider.
    /// </summary>
    /// <param name="data">The retrieved <see cref="IEnumerable{XElement}"/> if successful.</param>
    /// <returns>
    /// <see langword="true"/> if the data was successfully retrieved as a <see cref="DataTable"/>; otherwise, <see langword="false"/>.
    /// </returns>
    public bool TryGetUnderlyingDataAsXml(out IEnumerable<XElement> data)
    {
        try
        {
            data = CurrentProvider.ToXml();

            return true;
        }
        catch
        {
            data = Enumerable.Empty<XElement>();

            return false;
        }
    }

    #endregion

    #region public override methods

    /// <summary>
    /// Returns a string representation of the <see cref="ModelService"/> instance.
    /// </summary>
    /// <returns>A string containing information about the data provider.</returns>
    public override string ToString() => $"Model=\"{CurrentModel.Name}\", Provider=\"{CurrentProvider.GetType().Name}\"";

    #endregion
}
