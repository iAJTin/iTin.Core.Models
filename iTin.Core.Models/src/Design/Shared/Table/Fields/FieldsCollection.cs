
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;

using iTin.Core.Helpers;
using iTin.Core.Models.Design.Enums;
using iTin.Core.Models.Design.Table.Fields;

namespace iTin.Core.Models.Design.Table;

/// <summary>
/// Collection of data fields. Each element defines a data field.
/// </summary>
/// <remarks>
/// <para>Belongs to: <strong><c>Table</c></strong>. For more information, please see <see cref="TableDefinition"/></para>
/// <para><strong><u>Usage</u></strong>:</para>
/// <code lang="xml" title="ITEE Object Element Usage">
/// <![CDATA[
/// <Table ...>
///   <Fields>
///     <Field/>|<Fixed/>|<Gap/>|<Group/>|<Packet/>
///   </Fields>
/// </Table>
/// ]]>
/// </code>
/// <para><strong><u>Elements</u></strong></para>
/// <list type="table">
///   <listheader>
///     <term>Element</term>
///     <description>Description</description>
///   </listheader>
///   <item>
///     <term>Field</term>
///     <description>Represents a data field. For more information, please see <see cref="DataField"/></description>
///   </item>
///   <item>
///     <term>Fixed</term>
///     <description>Represents a piece of a field fixed-width data. For more information, please see <see cref="FixedField"/></description>
///   </item>
///   <item>
///     <term>Gap</term>
///     <description>Represents an empty data field. For more information, please see <see cref="GapField"/></description>
///   </item>
///   <item>
///     <term>Packet</term>
///     <description>Represents a packet data field. For more information, please see <see cref="PacketField"/></description>
///   </item>
/// </list>
/// </remarks>
public partial class FieldsCollection
{
    #region constructor/s

    #region [public] FieldsCollection(TableDefinition): Initializes a new instance of the class with a parent instance
    /// <summary>
    /// Initializes a new instance of the <see cref="FieldsCollection"/> class with a parent instance.
    /// </summary>
    /// <param name="parent">The parent <see cref="TableDefinition"/> instance.</param>
    public FieldsCollection(TableDefinition parent) : base(parent)
    {
    }
    #endregion

    #endregion

    #region public methods

    #region [public] (IEnumerable<BaseDataField>) GetRange(YesNo): Gets an enumerator to a list of fields that has visible headers
    /// <summary>
    /// Gets an enumerator to a list of fields that has visible headers.
    /// </summary>
    /// <param name="visibleHeaders">Table position</param>
    /// <returns>
    /// Enumerator that contains list of fields that has visible headers.
    /// </returns>
    /// <exception cref="InvalidEnumArgumentException">
    /// <paramref name="visibleHeaders"/> is not part of the enumeration.<br/>
    /// 
    /// -or-<br/>
    /// 
    /// <paramref name="visibleHeaders"/> is not an enumerated type.
    /// </exception>
    public IEnumerable<BaseDataField> GetRange(YesNo visibleHeaders)
    {
        SentinelHelper.IsEnumValid(visibleHeaders);

        return this.Where(field => field.Header.Show == YesNo.Yes).ToList();
    }
    #endregion

    #region [public] (IEnumerable<BaseDataField>) GetRange(KnownAggregateLocation): Gets an enumerator to a list of fields that meet the test of being added at the indicated position and this is visible
    /// <summary>
    /// Gets an enumerator to a list of fields that meet the test of being added at the indicated position and this is visible.
    /// </summary>
    /// <param name="location">Table position</param>
    /// <returns>
    /// Enumerator that contains list of fields that meet the condition and is visible.
    /// </returns>
    /// <exception cref="InvalidEnumArgumentException">
    /// <paramref name="location"/> is not part of the enumeration.<br/>
    /// 
    /// -or-<br/>
    /// 
    /// <paramref name="location"/> is not an enumerated type.
    /// </exception>
    public IEnumerable<BaseDataField> GetRange(KnownAggregateLocation location)
    {
        SentinelHelper.IsEnumValid(location);

        return this.Where(field => field.Aggregate.Show == YesNo.Yes && field.Aggregate.Location == location);
    }
    #endregion

    #region [public] (IEnumerable<BaseDataField>) GetRange(KnownFieldType): Returns an enumerator to a field list containing only those who meet the condition of type
    /// <summary>
    /// Returns an enumerator to a field list containing only those who meet the condition of type.
    /// </summary>
    /// <param name="field">Type of the field.</param>
    /// <returns>
    /// Enumerator that contains list of fields that meet the condition.
    /// </returns>
    /// <exception cref="InvalidEnumArgumentException">
    /// <paramref name="field"/> is not part of the enumeration.<br/>
    /// 
    /// -or-<br/>
    /// 
    /// <paramref name="field"/> is not an enumerated type.
    /// </exception>
    public IEnumerable<BaseDataField> GetRange(KnownFieldType field)
    {
        SentinelHelper.IsEnumValid(field);

        return this.Where(fld => fld.FieldType.Equals(field));
    }
    #endregion

    #region [public] (bool) HasVisibleAggregatesByLocation(KnownAggregateLocation): Gets a value indicating whether there is a field with a visible aggregate and at specified position
    /// <summary>
    /// Gets a value indicating whether there is a field with a visible aggregate and at specified position.
    /// </summary>
    /// <param name="location">Aggregate location</param>
    /// <returns>
    /// <see langword="true"/> if there is a field with a visible aggregate and at the specified location; otherwise, <see langword="false"/>.
    /// </returns>
    /// <exception cref="InvalidEnumArgumentException">
    /// <paramref name="location"/> is not part of the enumeration.<br/>
    /// 
    /// -or-<br/>
    /// 
    /// <paramref name="location"/> is not an enumerated type.
    /// </exception>
    public bool HasVisibleAggregatesByLocation(KnownAggregateLocation location)
    {
        SentinelHelper.IsEnumValid(location);

        return GetRange(location).Any();
    }
    #endregion

    #region [public] (bool) HasVisibleHeaders(): Gets a value indicating whether there are field with a visible header
    /// <summary>
    /// Gets a value indicating whether there are field with a visible header.
    /// </summary>
    /// <returns>
    /// <see langword="true"/> if there are field with a visible header; otherwise, <see langword="false"/>.
    /// </returns>
    public bool HasVisibleHeaders() => GetRange(YesNo.Yes).Any();
    #endregion

    #endregion
}

///// <summary>
///// Validates the field definition list and the definition of field styles.
///// </summary>
///// <exception cref="InvalidFieldsDefinitionException">There are field definition errors.</exception>
///// <exception cref="InvalidStylesDefinitionException">There are styles definition errors.</exception>
//public void Validate()
//{
//    var hasFieldErrors = HasFieldErrors(this, out var fieldErrorDictionary);
//    if (hasFieldErrors)
//    {
//        var message = ErrorMessageHelper.FormatFieldErrorMessage(fieldErrorDictionary);
//        throw new InvalidFieldsDefinitionException(message);
//    }

//    var hasFieldStyleErrors = HasStyleErrors(this, out var fieldStyleErrorDictionary);
//    if (!hasFieldStyleErrors)
//    {
//        return;
//    }

//    var errorMessage = ErrorMessageHelper.FormatStyleErrorMessage(fieldStyleErrorDictionary);
//    throw new InvalidStylesDefinitionException(errorMessage);
//}

//#region private static methods

///// <summary>
///// Gets a value indicating whether there are errors in field names assigned to the field list.
///// </summary>
///// <param name="fields">Field list.</param>
///// <param name="errorTable">Dictionary of fields that contains the list of objects with error.</param>
///// <returns>
///// <strong>true</strong> if field not found; otherwise, <strong>false</strong>.
///// </returns>
///// <remarks>
///// The parameter <paramref name="errorTable"/> contains a dictionary containing the field and the list of elements whose is not properly defined.
///// </remarks>
//private static bool HasFieldErrors(FieldsCollection fields, out Dictionary<BaseDataField, IEnumerable<string>> errorTable)
//{
//    SentinelHelper.ArgumentNull(fields, nameof(fields));

//    errorTable = new Dictionary<BaseDataField, IEnumerable<string>>();
//    foreach (var field in fields)
//    {
//        var typeFieldList = new List<string>();
//        switch (field.FieldType)
//        {
//            case KnownFieldType.Fixed:
//                var fixedField = (FixedField)field;

//                var fix = fixedField.Owner.Parent.Parent.Resources.Fixed;
//                var exist = fix.Contains(fixedField.Pieces);
//                if (!exist)
//                {
//                    typeFieldList.Add("Pieces");
//                }
//                else
//                {
//                    var pieces = fix[fixedField.Pieces];
//                    exist = pieces.Pieces.Contains(fixedField.Piece);
//                    if (!exist)
//                    {
//                        typeFieldList.Add("Piece");
//                    }
//                }
//                break;

//            case KnownFieldType.Group:
//                var groupField = (GroupField)field;

//                var groups = field.Owner.Parent.Parent.Resources.Groups;
//                var existGroupField = groups.Contains(groupField.Name);
//                if (!existGroupField)
//                {
//                    typeFieldList.Add("Group");
//                }
//                break;
//        }

//        var totalFixed = typeFieldList.Count;
//        if (totalFixed > 0)
//        {
//            errorTable.Add(field, typeFieldList);
//        }
//    }

//    return errorTable.Count > 0;
//}

///// <summary>
///// Gets a value indicating whether there are errors in style names assigned to the field list.
///// </summary>
///// <param name="fields">Field list.</param>
///// <param name="errorTable">Dictionary of fields that contains the list of objects with error <c>Style</c> property.</param>
///// <returns>
///// <strong>true</strong> if a specified style to a field is empty or not found in the list of defined styles; otherwise, <strong>false</strong>.
///// </returns>
///// <remarks>
///// The parameter <paramref name="errorTable"/> contains a dictionary containing the field and the list of elements whose style is not properly defined.
///// </remarks>
//private static bool HasStyleErrors(FieldsCollection fields, out Dictionary<BaseDataField, IEnumerable<string>> errorTable)
//{
//    SentinelHelper.ArgumentNull(fields, nameof(fields));

//    errorTable = new Dictionary<BaseDataField, IEnumerable<string>>();
//    foreach (var field in fields)
//    {
//        var styles = new List<string>();

//        var exist = field.Header.CheckStyleName();
//        if (!exist)
//        {
//            styles.Add("Header");
//        }

//        exist = field.Value.CheckStyleName();
//        if (!exist)
//        {
//            styles.Add("Value");
//        }

//        exist = field.Aggregate.CheckStyleName();
//        if (!exist)
//        {
//            styles.Add("Aggregate");
//        }

//        var totalStyles = styles.Count;
//        if (totalStyles > 0)
//        {
//            errorTable.Add(field, styles);
//        }
//    }

//    var totalFields = errorTable.Count;
//    return totalFields > 0;
//}

//#endregion
