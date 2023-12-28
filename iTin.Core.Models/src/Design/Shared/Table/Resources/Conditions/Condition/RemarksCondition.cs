
using System;
using System.Diagnostics;
using System.Globalization;
using System.Xml.Serialization;

using iTin.Core.Helpers;
using iTin.Core.Models.ComponentModel.Exceptions;
using iTin.Core.Models.Design.ComponentModel;
using iTin.Core.Models.Design.Enums;
using iTin.Core.Models.Helpers;

using ModelRegularExpressionHelper = iTin.Core.Models.Helpers.RegularExpressionHelper;

namespace iTin.Core.Models.Design.Table.Resource;

/// <summary>
/// Represents a field condition. Defines the style that will be applied to the field when it met specified condition.
/// </summary>
public partial class RemarksCondition : ICloneable
{
    #region private memebrs

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string _style;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string _value;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private KnownOperator _operator;

    #endregion

    #region public static readonly properties

    /// <summary>
    /// Gets an empty condition.
    /// </summary>
    /// <value>
    /// An empty condition.
    /// </value>
    public static RemarksCondition Empty => new();

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
    /// Gets or sets a value that represents the criteria to apply to the field of this condition. 
    /// </summary>
    /// <value>
    /// One of the enumeration values <see cref ="KnownOperator"/>.
    /// </value>
    /// <remarks>
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <?xml version="1.0" encoding="utf-8">
    /// <RemarksValue Criterial="EqualTo|NotEqualTo|LessThan|LessOrEqualThan|GreatherThan|GreatherOrEqualsThan|In|Like|Beetween" .../>
    /// ]]>
    /// </code>
    /// </remarks>
    /// <example>
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <?xml version="1.0" encoding="utf-8">
    /// <Resources>
    ///   <Conditions>
    ///     <RemarksValue Key="eq" Active="Yes" Field="TOTAL" Criterial="EqualTo" Value="10" EntireRow="No" Style="eqTotalStyle"/>
    ///     ...
    ///   </Conditions>
    /// </Resources>
    /// ]]>
    /// </code>
    /// </example>
    [XmlAttribute]
    public KnownOperator Criterial
    {
        get => (KnownOperator)Enum.Parse(typeof(KnownOperator), GetStaticBindingValue(_operator.ToString()));
        set
        {
            SentinelHelper.IsEnumValid(value);

            _operator = value;
        }
    }

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
    /// <RemarksValue Style="string" .../>
    /// ]]>
    /// </code>
    /// </remarks>
    /// <example>
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <?xml version="1.0" encoding="utf-8">
    /// <Resources>
    ///   <Conditions>
    ///     <RemarksValue Key="eq" Active="Yes" Field="TOTAL" Criterial="EqualTo" Value="10" EntireRow="No" Style="eqTotalStyle"/>
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

    /// <summary>
    /// Defines the value associated with the specified condition that the condition must meet.
    /// </summary>
    /// <value>
    /// A <see cref ="string"/> that contains criterial value.
    /// </value>
    /// <remarks>
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <?xml version="1.0" encoding="utf-8">
    /// <RemarksValue Value="string" .../>
    /// ]]>
    /// </code>
    /// </remarks>
    /// <example>
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <?xml version="1.0" encoding="utf-8">
    /// <Resources>
    ///   <Conditions>
    ///     <RemarksValue Key="eq" Active="Yes" Field="TOTAL" Criterial="EqualTo" Value="10" EntireRow="No" Style="eqTotalStyle"/>
    ///     ...
    ///   </Conditions>
    /// </Resources>
    /// ]]>
    /// </code>
    /// </example>
    [XmlAttribute]
    public string Value
    {
        get => GetStaticBindingValue(_value);
        set
        {
            SentinelHelper.ArgumentNull(value, nameof(value));

            _value = value;
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
    public override ConditionResult Evaluate(int row, int col, FieldValueInformation target) => new() {CanApply = EvaluateCriterial(Criterial, target, Value, Locale), Style = Style};

    #endregion

    #region public methods

    /// <summary>
    /// Clones this instance.
    /// </summary>
    /// <returns>A new object that is a copy of this instance.</returns>
    public RemarksCondition Clone() => (RemarksCondition)MemberwiseClone();

    #endregion

    #region private static methods

    /// <summary>
    /// Evaluates criterial
    /// </summary>
    /// <param name="op">Operator</param>
    /// <param name="context">Field data context</param>
    /// <param name="testValue">Value to evaluate</param>
    /// <param name="locale">Culture to use with field data value</param>
    /// <returns>
    /// <c>true</c> if it meets the criteria; otherwise <c>false</c>.
    /// </returns>
    private static bool EvaluateCriterial(KnownOperator op, FieldValueInformation context, string testValue, KnownCulture locale)
    {
        var result = false;

        var culture = locale == KnownCulture.Current
            ? CultureInfo.CurrentUICulture
            : new CultureInfo(locale.ToString());

        switch (op)
        {
            #region Criterial: Beetween
            //case KnownOperator.Beetween:
            //    {
            //        var values = testValue.Split(' ').ToList();
            //        var totalValues = values.Count;
            //        if (totalValues != 2)
            //        {
            //            throw new ArgumentOutOfRangeException();
            //        }

            //        var dataValueAsSpanishFormat = dataValue.Replace(".", ",");
            //        var okValueDecimal = decimal.TryParse(dataValueAsSpanishFormat, out decimal dataValueAsDecimal);

            //        var leftValueAsSpanishFormat = values[0].Replace(".", ",");
            //        var okLeftValueDecimal = decimal.TryParse(leftValueAsSpanishFormat, out decimal leftValueAsDecimal);

            //        var rightValueAsSpanishFormat = values[1].Replace(".", ",");
            //        var okRightValueDecimal = decimal.TryParse(rightValueAsSpanishFormat, out decimal rightValueAsDecimal);

            //        var canContinue = okValueDecimal && okLeftValueDecimal && okRightValueDecimal;
            //        if (!canContinue)
            //        {
            //            break;
            //        }

            //        if (dataValueAsDecimal >= leftValueAsDecimal && dataValueAsDecimal >= rightValueAsDecimal)
            //        {
            //            result = true;
            //        }
            //        break;
            //    }
            #endregion

            #region Criterial: EqualTo
            case KnownOperator.EqualTo:

                if (context.IsText)
                {
                    var left = context.Value.ToString();
                    var right = testValue;
                    return left.Equals(right);
                }

                if (context.IsNumeric)
                {
                    var left = decimal.Parse(context.Value.ToString(), culture);
                    var right = decimal.Parse(testValue, culture);
                    return left.Equals(right);
                }

                if (context.IsDateTime)
                {
                    var left = DateTime.Parse(context.Value.ToString(), culture);
                    var right = DateTime.Parse(testValue, culture);

                    return left.Equals(right);
                }
                break;
            #endregion

            #region Criterial: GreatherOrEqualsThan
            //case KnownOperator.GreatherOrEqualsThan:
            //    {
            //        var dataValueAsSpanishFormat = dataValue.Replace(".", ",");
            //        var okValueDecimal = decimal.TryParse(dataValueAsSpanishFormat, out decimal dataValueAsDecimal);

            //        var testValueAsSpanishFormat = testValue.Replace(".", ",");
            //        var okTestValueDecimal = decimal.TryParse(testValueAsSpanishFormat, out decimal testValueAsDecimal);

            //        var canContinue = okValueDecimal && okTestValueDecimal;
            //        if (!canContinue)
            //        {
            //            break;
            //        }

            //        if (dataValueAsDecimal >= testValueAsDecimal)
            //        {
            //            result = true;
            //        }
            //        break;
            //    }
            #endregion

            #region Criterial: GreatherThan
            //case KnownOperator.GreatherThan:
            //    {
            //        var dataValueAsSpanishFormat = dataValue.Replace(".", ",");
            //        var okValueDecimal = decimal.TryParse(dataValueAsSpanishFormat, out decimal dataValueAsDecimal);

            //        var testValueAsSpanishFormat = testValue.Replace(".", ",");
            //        var okTestValueDecimal = decimal.TryParse(testValueAsSpanishFormat, out decimal testValueAsDecimal);

            //        var canContinue = okValueDecimal && okTestValueDecimal;
            //        if (!canContinue)
            //        {
            //            break;
            //        }

            //        if (dataValueAsDecimal > testValueAsDecimal)
            //        {
            //            result = true;
            //        }
            //        break;
            //    }
            #endregion

            #region Criterial: In
            //case KnownOperator.In:
            //    {
            //        var inValues = testValue.Split(' ').ToList();
            //        if (dataValue.In(inValues))
            //        {
            //            result = true;
            //        }
            //        break;
            //    }
            #endregion

            #region Criterial: LessOrEqualThan
            //case KnownOperator.LessOrEqualThan:
            //    {
            //        var dataValueAsSpanishFormat = dataValue.Replace(".", ",");
            //        var okValueDecimal = decimal.TryParse(dataValueAsSpanishFormat, out decimal dataValueAsDecimal);

            //        var testValueAsSpanishFormat = testValue.Replace(".", ",");
            //        var okTestValueDecimal = decimal.TryParse(testValueAsSpanishFormat, out decimal testValueAsDecimal);

            //        var canContinue = okValueDecimal && okTestValueDecimal;
            //        if (!canContinue)
            //        {
            //            break;
            //        }

            //        if (dataValueAsDecimal <= testValueAsDecimal)
            //        {
            //            result = true;
            //        }
            //        break;
            //    }
            #endregion

            #region Criterial: LessThan
            //case KnownOperator.LessThan:
            //    {
            //        var dataValueAsSpanishFormat = dataValue.Replace(".", ",");
            //        var okValueDecimal = decimal.TryParse(dataValueAsSpanishFormat, out decimal dataValueAsDecimal);

            //        var testValueAsSpanishFormat = testValue.Replace(".", ",");
            //        var okTestValueDecimal = decimal.TryParse(testValueAsSpanishFormat, out decimal testValueAsDecimal);

            //        var canContinue = okValueDecimal && okTestValueDecimal;
            //        if (!canContinue)
            //        {
            //            break;
            //        }

            //        if (dataValueAsDecimal < testValueAsDecimal)
            //        {
            //            result = true;
            //        }
            //        break;
            //    }
            #endregion

            #region Criterial: Like
            //case KnownOperator.Like:
            //    if (dataValue.Contains(testValue))
            //    {
            //        result = true;
            //    }
            //    break;
            #endregion

            #region Criterial: NotEqualTo
            case KnownOperator.NotEqualTo:
                if (context.IsText)
                {
                    var left = context.Value.ToString();
                    var right = testValue;
                    return !left.Equals(right);
                }

                if (context.IsNumeric)
                {
                    var left = decimal.Parse(context.Value.ToString(), culture);
                    var right = decimal.Parse(testValue, culture);
                    return !left.Equals(right);
                }

                if (context.IsDateTime)
                {
                    var left = DateTime.Parse(context.Value.ToString(), culture);
                    var right = DateTime.Parse(testValue, culture);

                    return !left.Equals(right);
                }
                break;
            #endregion
        }

        return result;
    }

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
