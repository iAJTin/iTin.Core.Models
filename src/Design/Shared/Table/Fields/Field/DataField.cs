
using System;
using System.Diagnostics;
using System.Xml.Serialization;

using iTin.Core.Helpers;
using iTin.Core.Models.ComponentModel.Exceptions;
using iTin.Core.Models.Helpers;

using ModelsRegularExpressionHelper = iTin.Core.Models.Helpers.RegularExpressionHelper;

namespace iTin.Core.Models.Design.Table.Fields;

/// <summary>
/// A Specialization of <see cref="BaseDataField"/> class.<br/>
/// Which acts as the base class for different data fields.<br/>
/// Represents a data field.
/// </summary>
/// <remarks>
/// <para>
/// The following table shows different data fields.
/// </para>
/// <list type="table">
///   <listheader>
///     <term>Class</term>
///     <description>Description</description>
///   </listheader>
///   <item>
///     <term><see cref="GroupField"/></term>
///     <description>Represents a field as union of several data field.</description>
///   </item>
/// </list>
/// <para>Belongs to: <strong><c>Fields</c></strong>. For more information, please see <see cref="Fields"/>.</para>
/// <para><strong><u>Usage</u></strong>:</para>
/// <code lang="xml" title="ITEE Object Element Usage">
/// <![CDATA[
/// <Field ...>
///   <Header/>
///   <Value/>
///   <Aggregate/>
/// </Field>
/// ]]>
/// </code>
/// <para><strong><u>Attributes</u></strong></para>
/// <table>
///   <thead>
///     <tr>
///       <th>Attribute</th>
///       <th>Optional</th>
///       <th>Description</th>
///       </tr>
///   </thead>
///   <tbody>
///     <tr>
///       <td><see cref="Name"/></td>
///       <td align="center">No</td>
///       <td>Name of the field.</td>
///     </tr>
///     <tr>
///       <td><see cref="BaseDataField.Alias"/></td>
///       <td align="center">Yes</td>
///       <td>The alias of data field. This value is used as the column header.</td>
///     </tr>
///   </tbody>
/// </table>
/// <para><strong><u>Elements</u></strong></para>
/// <list type="table">
///   <listheader>
///     <term>Element</term>
///     <description>Description</description>
///   </listheader>
///   <item>
///     <term><see cref="BaseDataField.Header"/></term> 
///     <description>Reference to visual setting of header of the data field.</description>
///   </item>
///   <item>
///     <term><see cref="BaseDataField.Value"/></term> 
///     <description>Reference to visual setting of value of the data field.</description>
///   </item>
///   <item>
///     <term><see cref="BaseDataField.Aggregate"/></term> 
///     <description>Reference to visual setting of aggregate function of the data field.</description>
///   </item>
/// </list>
/// </remarks>
public partial class DataField
{
    #region private members

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string _name;

    #endregion

    #region public properties

    /// <summary>
    /// Gets or sets the name of the field.
    /// </summary>
    /// <value>
    /// The name of the field.
    /// Are only allow strings made ​​up of letters, numbers and following special chars <strong>'<c>_ - # * @ % $</c>'</strong>.
    /// </value>
    /// <remarks>
    /// <para><strong><u>Usage</u></strong>:</para>
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <Field Name="string" ...>
    /// ...
    /// </Field>
    /// ]]>
    /// </code>
    /// </remarks>
    /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/></exception>
    /// <exception cref="InvalidFieldIdentifierNameException"><paramref name="value" /> is not a valid field identifier name.</exception>
    [XmlAttribute]
    public string Name
    {
        get => GetStaticBindingValue(_name);
        set
        {
            SentinelHelper.ArgumentNull(value, nameof(value));

            var isBinded = ModelsRegularExpressionHelper.IsStaticBindingResource(value);
            if (!isBinded)
            {
                SentinelHelper.IsFalse(
                    ModelsRegularExpressionHelper.IsValidFieldName(value), 
                    new InvalidFieldIdentifierNameException(
                        ErrorMessageHelper.FieldIdentifierNameErrorMessage(
                            "Field",
                            "Name", 
                            value)));                        
            }
                
            _name = value;
        }
    }

    #endregion
}
