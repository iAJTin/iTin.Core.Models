﻿
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Xml.Serialization;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using iTin.Core.Helpers;
using iTin.Core.Models.Design.Enums;
using iTin.Core.Models.Design.Helpers;

namespace iTin.Core.Models.Design;

/// <summary>
/// Represents a font. Defines a particular format for text, including font face, size, and style attributes.
/// </summary>
public partial class FontModel
{
    #region private constants

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const string DefaultFontColor = "Black";

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const string DefaultFontName = "Segoe UI";

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const YesNo DefaultFontBold = YesNo.No;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const YesNo DefaultFontItalic = YesNo.No;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const float DefaultFontSize = 10.0f;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const YesNo DefaultFontUnderline = YesNo.No;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const YesNo DefaultIsScalable = YesNo.Yes;

    #endregion

    #region private members

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string _name;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string _color;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private float _size;

    #endregion

    #region constructor/s

    /// <summary>
    /// Initializes a new instance of the <see cref="FontModel"/> class.
    /// </summary>
    public FontModel()
    {
        Color = DefaultFontColor;
        Name = DefaultFontName;
        Size = DefaultFontSize;
        Bold = DefaultFontBold;
        Italic = DefaultFontItalic;
        IsScalable = DefaultIsScalable;
        Underline = DefaultFontUnderline;
    }

    #endregion

    #region public static properties

    /// <summary>
    /// Gets default font settings.
    /// </summary>
    /// <value>
    /// A <see cref="FontModel"/> reference containing the default font settings.
    /// </value>
    public static FontModel DefaultFont => new();

    #endregion

    #region public readonly properties

    /// <summary>
    /// Gets a value that represents the different styles defined for this font.
    /// </summary>
    /// <value>
    /// A <see cref="FontStyle"/> value that represents the different styles defined for this font.
    /// </value>
    [JsonIgnore]
    [XmlIgnore]
    public FontStyle FontStyles
    {
        get
        {
            var fontStyles = FontStyle.Regular;
            if (Bold == YesNo.Yes)
            {
                fontStyles |= FontStyle.Bold;
            }

            if (Italic == YesNo.Yes)
            {
                fontStyles |= FontStyle.Italic;
            }

            if (Underline == YesNo.Yes)
            {
                fontStyles |= FontStyle.Underline;
            }

            return fontStyles;
        }
    }

    #endregion

    #region public properties

    /// <summary>
    /// Gets or sets preferred font name. The default is <b>Segoe UI</b>.
    /// </summary>
    /// <value>
    /// Preferred font name. If specified a font name not existent be use the default font. 
    /// </value>
    [JsonProperty]
    [XmlAttribute]
    [DefaultValue(DefaultFontName)]
    public string Name
    {
        get => _name;
        set
        {
            var isValidName = false;
            if (!string.IsNullOrEmpty(value))
            {
                var isValidFont = IsValidFontName(value);
                if (isValidFont)
                {
                    isValidName = true;
                }
            }

            _name = isValidName
                ? value
                : DefaultFontName;
        }
    }

    /// <summary>
    /// Gets or sets preferred font size. The default is <b>10.0</b>.
    /// </summary>
    /// <value>
    /// Preferred font size.
    /// </value>
    /// <exception cref="ArgumentOutOfRangeException">The value specified is less than of valid value.</exception>
    [JsonProperty]
    [XmlAttribute]
    [DefaultValue(DefaultFontSize)]
    public float Size
    {
        get => _size;
        set
        {
            SentinelHelper.ArgumentLessThan("value", value, 0.0f);

            _size = value;
        }
    }

    /// <summary>
    /// Gets or sets preferred font color. The default is <b>Black</b>.
    /// </summary>
    /// <value>
    /// Preferred font color.
    /// </value>
    /// <exception cref="ArgumentNullException">The value specified is <b>null</b>.</exception>
    [JsonProperty]
    [XmlAttribute]
    [DefaultValue(DefaultFontColor)]
    public string Color
    {
        get => _color;
        set
        {
            SentinelHelper.ArgumentNull(value, nameof(value));

            _color = value;
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether this font is scalable. The default is <see cref="YesNo.Yes"/>.
    /// </summary>
    /// <value>
    /// <see cref="YesNo.Yes"/> if font is scalable; otherwise, <see cref="YesNo.No"/>.
    /// </value>
    [JsonProperty]
    [XmlAttribute]
    [DefaultValue(DefaultIsScalable)]
    [JsonConverter(typeof(StringEnumConverter))]
    public YesNo IsScalable { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether bold style is applied for this font. The default is <see cref="YesNo.No"/>.
    /// </summary>
    /// <value>
    /// <see cref="YesNo.Yes"/> if bold style is applied for this font; otherwise, <see cref="YesNo.No"/>.
    /// </value>
    [JsonProperty]
    [XmlAttribute]
    [DefaultValue(DefaultFontBold)]
    [JsonConverter(typeof(StringEnumConverter))]
    public YesNo Bold { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether italic style is applied for this font. The default is <see cref="YesNo.No"/>.
    /// </summary>
    /// <value>
    /// <see cref="YesNo.Yes"/> if italic style is applied for this font; otherwise, <see cref="YesNo.No"/>.
    /// </value>
    [JsonProperty]
    [XmlAttribute]
    [DefaultValue(DefaultFontItalic)]
    [JsonConverter(typeof(StringEnumConverter))]
    public YesNo Italic { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the underline style is applied for this font. The default is <see cref="YesNo.No"/>.
    /// </summary>
    /// <value>
    /// <see cref="YesNo.Yes"/> if the underline style is applied for this font; otherwise, <see cref="YesNo.No"/>.
    /// </value>
    [JsonProperty]
    [XmlAttribute]
    [DefaultValue(DefaultFontUnderline)]
    [JsonConverter(typeof(StringEnumConverter))]
    public YesNo Underline { get; set; }

    #endregion

    #region public override properties

    /// <inheritdoc />
    /// <summary>
    /// Gets a value indicating whether this instance is default.
    /// </summary>
    /// <value>
    /// <b>true</b> if this instance contains the default; otherwise, <b>false</b>.
    /// </value>
    public override bool IsDefault =>
        Name.Equals(DefaultFontName) &&
        Bold.Equals(DefaultFontBold) &&
        Size.Equals(DefaultFontSize) &&
        Color.Equals(DefaultFontColor) &&
        Italic.Equals(DefaultFontItalic) &&
        IsScalable.Equals(DefaultIsScalable) &&
        Underline.Equals(DefaultFontUnderline);

    #endregion

    #region public methods

    /// <summary>
    /// Gets a reference to the <see cref="T:System.Drawing.Color" /> structure preferred for this font.
    /// </summary>
    /// <returns>
    /// <see cref="T:System.Drawing.Color"/> structure that represents a .NET color.
    /// </returns>
    public Color GetColor() => ColorHelper.GetColorFromString(Color);

    /// <summary>
    /// Gets a reference to native .NET font representing the font model
    /// </summary>
    /// <returns>
    /// Native .NET font representing the font model
    /// </returns>
    public Font ToFont() => new(Name, Size, FontStyles, GraphicsUnit.Pixel);

    #endregion

    #region private static methods

    /// <summary>
    /// Gets a value indicating whether the font is installed on this system.
    /// </summary>
    /// <param name="fontName">Font to check.</param>
    /// <returns>
    /// <strong>true</strong> if the font is installed on the system; otherwise, <strong>false</strong>.
    /// </returns>
    private static bool IsValidFontName(string fontName)
    {
        using var ifc = new InstalledFontCollection();

        return ifc.Families.Any(font => font.Name.Equals(fontName, StringComparison.Ordinal));
    }

    #endregion
}
