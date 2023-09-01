
using iTin.Core.Models.Design.ComponentModel;

namespace iTin.Core.Models.Design.Table.Resource;

/// <summary>
/// Declares a generic condition to use.
/// </summary>
public interface ICondition
{
    /// <summary>
    /// Returns result of evaluates condition.
    /// </summary>
    /// <returns>
    /// A <see cref="ConditionResult"/> object that contains evaluate result.
    /// </returns>
    ConditionResult Evaluate();

    /// <summary>
    /// Returns result of evaluates condition.
    /// </summary>
    /// <param name="row">Data row</param>
    /// <param name="col">Field column</param>
    /// <returns>
    /// A <see cref="ConditionResult"/> object that contains evaluate result.
    /// </returns>
    ConditionResult Evaluate(int row, int col);

    /// <summary>
    /// Returns result of evaluates condition.
    /// </summary>
    /// <param name="row">Data row</param>
    /// <param name="col">Field column</param>
    /// <param name="target">Field data</param>
    /// <returns>
    /// A <see cref="ConditionResult"/> object that contains evaluate result.
    /// </returns>
    ConditionResult Evaluate(int row, int col, FieldValueInformation target);
}
