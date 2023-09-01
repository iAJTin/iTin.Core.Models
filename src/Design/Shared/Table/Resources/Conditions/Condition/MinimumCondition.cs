﻿
using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Xml.Serialization;

using iTin.Core.Helpers;
using iTin.Core.Models.ComponentModel.Exceptions;
using iTin.Core.Models.Design.ComponentModel;
using iTin.Core.Models.Design.Enums;
using iTin.Core.Models.Helpers;

using ModelRegularExpressionHelper = iTin.Core.Models.Helpers.RegularExpressionHelper;

namespace iTin.Core.Models.Design.Table.Resource;

/// <summary>
/// Represents a field condition. Defines the style that will be applied to the field when its value is the minimum.
/// </summary>
public partial class MinimumCondition : ICloneable
{
    #region private memebrs

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string _style;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private object _minValue;

    #endregion

    #region constructor/s

    /// <summary>
    /// Initializes a new instance of the <see cref="MinimumCondition" /> class.
    /// </summary>
    public MinimumCondition()
    {
        _minValue = null;
    }

    #endregion

    #region public static readonly properties

    /// <summary>
    /// Gets an empty condition.
    /// </summary>
    /// <value>
    /// An empty condition.
    /// </value>
    public static MinimumCondition Empty => new();

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
    /// Gets or sets a value that represents the style that is applied when the condition is met.
    /// </summary>
    /// <value>
    /// A <see cref ="string"/> that represents the style that is applied when the condition is met.
    /// </value>
    /// <remarks>
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <?xml version="1.0" encoding="utf-8">
    /// <MinimumValue Style="string" .../>
    /// ]]>
    /// </code>
    /// </remarks>
    /// <example>
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <?xml version="1.0" encoding="utf-8">
    /// <Resources>
    ///   <Conditions>
    ///     <MinimumValue Key="max" Active="Yes" Field="TOTAL" EntireRow="No" Style="minTotalStyle"/>
    ///     ...
    ///   </Conditions>
    /// </Resources>
    /// ]]>
    /// </code>
    /// </example>
    [XmlAttribute]
    public string Style
    {
        get => GetStaticBindingValue(_style);
        set
        {
            SentinelHelper.ArgumentNull(value, nameof(value));
            SentinelHelper.IsFalse(
                ModelRegularExpressionHelper.IsValidIdentifier(value), 
                new InvalidIdentifierNameException(ErrorMessageHelper.ModelIdentifierNameErrorMessage(GetType().Name, nameof(Style), value)));

            _style = value;
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
        if (target.IsText)
        {
            return ConditionResult.Default;
        }

        if (_minValue == null)
        {
            var culture = Locale == KnownCulture.Current
                ? CultureInfo.CurrentUICulture
                : new CultureInfo(Locale.ToString());

            if (target.IsNumeric)
            {
                _minValue = CalculateNumericMinValue(culture);
            }

            if (target.IsDateTime)
            {
                _minValue = CalculateDateTimeMinValue(culture);
            }
        }

        var remarks = new RemarksCondition
        {
            Active = Active,
            Criterial = KnownOperator.EqualTo,
            EntireRow = EntireRow,
            Field = Field,
            Locale = Locale,
            Style = Style,
            Value = _minValue.ToString()
        };

        return remarks.Evaluate(row, col, target);
    }

    #endregion

    #region public methods

    /// <summary>
    /// Clones this instance.
    /// </summary>
    /// <returns>A new object that is a copy of this instance.</returns>
    public MinimumCondition Clone() => (MinimumCondition)MemberwiseClone();

    #endregion

    #region private methods

    private DateTime CalculateDateTimeMinValue(IFormatProvider culture)
    {
        var data = GetFieldAttributeEnumerable();
        var result = data.Select(value => DateTime.Parse(value, culture));

        return result.Min();
    }

    private decimal CalculateNumericMinValue(IFormatProvider culture)
    {
        var data = GetFieldAttributeEnumerable();
        var result = data.Select(value => decimal.Parse(value, culture));

        return result.Min();
    }

    /// <summary>
    /// Creates a new object that is a copy of the current instance.
    /// </summary>
    /// <returns>
    /// A new object that is a copy of this instance.
    /// </returns>
    object ICloneable.Clone() => Clone();

    #endregion
}
