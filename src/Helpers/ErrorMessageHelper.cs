
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using iTin.Core.Models.Design.Enums;
using iTin.Core.Models.Design.Table.Fields;

namespace iTin.Core.Models.Helpers;

/// <summary> 
/// Static class than contains methods for creates error messages.
/// </summary>
internal static class ErrorMessageHelper
{
    /// <summary>
    /// Returns a <see cref="StringBuilder"/> than contains the error message for wrong field identifier.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <param name="attribute">The attribute.</param>
    /// <param name="attributeValue">The value attribute.</param>
    /// <returns>
    /// A <see cref="StringBuilder"/> that contains the error message.
    /// </returns>
    public static StringBuilder FieldIdentifierNameErrorMessage(string element, string attribute, string attributeValue)
    {
        var message = new StringBuilder();

        message.AppendFormat(CultureInfo.CurrentCulture, ErrorMessage.ModelErrorHeaderText, ErrorMessage.InvalidFieldNameText);
        message.AppendLine();
        message.AppendFormat(CultureInfo.CurrentCulture, ErrorMessage.ModelErrorSimpleElementLine, element);
        message.AppendLine();
        message.AppendFormat(CultureInfo.CurrentCulture, ErrorMessage.ModelErrorAttributeLine, attribute, attributeValue);
        message.AppendLine();
        message.AppendFormat(CultureInfo.CurrentCulture, ErrorMessage.ModelErrorCommentLine, ErrorMessage.FieldText, ErrorMessage.ModelFieldNameValidSpecialChars);

        return message;
    }

    /// <summary>
    /// Returns a <see cref="StringBuilder"/> than contains the error message for wrong field definitions.
    /// </summary>
    /// <param name="messageDictionary">Data dictionary to format.</param>
    /// <returns>
    /// A <see cref="StringBuilder"/> than contains the formatted error message.
    /// </returns>
    public static StringBuilder FormatFieldErrorMessage(Dictionary<BaseDataField, IEnumerable<string>> messageDictionary)
    {
        var message = new StringBuilder();

        message.AppendLine();
        message.AppendFormat(ErrorMessage.ModelErrorHeaderText, ErrorMessage.InvalidFieldDefinitionListText);
        message.AppendLine();
        foreach (var entry in messageDictionary)
        {
            switch (entry.Key.FieldType)
            {
                case KnownFieldType.Group:
                    var groupField = (GroupField)entry.Key;
                    message.AppendFormat(CultureInfo.CurrentCulture, ErrorMessage.ModelGroupFieldDefinitionErrorText, groupField.Name);
                    message.AppendLine();
                    break;

                case KnownFieldType.Fixed:
                    var fixedField = (FixedField)entry.Key;
                    if (entry.Value.First().Equals("Pieces"))
                    {
                        message.AppendFormat(CultureInfo.CurrentCulture, ErrorMessage.ModelFixedFieldDefinitionErrorText, fixedField.Pieces);
                    }
                    else
                    {
                        message.AppendFormat(CultureInfo.CurrentCulture, ErrorMessage.ModelPieceDefinitionErrorText, fixedField.Piece, fixedField.Pieces);
                    }

                    message.AppendLine();
                    break;
            }
        }

        return message;
    }

    /// <summary>
    /// Returns a <see cref="StringBuilder"/> than contains the error message for wrong field style definitions.
    /// </summary>
    /// <param name="messageDictionary">Data dictionary to format.</param>
    /// <returns>
    /// A <see cref="StringBuilder"/> than contains the formatted error message.
    /// </returns>
    public static StringBuilder FormatStyleErrorMessage(Dictionary<BaseDataField, IEnumerable<string>> messageDictionary)
    {
        var message = new StringBuilder();

        message.Append(ErrorMessage.StyleErrorFormatMessageHeader);
        message.AppendLine();

        foreach (var entry in messageDictionary)
        {
            switch (entry.Key.FieldType)
            {
                #region Fixed Field
                case KnownFieldType.Fixed:
                    var fixedField = (FixedField)entry.Key;

                    var qualifiedFieldNameBuilder = new StringBuilder();
                    qualifiedFieldNameBuilder.Append(fixedField.Piece);
                    qualifiedFieldNameBuilder.Append(" [ ");
                    qualifiedFieldNameBuilder.Append(fixedField.Pieces);
                    qualifiedFieldNameBuilder.Append(" ]");

                    message.AppendFormat(CultureInfo.CurrentCulture, ErrorMessage.StyleErrorFormatMessageFieldLine, qualifiedFieldNameBuilder, fixedField.Alias);
                    message.AppendLine();

                    foreach (var style in entry.Value)
                    {
                        var styleValue = style.Equals("Header")
                            ? fixedField.Header.Style
                            : fixedField.Value.Style;
                        message.AppendFormat(CultureInfo.InvariantCulture, ErrorMessage.StyleErrorFormatMessageNotFound, style, styleValue, Environment.NewLine);
                        message.AppendLine();
                    }

                    break;
                #endregion

                #region Gap Field
                case KnownFieldType.Gap:
                    var gapField = (GapField)entry.Key;

                    message.AppendFormat(CultureInfo.CurrentCulture, ErrorMessage.StyleErrorFormatMessageFieldLine, "[Gap Field]", gapField.Alias);
                    message.AppendLine();

                    foreach (var style in entry.Value)
                    {
                        var styleValue = style.Equals("Header")
                            ? gapField.Header.Style
                            : gapField.Value.Style;
                        message.AppendFormat(CultureInfo.CurrentCulture, ErrorMessage.StyleErrorFormatMessageNotFound, style, styleValue, Environment.NewLine);
                        message.AppendLine();
                    }

                    break;
                #endregion

                #region Data, Group Field
                default:
                    var dataField = (DataField)entry.Key;
                    message.AppendFormat(CultureInfo.CurrentCulture, ErrorMessage.StyleErrorFormatMessageFieldLine, dataField.Name, dataField.Alias);
                    message.AppendLine();

                    foreach (var style in entry.Value)
                    {
                        var styleValue = style.Equals("Header")
                            ? dataField.Header.Style
                            : dataField.Value.Style;
                        message.AppendFormat(CultureInfo.CurrentCulture, ErrorMessage.StyleErrorFormatMessageNotFound, style, styleValue, Environment.NewLine);
                        message.AppendLine();
                    }

                    break;
                #endregion
            }
        }

        message.Append(ErrorMessage.ModelStyleErrorCommentLine);
        return message;
    }

    /// <summary>
    /// Returns a <see cref="StringBuilder"/> than contains the error message for wrong identifier.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <param name="attribute">The attribute.</param>
    /// <param name="attributeValue">The value attribute.</param>
    /// <returns>
    /// A <see cref="StringBuilder"/> that contains the error message.
    /// </returns>
    public static StringBuilder ModelIdentifierNameErrorMessage(string element, string attribute, string attributeValue)
    {
        var message = new StringBuilder();

        message.AppendFormat(CultureInfo.CurrentCulture, ErrorMessage.ModelErrorHeaderText, ErrorMessage.InvalidIdentifierText);
        message.AppendLine();
        message.AppendFormat(CultureInfo.CurrentCulture, ErrorMessage.ModelErrorSimpleElementLine, element);
        message.AppendLine();
        message.AppendFormat(CultureInfo.CurrentCulture, ErrorMessage.ModelErrorAttributeLine, attribute, attributeValue);
        message.AppendLine();
        message.AppendFormat(CultureInfo.CurrentCulture, ErrorMessage.ModelErrorCommentLine, ErrorMessage.IdentifierText, ErrorMessage.ModelIdentifierValidSpecialChars);

        return message;
    }

    /// <summary>
    /// Returns a <see cref="StringBuilder"/> than contains the error message for wrong file name.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <param name="elementValue">Filename value.</param>
    /// <returns>
    /// A <see cref="StringBuilder"/> that contains the error message.
    /// </returns>
    public static StringBuilder ModelFileNameErrorMessage(string element, string elementValue)
    {
        var message = new StringBuilder();

        message.AppendFormat(CultureInfo.CurrentCulture, ErrorMessage.ModelErrorHeaderText, ErrorMessage.InvalidFileNameText);
        message.AppendLine();
        message.AppendFormat(CultureInfo.CurrentCulture, ErrorMessage.ModelErrorComplexElementLine, element, elementValue);
        message.AppendLine();
        message.AppendFormat(CultureInfo.CurrentCulture, ErrorMessage.ModelFileNameError, ErrorMessage.ModelFileNameInvalidSpecialChars);

        return message;
    }

    /// <summary>
    /// Returns a <see cref="StringBuilder"/> than contains the error message for wrong path.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <param name="elementValue">Filename value.</param>
    /// <returns>
    /// A <see cref="StringBuilder"/> that contains the error message.
    /// </returns>
    public static StringBuilder ModelPathErrorMessage(string element, string elementValue)
    {
        var message = new StringBuilder();

        message.AppendFormat(CultureInfo.CurrentCulture, ErrorMessage.ModelErrorHeaderText, ErrorMessage.InvalidPathText);
        message.AppendLine();
        message.AppendFormat(CultureInfo.CurrentCulture, ErrorMessage.ModelErrorComplexElementLine, element, elementValue);
        message.AppendLine();
        message.AppendFormat(CultureInfo.CurrentCulture, ErrorMessage.ModelErrorCommentLine, ErrorMessage.PathText, ErrorMessage.ModelPathNameValidSpecialChars);

        return message;
    }
}
