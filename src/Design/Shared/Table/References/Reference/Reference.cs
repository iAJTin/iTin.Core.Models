
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

using Newtonsoft.Json;

using iTin.Core.Helpers;
using iTin.Core.Models.ComponentModel.Exceptions;
using iTin.Core.Models.Helpers;

using RegularExpressionHelper = iTin.Core.Helpers.RegularExpressionHelper;

namespace iTin.Core.Models.Design.Table;

/// <summary>
/// Defines an external assembly reference.
/// </summary>
public partial class Reference
{
    #region private constants

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const string DefaultPath = "Default";

    #endregion

    #region private members

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string _path;

    #endregion

    #region constructor/s

    #region [public] Reference(): Initializes a new instance of the class
    /// <summary>
    /// Initializes a new instance of the <see cref="Reference"/> class.
    /// </summary>
    public Reference()
    {
        Path = DefaultPath;
    }
    #endregion

    #endregion

    #region public readonly properties

    #region [public] (ReferencesCollection) Owner: Gets the element that owns this instance
    /// <summary>
    /// Gets the element that owns this <see cref="Reference"/>.
    /// </summary>
    /// <value>
    /// The <see cref="ReferencesCollection"/> that owns this <see cref="Reference"/>.
    /// </value>
    [JsonIgnore]
    [XmlIgnore]
    [Browsable(false)]
    public ReferencesCollection Owner { get; private set; }
    #endregion

    #endregion

    #region public properties

    #region [public] (string) Assembly: Gets or sets the assembly name
    /// <summary>
    /// Gets or sets the assembly name.
    /// </summary>
    /// <value>
    /// The target field acts as filter.
    /// </value>
    [JsonProperty]
    [XmlAttribute]
    public string Assembly { get; set; }
    #endregion

    #region [public] (string) Path: Gets or sets the path where is located the assembly
    /// <summary>
    /// Gets or sets the path where is located the assembly.
    /// </summary>
    /// <value>
    /// Path where is located the assembly. To specify a relative path use the character (~). The default is "<c>Default</c>".
    /// </value>
    /// <remarks>
    /// <code lang="xml" title="ITEE Object Element Usage">
    /// <![CDATA[
    /// <Reference Path="Default|string" .../>
    /// ]]>
    /// </code>
    /// </remarks>
    /// <exception cref=ArgumentNullException">If <paramref name="value" /> is <see langword="null"/>.</exception>
    /// <exception cref="InvalidPathNameException">If <paramref name="value" /> is an invalid path name.</exception>
    [JsonProperty]
    [XmlAttribute]
    [DefaultValue(DefaultPath)]
    public string Path
    {
        get => _path.Replace(DefaultPath, "~");
        set
        {
            SentinelHelper.ArgumentNull(value, nameof(value));
            SentinelHelper.IsFalse(
                RegularExpressionHelper.IsValidPath(value), 
                new InvalidPathNameException(ErrorMessageHelper.ModelPathErrorMessage(nameof(Path), value)));

            _path = value;
        }
    }
    #endregion

    #endregion
}
