using System.Runtime.Serialization;

namespace Domain.Exceptions;

public class BusinessValidationException : BusinessException
{
    protected BusinessValidationException()
    {
    }

    protected BusinessValidationException(string message) : base(message)
    {
    }

    protected BusinessValidationException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected BusinessValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public static BusinessValidationException QuoteIsNotValid(IDictionary<string, object> customProperties = null)
    {
        var exception = new BusinessValidationException("Quote is not valid.");

        AddCustomProperties(exception, customProperties);

        return exception;
    }
}