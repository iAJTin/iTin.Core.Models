
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

using Newtonsoft.Json;

using iTin.Core.Models.Design.Styles;
using iTin.Core.Models.Design.Table.Resource;
using iTin.Core.Models.Design.Table.Resource.Filters;

namespace iTin.Core.Models.Design.Table;

/// <summary>
/// Includes the description and data table definition to export. 
/// </summary>
/// <remarks>
/// <para>Belongs to: <strong><c>Table</c></strong>. For more information, please see <see cref="TableDefinition"/>.</para>
/// <para><u><strong>Usage</strong></u></para>
/// <code lang="xml" title="ITEE Object Element Usage">
/// <![CDATA[
/// <Resources>
///   <Conditions/>
///   <Filters/>
///   <Fixed/>
///   <Groups/>
///   <Styles/>
/// </Resources>
/// ]]>
/// </code>
/// <para><strong>Elements</strong></para>
/// <list type="table">
///   <listheader>
///     <term>Element</term>
///     <description>Description</description>
///   </listheader>
///   <item>
///     <term><see cref="Conditions"/></term>
///     <description>Gets or sets the collection of user-defined conditions. Each element defines a user-condition.</description>
///   </item>
///   <item>
///     <term><see cref="Filters"/></term>
///     <description>Gets or sets the collection of user-defined filters. Each element defines a data filter.</description>
///   </item>
///   <item>
///     <term><see cref="Fixed"/></term>
///     <description>Collection of user-defined pieces. Each element is a collection of smaller pieces result of splitting the reference field.</description>
///   </item>
///   <item>
///     <term><see cref="Groups"/></term>
///     <description>Collection of user-defined groups. Each element is result from the union of several data field.</description>
///   </item>
///   <item>
///     <term><see cref="Styles"/></term>
///     <description>Collection of user-defined styles. Each element defines type of content, such as the background color, the alignment type, the data type and the font type.</description>
///   </item>
/// </list>
/// </remarks>
public partial class Resources
{
    #region private members

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private FixedCollection _fixed;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private GroupsCollection _groups;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private FiltersCollection _filters;

    //[DebuggerBrowsable(DebuggerBrowsableState.Never)]
    //private ConditionsCollection _conditions;

    #endregion

    #region constructor/s

    #region [public] Resources(TableDefinition): Initializes a new instance of the class
    /// <summary>
    /// Initializes a new instance of the <see cref="TableDefinition"/> class.
    /// </summary>
    public Resources() : this(null)
    {
    }
    #endregion

    #region [public] Resources(TableDefinition): Initializes a new instance of the class with a parent instance
    /// <summary>
    /// Initializes a new instance of the <see cref="TableDefinition"/> class with a parent instance.
    /// </summary>
    public Resources(TableDefinition parent)
    {
        Parent = parent;
    }
    #endregion

    #endregion

    #region public readonly properties

    #region [public] (TableDefinition) Parent: Gets the parent element of the element
    /// <summary>
    /// Gets the parent element of the element.
    /// </summary>
    /// <value>
    /// The element that represents the container element of the element.
    /// </value>
    [JsonIgnore]
    [XmlIgnore]
    [Browsable(false)]
    public TableDefinition Parent { get; private set; }
    #endregion

    #endregion

    #region public properties

    //#region [public] (ConditionsCollection) Conditions: Gets or sets the collection of user-defined conditions
    ///// <summary>
    ///// Gets or sets the collection of user-defined conditions.
    ///// </summary>
    ///// <remarks>
    ///// <para>
    ///// Collection of user-defined conditions.<br/>
    ///// Each element defines a user-condition.
    ///// </para>
    ///// <para><u><strong>Usage</strong></u></para>
    ///// <code lang="xml" title="ITEE Object Element Usage">
    ///// <![CDATA[
    ///// <Resources>
    /////   <Conditions>
    /////     <MaximumValue|MinimumValue|RemarksValue|WhenChangeValue|ZeroValue .../>
    /////     ...
    /////     ...
    /////   </Conditions>
    /////   ...
    ///// </Resources>
    ///// ]]>
    ///// </code>
    ///// </remarks>
    //[JsonProperty("conditions")]
    //[XmlArrayItem("MaximumValue", typeof(MaximumCondition), IsNullable = false)]
    //[XmlArrayItem("MinimumValue", typeof(MinimumCondition), IsNullable = false)]
    //[XmlArrayItem("RemarksValue", typeof(RemarksCondition), IsNullable = false)]
    //[XmlArrayItem("WhenChangeValue", typeof(WhenChangeCondition), IsNullable = false)]
    //[XmlArrayItem("ZeroValue", typeof(ZeroCondition), IsNullable = false)]
    //public ConditionsCollection Conditions
    //{
    //    get => _conditions ??= new ConditionsCollection(this);
    //    set => _conditions = value;
    //}
    //#endregion

    #region [public] (FiltersCollection) Filters: Gets or sets the collection of user-defined filters
    /// <summary>
    /// Gets or sets the collection of user-defined filters.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Collection of user-defined filters.<br/>
    /// Each element defines a data filter.
    /// </para>
    /// <para><u><strong>Usage</strong></u></para>
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <Resources>
    ///   <Filters>
    ///     <Filter .../>
    ///     ...
    ///     ... 
    ///   </Filters>
    ///   ...
    /// <Resources>
    /// ]]>
    /// </code>
    /// </remarks>
    [JsonProperty("filters")]
    [XmlArrayItem("Filter", typeof(Filter), IsNullable = false)]
    public FiltersCollection Filters
    {
        get => _filters ??= new FiltersCollection(this);
        set => _filters = value;
    }
    #endregion

    #region [public] (Fixed) Fixed: Gets or sets collection of user-defined pieces
    /// <summary>
    /// Gets or sets a collection of user-defined pieces.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Collection of user-defined pieces.<br/>
    /// Each element is a collection of smaller pieces result of splitting the reference field.
    /// </para>
    /// <para><u><strong>Usage</strong></u></para>
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <Resources>
    ///   <Fixed>
    ///     <Pieces ...>
    ///       <Piece .../>
    ///       ...
    ///       ...
    ///     </Pieces>
    ///   </Fixed>
    ///   ...
    /// </Resources>
    /// ]]>
    /// </code>
    /// </remarks>
    [JsonProperty("fixed")]
    [XmlArrayItem("Pieces", typeof(Fixed), IsNullable = false)]
    public FixedCollection Fixed
    {
        get => _fixed ??= new FixedCollection(this);
        set => _fixed = value;
    }
    #endregion

    #region [public] (GroupsCollections) Groups: Gets or sets collection of user-defined groups
    /// <summary>
    /// Gets or sets a collection of user-defined groups.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Collection of user-defined groups.<br/>
    /// Each element is result from the union of several data field.
    /// </para>
    /// <para><u><strong>Usage</strong></u></para>
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <Resources>
    ///   <Groups>
    ///     <Group .../>
    ///     ...
    ///     ...
    ///   </Groups>
    ///   ...
    /// </Resources>
    /// ]]>
    /// </code>
    /// </remarks>
    [JsonProperty("groups")]
    [XmlArrayItem("Group", typeof(Group), IsNullable = false)]
    public GroupsCollection Groups
    {
        get => _groups ??= new GroupsCollection(this);
        set => _groups = value;
    }
    #endregion

    #region [public] (IStyles) Styles: Collection of user-defined styles
    /// <summary>
    /// Gets or sets a collection of user-defined styles.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Collection of user-defined styles.<br/>
    /// Each element defines type of content, such as the background color, the alignment type, the data type and the font type.
    /// </para>
    /// <para><u><strong>Usage</strong></u></para>
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <Resources>
    ///   <Styles>
    ///     <Style .../>
    ///     ...
    ///     ...
    ///   </Styles>
    ///   ...
    /// </Resources>
    /// ]]>
    /// </code>
    /// </remarks>
    [XmlIgnore]
    [JsonProperty("styles")]
    [XmlArrayItem("Style", typeof(BaseStyle), IsNullable = false)]
    public IStyles Styles { get; set; }
    #endregion

    #endregion

    #region public methods

    #region [public] (IStyle) GetStyleResourceByName(string): Gets specified style resource by name
    /// <summary>
    /// Gets specified style resource by name.
    /// </summary>
    /// <param name="name">Name of style.</param>
    /// <returns>
    /// A <see cref="IStyle"/> which contains specified style resource.
    /// </returns>
    public IStyle GetStyleResourceByName(string name) => Styles.GetBy(name);
    #endregion

    #endregion

    #region internal methods

    #region [internal] (void) SetParent(TableDefinition): Sets the parent element of the element
    /// <summary>
    /// Sets the parent element of the element.
    /// </summary>
    /// <param name="reference">Reference to parent.</param>
    internal void SetParent(TableDefinition reference)
    {
        Parent = reference;
    }
    #endregion

    #endregion
}
