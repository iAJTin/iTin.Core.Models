
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Xml.Serialization;

using Newtonsoft.Json;

using iTin.Core.Helpers;
using iTin.Core.Models.ComponentModel.Exceptions;
using iTin.Core.Models.Helpers;

using ModelRegularExpressionHelper = iTin.Core.Models.Helpers.RegularExpressionHelper;

namespace iTin.Core.Models.Design.Table.Resource;

/// <summary>
/// Contains a collection of field names. Each element is result of the union of a field list.
/// </summary>
/// <remarks>
/// Belongs to: <strong><c>Groups</c></strong>. For more information, please see <see cref="GroupsCollection"/>.
/// <para><u><strong>Usage</strong></u></para>
/// <code lang="xml" title="ITEE Object Element Usage">
/// <![CDATA[
/// <?xml version="1.0" encoding="utf-8">
/// <Group ...>
///   <Field/>
///   <Field/>
///   ... 
/// </Group>
/// ]]>
/// </code>
/// <para><strong><u>Attributes</u></strong></para>
/// <list type="table">
///  <listheader>
///   <term>Attribute</term>
///   <term>Optional</term>
///   <description>Description</description>
///  </listheader>
///  <item>
///   <term><see cref="Name"/></term>
///   <term>No</term>
///   <description>Name of the group.</description>
///  </item>
/// </list>
/// <para><strong><u>Elements</u></strong></para>
/// <list type="table">
///   <listheader>
///     <term>Element</term>
///     <description>Description</description>
///   </listheader>
///   <item>
///     <term><see cref="Fields"/></term> 
///     <description>Collection of fields contained within the group. Each element is composed of a field name and origin a field separator.</description>
///   </item>
/// </list>
/// </remarks>
public partial class Group
{
    #region private members

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string _name;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private List<GroupItem> _groupItems;

    #endregion

    #region public readonly properties

    #region [public] (bool) Multiline: Gets a value indicating whether this is a multiline field
    /// <summary>
    /// Gets a value indicating whether this is a multiline field. 
    /// </summary>
    /// <value>
    /// <see langword="true"/> if is a multiline field; otherwise, <see langword="false"/>.
    /// </value>
    [JsonIgnore]
    [XmlIgnore]
    public bool Multiline => Fields.Any(field => field.Separator.Equals("New Line", StringComparison.OrdinalIgnoreCase));
    #endregion

    #region [public] (GroupsCollection) Owner: Gets the element that owns this instance
    /// <summary>
    /// Gets the element that owns this <see cref="Group"/>.
    /// </summary>
    /// <value>
    /// The <see cref="GroupsCollection" /> that owns this <see cref="Group"/>.
    /// </value>
    [JsonIgnore]
    [XmlIgnore]
    [Browsable(false)]
    public GroupsCollection Owner { get; private set; }
    #endregion

    #endregion

    #region public properties

    #region [public] (string) Name: Gets or sets the name of the group
    /// <summary>
    /// Gets or sets the name of the group.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The name of the group.<br/>
    /// Are only allow strings made ​​up of letters, numbers and following special chars <strong>'<c>_ - # * @ % $</c>'</strong>.
    /// </para>
    /// <strong><u>Usage</u></strong>:
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <Group Name="string">
    ///     ...
    /// </Group>
    /// ]]>
    /// </code>
    /// <example>
    /// <para><strong><u>Examples</u></strong>:</para>
    /// The following example creates a new group called <c>AddressGroup</c> as a result of the union of three fields.
    /// <para><c>XML</c></para>
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <Groups>
    ///   <Group Name="AddressGroup">
    ///     <Field Name="CMADR1" Separator="Comma"/>
    ///     <Field Name="CMCITY" Separator="Comma"/>
    ///     <Field Name="CMPSTAL"/>
    ///   </Group>
    /// </Groups>
    /// ]]>
    /// </code>
    /// <para><c>C#</c></para>
    /// <code lang="cs">
    /// public void CreateGroup()
    /// {
    ///     Groups groups = new Groups();
    ///
    ///     Group addressGroup = new Group
    ///     {
    ///         Name = "AddressGroup",
    ///         Fields = new List&lt;GroupItem&gt;
    ///         {
    ///             new GroupItem { Name = "CMADR1", Separator = "Comma" },
    ///             new GroupItem { Name = "CMCITY", Separator = "Comma" },
    ///             new GroupItem { Name = "CMPSTAL" }
    ///         }
    ///     };
    /// 
    ///     addressGroup.SetOwner(groups);
    ///     groups.Items.Add(addressGroup);
    /// }
    /// </code>
    /// </example>
    /// </remarks>
    /// <exception cref="ArgumentNullException">If <paramref name="value" /> is <see langword="null"/>.</exception>
    /// <exception cref="InvalidFieldIdentifierNameException">If <paramref name="value" /> not is a valid field identifier name.</exception>
    [JsonProperty]
    [XmlAttribute]
    public string Name
    {
        get => _name;
        set
        {
            SentinelHelper.ArgumentNull(value, nameof(value));
            SentinelHelper.IsFalse(
                ModelRegularExpressionHelper.IsValidFieldName(value), 
                new InvalidFieldIdentifierNameException(ErrorMessageHelper.FieldIdentifierNameErrorMessage(nameof(Group), nameof(Name), value)));

            _name = value;
        }
    }
    #endregion

    #region [public] (List<GroupItem>) Fields: Gets or sets collection of fields contained within the group
    /// <summary>
    /// Gets or sets collection of fields contained within the group.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Collection of fields contained within the group.<br/>
    /// Each element is composed of a field name and a field separator.
    /// </para>
    /// <para><u><strong>Usage</strong></u></para>
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <Group>
    ///   <Field .../>
    ///   <Field .../>
    ///   ...
    /// </Group>
    /// ]]>
    /// </code>
    /// <para><strong><u>Examples</u></strong>:</para>
    /// <example>
    /// The following example creates a new group called <c>AddressGroup</c> as a result of the union of three fields.
    /// <para><c>XML</c></para>
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <Groups>
    ///   <Group Name="AddressGroup">
    ///     <Field Name="CMADR1" Separator="Comma"/>
    ///     <Field Name="CMCITY" Separator="Comma"/>
    ///     <Field Name="CMPSTAL"/>
    ///   </Group>
    /// </Groups>
    /// ]]>
    /// </code>
    /// <para><c>C#</c></para>
    /// <code lang="cs">
    /// public void CreateGroup()
    /// {
    ///     Groups groups = new Groups();
    ///
    ///     Group addressGroup = new Group
    ///     {
    ///         Name = "AddressGroup",
    ///         Fields = new List&lt;GroupItem&gt;
    ///         {
    ///             new GroupItem { Name = "CMADR1", Separator = "Comma" },
    ///             new GroupItem { Name = "CMCITY", Separator = "Comma" },
    ///             new GroupItem { Name = "CMPSTAL" }
    ///         }
    ///     };
    /// 
    ///     addressGroup.SetOwner(groups);
    ///     groups.Items.Add(addressGroup);
    /// }
    /// </code>
    /// </example>
    /// </remarks>
    [JsonProperty("fields")]
    [XmlElement("Field")]
    public List<GroupItem> Fields
    {
        get
        {
            _groupItems ??= new List<GroupItem>();
            foreach (var item in _groupItems)
            {
                item.SetOwner(this);
            }

            return _groupItems;
        }
        set => _groupItems = value;
    }
    #endregion

    #endregion

    #region public methods

    #region [public] (void) SetOwner(GroupsCollection): Sets the element that owns this instance
    /// <summary>
    /// Sets the element that owns this <see cref="Group"/>.
    /// </summary>
    /// <param name="reference">Reference to owner.</param>
    public void SetOwner(GroupsCollection reference)
    {
        Owner = reference;
    }
    #endregion

    #endregion
}
