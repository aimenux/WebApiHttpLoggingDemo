namespace Domain.Exceptions;

public class BusinessException : ApplicationException
{
    protected BusinessException(string message) : base(message)
    {
    }

    protected static void AddCustomProperties(Exception exception, IDictionary<string, object> customProperties)
    {
        if (customProperties == null)
        {
            return;
        }

        foreach (var (key, value) in customProperties)
        {
            exception.Data.Add(key, value);
        }
    }
}