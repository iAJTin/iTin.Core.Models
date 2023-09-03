
using System;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using NativeIO = System.IO;

namespace iTin.Core.Models.Data.Provider;

/// <summary>
/// A Specialization of <see cref="BaseDataProvider"/><br/>
/// Represents a source object based on <strong>Json</strong>
/// </summary>
public class JsonProvider : BaseDataProvider
{
    #region private readonly members

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly Uri _inputJsonUri;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly DataProviderConfiguration _configuration;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly char[] _monarchSpecialChars = { '#', '*', '@' };

    #endregion

    #region constructor/s

    /// <summary>
    /// Initializes a new instance of the <see cref="JsonProvider"/> class.
    /// </summary>
    /// <param name="inputUri">Target uri</param>
    /// <param name="configuration"></param>
    public JsonProvider(Uri inputUri, DataProviderConfiguration configuration = null)
    {
        _inputJsonUri = inputUri;
        _configuration = configuration;
        SpecialChars = _monarchSpecialChars;
    }

    #endregion

    #region public override readonly properties

    /// <summary>
    /// Gets a value indicating whether you can create an <strong>XML</strong> file from the current instance of the object.
    /// </summary>
    /// <value>
    /// Always returns <see langword="true"/>.
    /// </value>
    public override bool CanCreateInputXml => true;

    /// <summary>
    /// Gets a value indicating whether this instance can get data table.
    /// </summary>
    /// <value>
    /// Always returns <see langword="false"/>.
    /// </value>
    public override bool CanGetDataTable => false;

    #endregion

    #region protected override methods

    /// <inheritdoc />
    protected override void OnCreateInputXml()
    {
        // Read json file
        var jsonString = NativeIO.File.ReadAllText(_inputJsonUri.AbsolutePath);

        try
        {
            // Intenta analizar el JSON
            var parsedJson = JToken.Parse(jsonString);

            JObject jsonObject = null;
            switch (parsedJson)
            {
                case JObject @object:

                    var jsonNodes = parsedJson.SelectTokens(_configuration.InputNodes);

                    jsonObject = JObject.Parse(jsonString);
                    break;

                case JArray array:

                    jsonObject = new JObject
                    {
                        [_configuration.InputNodes] = parsedJson
                    };

                    break;

                default:
                    throw new InvalidCastException("No es un JSON válido");
            }

            var a = ConvertJsonObjectToXml(jsonObject);

            // Convert JSON into XML
            var xmlDocument = JsonConvert.DeserializeXmlNode(jsonObject.ToString(), "Root", true);
            
            // Save XML document
            xmlDocument?.Save(InputUri.AbsolutePath);
        }
        catch (JsonReaderException)
        {
            throw new InvalidCastException("No es un JSON válido");
        }



    }

    private static XElement ConvertJsonObjectToXml(JObject jsonObject)
    {
        XElement root = new XElement("Root");

        foreach (var property in jsonObject.Properties())
        {
            if (property.Value is JValue)
            {
                // Si es un valor JSON simple, lo convertimos en un atributo
                root.Add(new XAttribute(property.Name, property.Value));
            }
            else if (property.Value is JObject)
            {
                // Si es un objeto JSON, lo convertimos en un elemento XML
                var element = new XElement(property.Name);
                element.Add(ConvertJsonObjectToXml((JObject)property.Value));
                root.Add(element);
            }
            else if (property.Value is JArray)
            {
                // Si es una matriz JSON, convertimos sus elementos en elementos XML
                foreach (var item in (JArray)property.Value)
                {
                    var element = new XElement(property.Name);
                    element.Add(ConvertJsonObjectToXml((JObject)item));
                    root.Add(element);
                }
            }
        }

        return root;
    }

    #endregion
}
