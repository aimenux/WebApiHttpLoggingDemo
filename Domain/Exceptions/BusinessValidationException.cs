namespace Domain.Exceptions;

public class BusinessValidationException : BusinessException
{
    protected BusinessValidationException(string message) : base(message)
    {
    }

    public static BusinessValidationException QuoteIsNotValid(IDictionary<string, object> customProperties = null)
    {
        var exception = new BusinessValidationException("Quote is not valid.");

        AddCustomProperties(exception, customProperties);

        return exception;
    }
}