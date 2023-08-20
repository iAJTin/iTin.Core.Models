
using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text;

using iTin.Core.Helpers;

namespace iTin.Core.Models.ComponentModel.Exceptions;

/// <summary>
/// This class represents the exception that is thrown when the identifier in export model is invalid.
/// </summary>
[Serializable]
public class InvalidIdentifierNameException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidIdentifierNameException"/> class.
    /// </summary>
    public InvalidIdentifierNameException()
        : base(string.Format(CultureInfo.CurrentCulture, ErrorMessage.InvalidIdentifierName))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidIdentifierNameException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public InvalidIdentifierNameException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidIdentifierNameException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public InvalidIdentifierNameException(StringBuilder message)
        : base(SentinelHelper.PassThroughNonNull(message).ToString())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidIdentifierNameException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (<strong>Nothing</strong> in Visual Basic) if no inner exception is specified.</param>
    public InvalidIdentifierNameException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidIdentifierNameException"/> class with serialized data.
    /// </summary>
    /// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
    /// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the source or destination.</param>
    protected InvalidIdentifierNameException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    { 
    }
}
