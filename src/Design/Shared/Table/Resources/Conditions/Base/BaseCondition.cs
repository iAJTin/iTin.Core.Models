
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Xml.Serialization;

using iTin.Core.Helpers;

using iTin.Core.Models.ComponentModel.Exceptions;
using iTin.Core.Models.Data;
using iTin.Core.Models.Design.ComponentModel;
using iTin.Core.Models.Design.Enums;
using iTin.Core.Models.Design.Table.Fields;
using iTin.Core.Models.Helpers;

using Newtonsoft.Json;

using ModelRegularExpressionHelper = iTin.Core.Models.Helpers.RegularExpressionHelper;

namespace iTin.Core.Models.Design.Table.Resource;

/// <summary>
/// Base class for the different types of field conditions.<br />
/// Which acts as the base class for different conditions.
/// </summary>
/// <remarks>
/// Belongs to: <strong><c>Resources</c></strong>. For more information, please see <see cref="Resources" />.
/// <para>The following table shows different conditions.</para>
/// <list type="table">
///   <listheader>
///     <term>Class</term>
///     <description>Description</description>
///   </listheader>
///   <item>
///     <term><see cref="MaximumCondition" /></term>
///     <description>Evaluates the maximum condition over a data field.</description>
///   </item>
///   <item>
///     <term><see cref="MinimumCondition" /></term>
///     <description>Evaluates the minimum condition over a data field.</description>
///   </item>
///   <item>
///     <term><see cref="RemarksCondition" /></term>
///     <description>Evaluates custom logic condition over a data field.</description>
///   </item>
///   <item>
///     <term><see cref="WhenChangeCondition" /></term>
///     <description>Evaluates condition over a data field.</description>
///   </item>
///   <item>
///     <term><see cref="ZeroCondition" /></term>
///     <description>Evaluates condition over a data field.</description>
///   </item>
/// </list>
/// </remarks>
public partial class BaseCondition : ICondition
{
    #region private constants

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const YesNo ActiveDefault = YesNo.Yes;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const YesNo EntireRowDefault = YesNo.No;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const KnownCulture LocaleDefault = KnownCulture.Current;

    #endregion

    #region private members

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string _key;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private YesNo _active;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string _field;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private YesNo _entrireRow;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private KnownCulture _locale;

    #endregion

    #region constructor/s

    #region [public] BaseCondition(): Initializes a new instance of the class
    /// <summary>
    /// Initializes a new instance of the <see cref="BaseCondition" /> class.
    /// </summary>
    protected BaseCondition()
    {
        _active = ActiveDefault;
        _locale = LocaleDefault;            
        _entrireRow = EntireRowDefault;
    }
    #endregion

    #endregion

    #region interfaces

    #region ICondition

    #region public abstrtact methods

    #region [public] {abstract} (ConditionResult) Evaluate(int, int, FieldValueInformation): Returns result of evaluates condition
    /// <summary>
    /// Returns result of evaluates condition.
    /// </summary>
    /// <param name="row">Data row</param>
    /// <param name="col">Field column</param>
    /// <param name="target">Field data</param>
    /// <returns>
    /// A <see cref="ConditionResult" /> object that contains evaluate result.
    /// </returns>
    public abstract ConditionResult Evaluate(int row, int col, FieldValueInformation target);
    #endregion

    #endregion

    #region public methods

    #region [public] (ConditionResult) Evaluate(int, int): Returns result of evaluates condition
    /// <summary>
    /// Returns result of evaluates condition.
    /// </summary>
    /// <param name="row">Data row</param>
    /// <param name="col">Field column</param>
    /// <returns>
    /// A <see cref="ConditionResult" /> object that contains evaluate result.
    /// </returns>
    public ConditionResult Evaluate(int row, int col)
    {
        var normalizedField = Field.ToUpperInvariant();
        var normalizedFieldName = BaseDataField.GetFieldNameFrom(Service.CurrentField).ToUpperInvariant();

        return normalizedField != normalizedFieldName
            ? ConditionResult.Default
            : Evaluate(row, col, Service.CurrentField.Value.GetValue());
    }
    #endregion

    #endregion

    #endregion

    #endregion

    #region protected properties

    #region [protected] (ModelService) Service: Gets a reference to an object that contains information about the context
    /// <summary>
    /// Gets a reference to an object that contains information about the context.
    /// </summary>
    /// <value>
    /// A <see cref="ModelService"/> that contains information about the context.
    /// </value>
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    protected ModelService Service => ModelService.Instance;
    #endregion

    #endregion

    #region public readonly properties

    #region [public] (ConditionsCollection) Owner: Gets the element that owns this instance
    /// <summary>
    /// Gets the element that owns this <see cref="BaseCondition"/>.
    /// </summary>
    /// <value>
    /// The <see cref="ConditionsCollection" /> that owns this <see cref="BaseCondition"/>.
    /// </value>
    [XmlIgnore]
    [Browsable(false)]
    public ConditionsCollection Owner { get; private set; }
    #endregion

    #endregion

    #region public properties

    #region [public] (YesNo) Active: Gets or sets a value that indicates if this condition is active
    /// <summary>
    /// Gets or sets a value that indicates if this condition is active.<br/>
    /// The default is <see cref="YesNo.Yes"/>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// <see cref="YesNo.Yes"/> if is active; otherwise, <see cref="YesNo.No"/>.
    /// </para>
    /// <strong><u>Usage</u></strong>:
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <MaximumCondition|MinimumCondition|RemarksCondition|WhenChangeCondition|ZeroCondition Active="Yes|No" ...>
    ///   ...
    /// <MaximumCondition|MinimumCondition|RemarksCondition|WhenChangeCondition|ZeroCondition>
    /// ]]>
    /// </code>
    /// <example>
    /// <para><strong><u>Examples</u></strong>:</para>
    /// <para><c>XML#</c></para>
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <Resources>
    ///   <Conditions>
    ///     <MaximumCondition Key="max" Active="Yes" Field="TOTAL" EntireRow="No" Style="maxTotalStyle"/>
    ///     ...
    ///   </Conditions>
    /// </Resources>
    /// ]]>
    /// </code>
    /// </example>
    /// </remarks>
    /// <exception cref="InvalidEnumArgumentException">
    /// <paramref name="value"/> is not part of the enumeration.<br/>
    /// 
    /// -or-<br/>
    /// 
    /// <paramref name="value"/> is not an enumerated type.
    /// </exception>
    [JsonProperty]
    [XmlAttribute]
    [DefaultValue(ActiveDefault)]
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

    #region [public] (YesNo) EntireRow: Gets or sets a value that indicates if condition style applies over the row
    /// <summary>
    /// Gets or sets a value that indicates if condition style applies over the row.<br/>
    /// The default is <see cref="YesNo.No"/>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// <see cref="YesNo.Yes"/> if condition style applies over row; otherwise, <see cref="YesNo.No"/> if style condition only applies over field cell.
    /// </para>
    /// <strong><u>Usage</u></strong>:
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <MaximumCondition|MinimumCondition|RemarksCondition|WhenChangeCondition|ZeroCondition EntireRow="Yes|No" ...>
    ///   ...
    /// <MaximumCondition|MinimumCondition|RemarksCondition|WhenChangeCondition|ZeroCondition>
    /// ]]>
    /// </code>
    /// <example>
    /// <para><strong><u>Examples</u></strong>:</para>
    /// <para><c>C#</c></para>
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <Resources>
    ///   <Conditions>
    ///     <MaximumCondition Key="max" Active="Yes" Field="TOTAL" EntireRow="No" Style="maxTotalStyle"/>
    ///     ...
    ///   </Conditions>
    /// </Resources>
    /// ]]>
    /// </code>
    /// </example>
    /// </remarks>
    /// <exception cref="InvalidEnumArgumentException">
    /// <paramref name="value"/> is not part of the enumeration.<br/>
    /// 
    /// -or-<br/>
    /// 
    /// <paramref name="value"/> is not an enumerated type.
    /// </exception>
    [JsonProperty]
    [XmlAttribute]
    [DefaultValue(EntireRowDefault)]
    public YesNo EntireRow
    {
        get => GetStaticBindingValue(_entrireRow.ToString()).ToUpperInvariant() == "NO" ? YesNo.No : YesNo.Yes;
        set
        {
            SentinelHelper.IsEnumValid(value);

            _entrireRow = value;
        }
    }
    #endregion

    #region [public] (string) Field: Gets or sets a value that represents the field on which the condition will be evaluated
    /// <summary>
    /// Gets or sets a value that represents the field on which the condition will be evaluated
    /// </summary>
    /// <remarks>
    /// <para>
    /// A <see cref ="string"/> that represents the field on which the condition will be evaluated.
    /// </para>
    /// <strong><u>Usage</u></strong>:
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <MaximumCondition|MinimumCondition|RemarksCondition|WhenChangeCondition|ZeroCondition Field="string" ...>
    ///   ...
    /// <MaximumCondition|MinimumCondition|RemarksCondition|WhenChangeCondition|ZeroCondition>
    /// ]]>
    /// </code>
    /// <example>
    /// <para><strong><u>Examples</u></strong>:</para>
    /// <para><c>XML</c></para>
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <Resources>
    ///   <Conditions>
    ///     <MaximumCondition Key="max" Active="Yes" Field="TOTAL" EntireRow="No" Style="maxTotalStyle"/>
    ///     ...
    ///   </Conditions>
    /// </Resources>
    /// ]]>
    /// </code>
    /// </example>
    /// </remarks>
    /// <exception cref="ArgumentNullException">If <paramref name="value" /> is <see langword="null"/>.</exception>
    /// <exception cref="InvalidFieldIdentifierNameException">If <paramref name="value" /> not is a valid field identifier name.</exception>
    [JsonProperty]
    [XmlAttribute]
    public string Field
    {
        get => GetStaticBindingValue(_field);
        set
        {
            SentinelHelper.ArgumentNull(value, nameof(value));
            SentinelHelper.IsFalse(
                ModelRegularExpressionHelper.IsValidFieldName(value), 
                new InvalidFieldIdentifierNameException(ErrorMessageHelper.FieldIdentifierNameErrorMessage(GetType().Name, nameof(Field), value)));

            _field = value;
        }
    }
    #endregion

    #region [public] (string) Key: Gets or sets a value that contains an identifier for this condition
    /// <summary>
    /// Gets or sets a value that contains an identifier for this condition.
    /// </summary>
    /// <remarks>
    /// <para>
    /// A <see cref ="string"/> that represents the identifier for this condition.
    /// </para>
    /// <strong><u>Usage</u></strong>:
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <MaximumCondition|MinimumCondition|RemarksCondition|WhenChangeCondition|ZeroCondition Key="string" ...>
    ///   ...
    /// <MaximumCondition|MinimumCondition|RemarksCondition|WhenChangeCondition|ZeroCondition>
    /// ]]>
    /// </code>
    /// <example>
    /// <para><strong><u>Examples</u></strong>:</para>
    /// <para><c>XML</c></para>
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <Resources>
    ///   <Conditions>
    ///     <MaximumCondition Key="max" Active="Yes" Field="TOTAL" EntireRow="No" Style="maxTotalStyle"/>
    ///     ...
    ///   </Conditions>
    /// </Resources>
    /// ]]>
    /// </code>
    /// </example>
    /// </remarks>
    /// <exception cref="ArgumentNullException">If <paramref name="value" /> is <see langword="null"/>.</exception>
    /// <exception cref="InvalidFieldIdentifierNameException">If <paramref name="value" /> not is a valid field identifier name.</exception>
    [JsonProperty]
    [XmlAttribute]
    public string Key
    {
        get => GetStaticBindingValue(_key);
        set
        {
            SentinelHelper.ArgumentNull(value, nameof(value));
            SentinelHelper.IsFalse(
                ModelRegularExpressionHelper.IsValidIdentifier(value), 
                new InvalidIdentifierNameException(ErrorMessageHelper.ModelIdentifierNameErrorMessage(GetType().Name, nameof(Key), value)));

            _key = value;
        }
    }
    #endregion

    #region [public] (KnownCulture) Locale: Gets or sets the data field culture
    /// <summary>
    /// Gets or sets the data field culture.<br/>
    /// The default is <see cref="KnownCulture.Current" />.
    /// </summary>
    /// <remarks>
    /// <para>
    /// One of the <see cref="KnownCulture" /> values. 
    /// </para>
    /// <strong><u>Usage</u></strong>:
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <MaximumCondition|MinimumCondition|RemarksCondition|WhenChangeCondition|ZeroCondition Locale="Current|any culture" ...>
    ///   ...
    /// <MaximumCondition|MinimumCondition|RemarksCondition|WhenChangeCondition|ZeroCondition>
    /// ]]>
    /// </code>
    /// <example>
    /// <para><strong><u>Examples</u></strong>:</para>
    /// <para><c>XML</c></para>
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <Resources>
    ///   <Conditions>
    ///     <MaximumCondition Key="max" Active="Yes" Field="TOTAL" EntireRow="No" Style="maxTotalStyle" Locale="en-EN"/>
    ///     ...
    ///   </Conditions>
    /// </Resources>
    /// ]]>
    /// </code>
    /// </example>
    /// </remarks>
    [JsonProperty]
    [XmlAttribute]
    [DefaultValue(LocaleDefault)]
    public KnownCulture Locale
    {
        get => _locale;
        set
        {
            var isValidLocale = true;
            if (!value.Equals(KnownCulture.Current))
            {
                var isValidCulture = IsValidCulture(value);
                if (!isValidCulture)
                {
                    isValidLocale = false;
                }
            }

            _locale = isValidLocale
                ? value
                : LocaleDefault;
        }
    }
    #endregion

    #endregion

    #region public methods

    #region [public] (ConditionResult) Evaluate(): Returns result of evaluates condition
    /// <summary>
    /// Returns result of evaluates condition.
    /// </summary>
    /// <returns>
    /// A <see cref="ConditionResult"/> object that contains evaluate result.
    /// </returns>
    public ConditionResult Evaluate()
    {
        var service = ModelService.Instance;

        return Evaluate(
            service.CurrentRow,
            service.CurrentCol);
    }
    #endregion

    #region [public] (void) SetOwner(ConditionsCollection): Sets the element that owns this instance
    /// <summary>
    /// Sets the element that owns this <see cref="BaseCondition"/>.
    /// </summary>
    /// <param name="reference">Reference to owner.</param>
    public void SetOwner(ConditionsCollection reference)
    {
        Owner = reference;
    }
    #endregion

    #endregion

    #region protected methods

    #region [protected] (IEnumerable<string>) GetFieldAttributeEnumerable: Returns a list of field condition with raw content
    /// <summary>
    /// Returns a list of field condition with raw content.
    /// </summary>
    /// <returns>
    /// A list of field condition with raw content.
    /// </returns>
    protected IEnumerable<string> GetFieldAttributeEnumerable()
    {
        var validRawData = new Collection<string>();
        foreach (var rawData in Service.RawDataFiltered)
        {
            var fieldAttr = rawData.Attribute(Field);
            if (fieldAttr == null)
            {
                continue;
            }

            if (string.IsNullOrEmpty(fieldAttr.Value))
            {
                continue;
            }

            validRawData.Add(fieldAttr.Value);
        }

        return validRawData;
    }
    #endregion

    #endregion

    #region private static methods

    #region [private] {static} (bool) IsValidCulture(KnownCulture): Gets a value indicating whether the font is installed on this system
    /// <summary>
    /// Gets a value indicating whether the font is installed on this system.
    /// </summary>
    /// <param name="culture">Culture to check.</param>
    /// <returns>
    /// <strong>true</strong> if the specified culture is installed on the system; otherwise, <strong>false</strong>.
    /// </returns>
    private static bool IsValidCulture(KnownCulture culture)
    {
        var iw32C = CultureInfo.GetCultures(CultureTypes.InstalledWin32Cultures);
        return iw32C.Any(clt => clt.Name == culture.GetDescription());
    }
    #endregion 

    #endregion 
}
