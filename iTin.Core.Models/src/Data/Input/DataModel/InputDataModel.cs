
using iTin.Core.Models.Design;
using iTin.Core.Models.Design.Table;

namespace iTin.Core.Models.Data.Input;

/// <summary>
/// Represents a model for input data, including the model itself, data input, resources, and filters.
/// </summary>
public class InputDataModel
{
    #region public properties

    /// <summary>
    /// Gets or sets the filter to apply to the input data model.
    /// </summary>
    public string CurrentFilter { get; set; }

    /// <summary>
    /// Gets or sets the data input for the input data model.
    /// </summary>
    public IDataInput DataInput { get; set; }

    /// <summary>
    /// Gets or sets the model associated with the input data.
    /// </summary>
    public IModel Model { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public ReferencesCollection References { get; set; }

    #endregion

    #region public methods

    /// <summary>
    /// Creates an instance of the <see cref="ModelService"/> class based on the current input data model.
    /// </summary>
    /// <returns>
    /// An instance of <see cref="ModelService"/> configured with the current input data model.
    /// </returns>
    public ModelService CreateService()
    {
        var service = ModelService.Instance;
        service.SetInputDataModel(this);

        return service;
    }

    #endregion
}
