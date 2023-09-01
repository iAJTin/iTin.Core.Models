
using System;
using System.Diagnostics;
using System.Xml.Serialization;

using iTin.Core.Helpers;
using iTin.Core.Models.ComponentModel.Exceptions;
using iTin.Core.Models.Design.ComponentModel;
using iTin.Core.Models.Design.Table.Fields;
using iTin.Core.Models.Helpers;

using ModelRegularExpressionHelper = iTin.Core.Models.Helpers.RegularExpressionHelper;

namespace iTin.Core.Models.Design.Table.Resource;

/// <summary>
/// Represents a field condition. Defines the style that will be applied to the field when it changes its value.
/// </summary>
public partial class WhenChangeCondition : ICloneable
{
    #region private members

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string _lastStyle;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string _fisrtSwapStyle;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string _secondSwapStyle;

    #endregion

    #region public static properties

    /// <summary>
    /// Gets an empty condition.
    /// </summary>
    /// <value>
    /// An empty condition.
    /// </value>
    public static WhenChangeCondition Empty => new();

    #endregion

    #region public readonly properties

    /// <summary>
    /// Gets a value indicating whether this condition is an empty condition.
    /// </summary>
    /// <value>
    /// <see langword="true"/> if is an empty condition; otherwise, <see langword="false"/>.
    /// </value>        
    public bool IsEmpty => IsDefault;

    #endregion

    #region public properties

    /// <summary>
    /// Gets or sets a value that represents the first style that is applied when the condition is met.
    /// </summary>
    /// <value>
    /// A <see cref ="string"/> that represents the first style that is applied when the condition is met.
    /// </value>
    /// <remarks>
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <?xml version="1.0" encoding="utf-8">
    /// <WhenChangeValue FirstSwapStyle="string" .../>
    /// ]]>
    /// </code>
    /// </remarks>
    /// <example>
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <?xml version="1.0" encoding="utf-8">
    /// <Resources>
    ///   <Conditions>
    ///     <WhenChangeValue Key="wchg" Active="Yes" Field="TOTAL" EntireRow="No" FirstSwapStyle="firstTotalStyle" SecondSwapStyle="secondTotalStyle"/>
    ///     ...
    ///   </Conditions>
    /// </Resources>
    /// ]]>
    /// </code>
    /// </example>
    [XmlAttribute]
    public string FirstSwapStyle
    {
        get => GetStaticBindingValue(_fisrtSwapStyle);
        set
        {
            SentinelHelper.ArgumentNull(value, nameof(value));
            SentinelHelper.IsFalse(
                ModelRegularExpressionHelper.IsValidIdentifier(value), 
                new InvalidIdentifierNameException(ErrorMessageHelper.ModelIdentifierNameErrorMessage(GetType().Name, nameof(FirstSwapStyle), value)));

            _fisrtSwapStyle = value;
        }
    }

    /// <summary>
    /// Gets or sets a value that represents the second style that is applied when the condition is met.
    /// </summary>
    /// <value>
    /// A <see cref ="string"/> that represents the second style that is applied when the condition is met.
    /// </value>
    /// <remarks>
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <?xml version="1.0" encoding="utf-8">
    /// <WhenChangeValue SecondSwapStyle="string" .../>
    /// ]]>
    /// </code>
    /// </remarks>
    /// <example>
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <?xml version="1.0" encoding="utf-8">
    /// <Resources>
    ///   <Conditions>
    ///     <WhenChangeValue Key="wchg" Active="Yes" Field="TOTAL" EntireRow="No" FirstSwapStyle="firstTotalStyle" SecondSwapStyle="secondTotalStyle"/>
    ///     ...
    ///   </Conditions>
    /// </Resources>
    /// ]]>
    /// </code>
    /// </example>
    [XmlAttribute]
    public string SecondSwapStyle
    {
        get => GetStaticBindingValue(_secondSwapStyle);
        set
        {
            SentinelHelper.ArgumentNull(value, nameof(value));
            SentinelHelper.IsFalse(
                ModelRegularExpressionHelper.IsValidIdentifier(value),
                new InvalidIdentifierNameException(ErrorMessageHelper.ModelIdentifierNameErrorMessage(GetType().Name,nameof(SecondSwapStyle), value)));

            _secondSwapStyle = value;
        }
    }

    #endregion

    #region public override methods

    /// <summary>
    /// Returns result of evaluates condition.
    /// </summary>
    /// <param name="row">Data row</param>
    /// <param name="col">Field column</param>
    /// <param name="target">Field data</param>
    /// <returns>
    /// A <see cref="ConditionResult"/> object that contains evaluate result.
    /// </returns>
    public override ConditionResult Evaluate(int row, int col, FieldValueInformation target)
    {
        var rows = Service.RawDataFiltered;
        var normalizedField = Field.ToUpperInvariant();

        string previousValue = null;
        if (row > 0)
        {
            var rowPreviousData = rows[row - 1];
            previousValue = rowPreviousData.Attribute(normalizedField)?.Value;
        }

        var rowData = rows[row];
        var currentValue = rowData.Attribute(normalizedField)?.Value;

        var fieldName = BaseDataField.GetFieldNameFrom(Service.CurrentField).ToUpperInvariant();

        if (previousValue == null)
        {
            if (normalizedField != fieldName)
            {
                return ConditionResult.Default; 
            }

            _lastStyle = FirstSwapStyle;
            return new ConditionResult {CanApply = true, Style = _lastStyle};
        }

        if (normalizedField != fieldName)
        {
            return ConditionResult.Default;
        }

        if (currentValue == previousValue)
        {
            return new ConditionResult { CanApply = true, Style = _lastStyle };
        }

        if (string.IsNullOrEmpty(SecondSwapStyle))
        {
            return new ConditionResult { CanApply = true, Style = _lastStyle };
        }

        _lastStyle = _lastStyle == FirstSwapStyle
            ? SecondSwapStyle
            : FirstSwapStyle;

        return new ConditionResult { CanApply = true, Style = _lastStyle };
    }

    #endregion

    #region public methods

    /// <summary>
    /// Clones this instance.
    /// </summary>
    /// <returns>
    /// A new object that is a copy of this instance.
    /// </returns>
    public WhenChangeCondition Clone() => (WhenChangeCondition)MemberwiseClone();

    #endregion

    #region private methods

    /// <summary>
    /// Creates a new object that is a copy of the current instance.
    /// </summary>
    /// <returns>
    /// A new object that is a copy of this instance.
    /// </returns>
    object ICloneable.Clone() => Clone();

    #endregion
}

//#region [private] (string) EntireRowApplyImpl(int, int): 
//private string EntireRowApplyImpl(int row, int col)
//{
//    var rows = Service.RawDataFiltered;
//    var normalizedField = Field.ToUpperInvariant();

//    string previousValue = null;
//    if (row > 0)
//    {
//        var rowPreviousData = rows[row - 1];
//        previousValue = rowPreviousData.Attribute(normalizedField)?.Value;
//    }

//    var rowData = rows[row];
//    var currentValue = rowData.Attribute(normalizedField)?.Value;
//    var fieldName = BaseDataFieldModel.GetFieldNameFrom(Service.CurrentField).ToUpperInvariant();

//    if (previousValue == null)
//    {
//        _lastStyle = FirstSwapStyle;
//        return _lastStyle;
//    }

//    int fieldCol = rowData.Attributes().IndexOfAttribute(normalizedField);
//    if (fieldCol == 0)
//    {
//        if (currentValue == previousValue)
//        {
//            return _lastStyle;
//        }

//        if (normalizedField == fieldName)
//        {
//            _lastStyle = _lastStyle == FirstSwapStyle
//                ? SecondSwapStyle
//                : FirstSwapStyle;
//        }

//        return _lastStyle;
//    }

//    if (currentValue == previousValue)
//    {
//        return _lastStyle;
//    }

//    var fieldsCount = Service.CurrentModel.Table.Fields.Count - 1;
//    if (col != fieldsCount)
//    {
//        return _lastStyle == FirstSwapStyle
//            ? SecondSwapStyle
//            : FirstSwapStyle;

//    }

//    _lastStyle = _lastStyle == FirstSwapStyle
//        ? SecondSwapStyle
//        : FirstSwapStyle;

//    return _lastStyle;
//}
//#endregion

//#region [private] (string) NonEntireRowApplyImpl(int, FieldValueInformation): 
//private string NonEntireRowApplyImpl(int row, FieldValueInformation target)
//{
//    var rows = Service.RawDataFiltered;
//    var normalizedField = Field.ToUpperInvariant();

//    string previousValue = null;
//    if (row > 0)
//    {
//        var rowPreviousData = rows[row - 1];
//        previousValue = rowPreviousData.Attribute(normalizedField)?.Value;
//    }

//    var rowData = rows[row];
//    var currentValue = rowData.Attribute(normalizedField)?.Value;

//    var fieldName = BaseDataFieldModel.GetFieldNameFrom(Service.CurrentField).ToUpperInvariant();

//    if (previousValue == null)
//    {
//        if (normalizedField != fieldName)
//        {
//            return row.IsOdd()
//                ? $"{target.Style.Name}_Alternate"
//                : target.Style.Name ?? StyleModel.NameOfDefaultStyle;
//        }

//        _lastStyle = FirstSwapStyle;
//        return _lastStyle;
//    }

//    if (normalizedField != fieldName)
//    {
//        return row.IsOdd()
//            ? $"{target.Style.Name}_Alternate"
//            : target.Style.Name ?? StyleModel.NameOfDefaultStyle;
//    }

//    if (currentValue == previousValue)
//    {
//        return _lastStyle;
//    }

//    if (string.IsNullOrEmpty(SecondSwapStyle))
//    {
//        return _lastStyle;
//    }

//    _lastStyle = _lastStyle == FirstSwapStyle
//        ? SecondSwapStyle
//        : FirstSwapStyle;

//    return _lastStyle;
//}
//#endregion
