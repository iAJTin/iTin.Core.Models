
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;

using Newtonsoft.Json;

using iTin.Core.ComponentModel.Patterns;
using iTin.Core.Helpers;

using iTin.Core.Models.ComponentModel.Exceptions;
using iTin.Core.Models.Design.Enums;
using iTin.Core.Models.Helpers;

using ModelsRegularExpressionHelper = iTin.Core.Models.Helpers.RegularExpressionHelper;

namespace iTin.Core.Models.Design;

public partial class BaseFilter : IFilter
{
    #region explicit

    /// <summary>
    /// Gets a value indicating whether this style is an empty border.
    /// </summary>
    /// <value>
    /// <b>true</b> if is an empty border; otherwise, <b>false</b>.
    /// </value>
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    bool IFilter.IsEmpty => IsDefault;

    /// <summary>
    /// Sets the element that owns this <see cref="IFilter"/>.
    /// </summary>
    /// <param name="reference">Reference to owner.</param>
    void IFilter.SetOwner(IFilters reference) => SetOwner(reference);

    #endregion

    #region public readonly properties

    /// <summary>
    /// Gets the element that owns this <see cref="IFilter"/>.
    /// </summary>
    /// <value>
    /// The <see cref="IFilters"/> that owns this <see cref="IFilter"/>.
    /// </value>
    [XmlIgnore]
    [JsonIgnore]
    public IFilters Owner { get; private set; }

    #endregion

    #region public properties

    /// <summary>
    /// Gets or sets value indicating whether this filter is active.
    /// </summary>
    /// <value>
    /// <b><see cref="YesNo.Yes"/></b> if is an active filter; otherwise, <b><see cref="YesNo.No"/></b>.
    /// </value>
    /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/></exception>
    [DefaultValue(DefaultActive)]
    public YesNo Active
    {
        get => GetStaticBindingValue(_active.ToString()).ToUpperInvariant() == "NO" ? YesNo.No : YesNo.Yes;
        set
        {
            SentinelHelper.IsEnumValid(value);

            _active = value;
        }
    }

    /// <summary>
    /// Gets or sets preferred filter operator.
    /// </summary>
    /// <value>
    /// Preferred filter operator.
    /// </value>
    /// <exception cref="InvalidEnumArgumentException">
    /// <paramref name="value"/> is not part of the enumeration.
    /// 
    /// -or-
    /// 
    /// <paramref name="value"/> is not an enumerated type.
    /// </exception>
    public KnownOperator Criterial
    {
        get => _criterial;
        set
        {
            SentinelHelper.IsEnumValid(value);

            _criterial = value;
        }
    }

    /// <summary>
    /// Gets or sets the target field acts as filter.
    /// </summary>
    /// <value>
    /// The target field acts as filter.
    /// </value>
    /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/></exception>
    /// <exception cref="InvalidEnumArgumentException"><paramref name="value"/> is not a valid field identifier</exception>
    public string Field
    {
        get => GetStaticBindingValue(_field);
        set
        {
            SentinelHelper.ArgumentNull(value, nameof(Field));
            SentinelHelper.IsFalse(
                ModelsRegularExpressionHelper.IsValidFieldName(value),
                new InvalidFieldIdentifierNameException(
                    ErrorMessageHelper.FieldIdentifierNameErrorMessage(
                        GetType().Name,
                        "Field",
                        value)));

            _field = value;
        }
    }

    /// <summary>
    /// Gets or sets the filter key into filters collection.
    /// </summary>
    /// <value>
    /// The filter key into filters collection.
    /// </value>
    /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/></exception>
    public string Key
    {
        get => GetStaticBindingValue(_key);
        set
        {
            SentinelHelper.ArgumentNull(value, nameof(Key));

            _key = value;
        }
    }

    /// <summary>
    /// Gets or sets the filter value.
    /// </summary>
    /// <value>
    /// The filter value.
    /// </value>
    /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/></exception>
    [DefaultValue(DefaultValue)]
    public string Value
    {
        get => GetStaticBindingValue(_value.ToString());
        set
        {
            SentinelHelper.ArgumentNull(value, nameof(Value));

            _value = value;
        }
    }

    #endregion

    #region public virtual methods

    /// <summary>
    /// Builds filter expression to execute.
    /// </summary>
    /// <returns>
    /// A expression to use for filter data
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public virtual ISpecification<XElement> BuildFilterExpression()
    {
        var normalizedField = Field.ToUpperInvariant();
        var normalizedValue = Value.ToUpperInvariant();

        switch (Criterial)
        {
            case KnownOperator.Beetween:
            {
                var values = normalizedValue.Split(' ').ToList();
                var totalValues = values.Count;
                if (totalValues != 2)
                {
                    throw new ArgumentOutOfRangeException();
                }

                var leftValue = decimal.Parse(values[0].Replace(".", ","));
                var left = new ExpressionSpecification<XElement>(element => decimal.Parse(element.Attribute(normalizedField).Value.ToUpperInvariant().Replace(".", ",")) >= leftValue);

                var rightValue = decimal.Parse(values[1].Replace(".", ","));
                var right = new ExpressionSpecification<XElement>(element => decimal.Parse(element.Attribute(normalizedField).Value.ToUpperInvariant().Replace(".", ",")) <= rightValue);

                return left.And(right);
            }

            case KnownOperator.EqualTo:
                return new ExpressionSpecification<XElement>(element => element.Attribute(normalizedField).Value.ToUpperInvariant().Equals(normalizedValue));

            case KnownOperator.GreatherThan:
            {
                var value = decimal.Parse(normalizedValue.Replace(".", ","));
                return new ExpressionSpecification<XElement>(element => decimal.Parse(element.Attribute(normalizedField).Value.ToUpperInvariant()) > value);
            }

            case KnownOperator.GreatherOrEqualsThan:
            {
                var value = decimal.Parse(normalizedValue.Replace(".", ","));
                return new ExpressionSpecification<XElement>(element => decimal.Parse(element.Attribute(normalizedField).Value.ToUpperInvariant()) >= value);
            }

            case KnownOperator.In:
            {
                var inValues = normalizedValue.Split(' ').ToList();
                return new ExpressionSpecification<XElement>(element => element.Attribute(normalizedField).Value.ToUpperInvariant().In(inValues));
            }

            case KnownOperator.LessThan:
            {
                var value = decimal.Parse(normalizedValue.Replace(".", ","));
                return new ExpressionSpecification<XElement>(element => decimal.Parse(element.Attribute(normalizedField).Value.ToUpperInvariant()) < value);
            }

            case KnownOperator.LessOrEqualThan:
            {
                var value = decimal.Parse(normalizedValue.Replace(".", ","));
                return new ExpressionSpecification<XElement>(element => decimal.Parse(element.Attribute(normalizedField).Value.ToUpperInvariant()) <= value);
            }

            case KnownOperator.Like:
                return new ExpressionSpecification<XElement>(element => element.Attribute(normalizedField).Value.ToUpperInvariant().Contains(normalizedValue));

            case KnownOperator.NotEqualTo:
                return new ExpressionSpecification<XElement>(element => element.Attribute(normalizedField).Value.ToUpperInvariant().Equals(normalizedValue));

            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    #endregion
}
