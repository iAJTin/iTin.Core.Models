
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

namespace iTin.Core.Models.Design.Table.Resource.Filters;

/// <summary>
/// Defines a data filter specification.
/// </summary>
public partial class Filter
{
    #region private constants

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const YesNo DefaultActive = YesNo.Yes;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const string DefaultValue = "";

    #endregion

    #region private memebrs

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private YesNo _active;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private KnownOperator _criterial;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string _field;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string _key;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private object _value;

    #endregion

    #region constructor/s

    #region [public] Filter(): Initializes a new instance of the class
    /// <summary>
    /// Initializes a new instance of the <see cref="Filter"/> class.
    /// </summary>
    public Filter()
    {
        Value = DefaultValue;
        Active = DefaultActive;
    }
    #endregion

    #endregion

    #region public static readonly properties

    /// <summary>
    /// Gets an empty filter.
    /// </summary>
    /// <value>
    /// An empty condition.
    /// </value>
    public static Filter Empty => new();

    #endregion

    #region public readonly properties

    #region [public] (FiltersCollection) Owner: Gets the element that owns this instance
    /// <summary>
    /// Gets the element that owns this <see cref="Filter"/>.
    /// </summary>
    /// <value>
    /// The <see cref="FiltersCollection"/> that owns this <see cref="Filter"/>.
    /// </value>
    [JsonIgnore]
    [XmlIgnore]
    [Browsable(false)]
    public FiltersCollection Owner { get; private set; }
    #endregion

    #endregion

    #region public properties

    #region [public] (YesNo) Active: Gets or sets value indicating whether this filter is active
    /// <summary>
    /// Gets or sets value indicating whether this filter is active.
    /// </summary>
    /// <value>
    /// <b><see cref="YesNo.Yes"/></b> if is an active filter; otherwise, <b><see cref="YesNo.No"/></b>.
    /// </value>
    /// <exception cref="InvalidEnumArgumentException">
    /// <paramref name="value"/> is not part of the enumeration.<br/>
    /// 
    /// -or-<br/>
    /// 
    /// <paramref name="value"/> is not an enumerated type.
    /// </exception>
    [XmlAttribute]
    [JsonProperty]
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
    #endregion

    #region [public] (KnownOperator) Criterial: Gets or sets preferred filter operator
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
    [XmlAttribute]
    [JsonProperty]
    public KnownOperator Criterial
    {
        get => _criterial;
        set
        {
            SentinelHelper.IsEnumValid(value);

            _criterial = value;
        }
    }
    #endregion

    #region [public] (string) Field: Gets or sets the target field acts as filter
    /// <summary>
    /// Gets or sets the target field acts as filter.
    /// </summary>
    /// <value>
    /// The target field acts as filter.
    /// </value>
    /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/></exception>
    /// <exception cref="InvalidEnumArgumentException"><paramref name="value"/> is not a valid field identifier</exception>
    [XmlAttribute]
    [JsonProperty]
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
                        nameof(Field),
                        value)));

            _field = value;
        }
    }
    #endregion

    #region [public] (string) Key: Gets or sets the filter key into filters collection
    /// <summary>
    /// Gets or sets the filter key into filters collection.
    /// </summary>
    /// <value>
    /// The filter key into filters collection.
    /// </value>
    /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/></exception>
    [XmlAttribute]
    [JsonProperty]
    public string Key
    {
        get => GetStaticBindingValue(_key);
        set
        {
            SentinelHelper.ArgumentNull(value, nameof(Key));

            _key = value;
        }
    }
    #endregion

    #region [public] (string) Value: Gets or sets the filter value
    /// <summary>
    /// Gets or sets the filter value.
    /// </summary>
    /// <value>
    /// The filter value.
    /// </value>
    /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/></exception>
    [XmlAttribute]
    [JsonProperty]
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

    #endregion

    #region public virtual methods

    #region [public] {virtual} (ISpecification<XElement>) BuildFilterExpression(): Builds filter expression to execute
    /// <summary>
    /// Builds filter expression to execute.
    /// </summary>
    /// <returns>
    /// A expression to use for filter data
    /// </returns>
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

    #endregion
}
